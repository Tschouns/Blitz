//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Base.RuntimeChecks;
    using Camera;
    using Display;
    using Geometry.Elements;
    using RenderLoop.Callback;
    using Point = Geometry.Elements.Point;
    using System.Numerics;

    /// <summary>
    /// Implements <see cref="IDrawCallback{TGameState}"/>.
    /// </summary>
    public sealed class DemoGameDisplayRenderer : IDisposable, IDrawCallback<DemoGameState>
    {
        /// <summary>
        /// The <see cref="IDisplay"/> to draw to.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// A sprite representing a car.
        /// </summary>
        private readonly ISprite _carSprite;

        /// <summary>
        /// Stores the current game state, to enable <see cref="DrawToDisplay(IDrawingContext)"/> to access it.
        /// </summary>
        private DemoGameState _currentGameState;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGameDisplayRenderer"/> class.
        /// </summary>
        public DemoGameDisplayRenderer(
            IDisplayFactory displayFactory,
            DisplayProperties displayProperties)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(displayProperties, nameof(displayProperties));

            this._display = displayFactory.CreateDisplay(displayProperties, this.DrawToDisplay);

            // Load sprites
            var carSpritePositionOrigin = new Point(Images.Car.Size.Width / 2, Images.Car.Size.Height / 2);
            this._carSprite = this._display.SpriteManager.LoadFromDrawingBitmap(
                Images.Car,
                carSpritePositionOrigin,
                Math.PI,
                0.1f);
        }

        /// <summary>
        /// Shows the display.
        /// </summary>
        public void ShowDisplay()
        {
            this._display.Show();
        }

        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            this._display.Dispose();
        }

        /// <summary>
        /// See <see cref="IDrawCallback{TGameState}.Draw(TGameState)"/>.
        /// </summary>
        public bool Draw(DemoGameState gameState)
        {
            Checks.AssertNotNull(gameState, nameof(gameState));

            this._currentGameState = gameState;
            return this._display.DrawFrame();
        }

        /// <summary>
        /// Helper method: transforms a specified polygon, using the specified transformation, and returns the
        /// transformed corners of the polygon.
        /// </summary>
        private static IEnumerable<Point> TransformPolygonForDrawing(
            Polygon polygon,
            ICameraTransformation transform)
        {
            var transformedCorners = polygon.Corners.Select(x => transform.WorldToViewport(x)).ToList();

            return transformedCorners;
        }

        /// <summary>
        /// Draw callback for the display.
        /// </summary>
        private void DrawToDisplay(IDrawingContext drawingContext)
        {
            Checks.AssertNotNull(drawingContext, nameof(drawingContext));

            var cameraTransformation = this._currentGameState.CameraTransformation;

            // Draw the buildings.
            foreach (var building in this._currentGameState.Buildings)
            {
                drawingContext.DrawPolygon(
                    TransformPolygonForDrawing(building.Polygon, cameraTransformation),
                    building.Color,
                    2);
            }

            // Draw the explosions.
            foreach (var explosion in this._currentGameState.Explosions)
            {
                ////var radius = explosion.Circle.Radius;
                ////var startPosition = new Point(
                ////    explosion.Circle.Center.X - (radius / 2),
                ////    explosion.Circle.Center.Y - (radius / 2));

                ////var temporaryHackPolygon = new Polygon(
                ////    startPosition,
                ////    new Point(startPosition.X + radius, startPosition.Y),
                ////    new Point(startPosition.X + radius, startPosition.Y + radius),
                ////    new Point(startPosition.X, startPosition.Y + radius));

                ////drawingContext.DrawPolygon(
                ////    TransformPolygonForDrawing(temporaryHackPolygon, transformation),
                ////    explosion.Color,
                ////    2);

                drawingContext.DrawCircle(
                    cameraTransformation.WorldToViewport(explosion.Circle.Center),
                    cameraTransformation.WorldToViewport(explosion.Circle.Radius),
                    explosion.Color,
                    2);
            }

            // Draw the cars.
            foreach (var car in this._currentGameState.Cars)
            {
                var positionVector = new System.Numerics.Vector2((float)car.Position.X, (float)car.Position.Y);
                var positionTransformation = Matrix3x2.CreateTranslation(positionVector);

                var rotationTransformation = Matrix3x2.CreateRotation(-(float)car.Orientation);

                this._carSprite.Draw(
                    rotationTransformation *
                    positionTransformation *
                    cameraTransformation.WorldToViewportMatrix3x2());

                drawingContext.DrawPolygon(
                    TransformPolygonForDrawing(car.Polygon, cameraTransformation),
                    car.Color,
                    2);
            }

            // Draw the cross.
            var width = this._display.Properties.Resolution.Width;
            var height = this._display.Properties.Resolution.Height;

            drawingContext.DrawLine(
                new Point(0, 0),
                new Point(width, height),
                Color.Green,
                1);

            drawingContext.DrawLine(
                new Point(0, height),
                new Point(width, 0),
                Color.Green,
                1);
        }
    }
}

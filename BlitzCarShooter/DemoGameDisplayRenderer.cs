﻿//-----------------------------------------------------------------------
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
    using Display.Sprites;
    using Geometry.Elements;
    using RenderLoop.Callback;
    using Point = Geometry.Elements.Point;
    using Geometry.Extensions;
    using Geometry.Transformation;

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
        /// A sprite representing a destroyed car.
        /// </summary>
        private readonly ISprite _carDestroyedSprite;

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
            ArgumentChecks.AssertNotNull(displayFactory, nameof(displayFactory));
            ArgumentChecks.AssertNotNull(displayProperties, nameof(displayProperties));

            this._display = displayFactory.CreateDisplay(displayProperties, this.DrawToDisplay);

            // Load sprites
            var carSpritePositionOrigin = new Point(Images.Car.Size.Width / 2, Images.Car.Size.Height / 2);

            this._carSprite = this._display.SpriteManager.LoadFromDrawingBitmap(
                Images.Car,
                carSpritePositionOrigin,
                Math.PI,
                0.02);

            this._carDestroyedSprite = this._display.SpriteManager.LoadFromDrawingBitmap(
                Images.CarDestroyed,
                carSpritePositionOrigin,
                Math.PI,
                0.02);
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
            ArgumentChecks.AssertNotNull(gameState, nameof(gameState));

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
            ArgumentChecks.AssertNotNull(drawingContext, nameof(drawingContext));

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
                drawingContext.DrawCircle(
                    cameraTransformation.WorldToViewport(explosion.Circle.Center),
                    cameraTransformation.WorldToViewport(explosion.Circle.Radius),
                    explosion.Color,
                    2);
            }

            // Draw the cars.
            foreach (var car in this._currentGameState.Cars)
            {
                //drawingContext.DrawPolygon(
                //    TransformPolygonForDrawing(car.Polygon, cameraTransformation),
                //    car.Color,
                //    2);

                // First rotate, and then translate the sprite.
                var transformation =
                    Matrix3x3.CreateTranslation(car.Position.AsVector()) *
                    Matrix3x3.CreateRotation(car.Orientation);

                // Apply camera transformation on top, and draw.
                var finalTransformation = cameraTransformation.WorldToViewportMatrix3x3 * transformation;
                if (car.IsDestroyed)
                {
                    this._carDestroyedSprite.Draw(finalTransformation);
                }
                else
                {
                    this._carSprite.Draw(finalTransformation);
                }
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

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDemoGunBoat
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
    using Geometry.Transformation;
    using RenderLoop.Callback;
    using Point = Geometry.Elements.Point;

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
            // ....
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

            // Draw origin cross
            drawingContext.DrawLine(new Point(0, 1000), new Point(0, -1000), Color.DarkGray, 1f);
        }
    }
}

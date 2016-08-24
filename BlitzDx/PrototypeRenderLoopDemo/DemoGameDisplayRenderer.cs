//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx.PrototypeRenderLoopDemo
{
    using System;
    using System.Drawing;
    using Base.RuntimeChecks;
    using Display;
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
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(displayProperties, nameof(displayProperties));

            this._display = displayFactory.CreateDisplay(displayProperties, this.DrawToDisplay);
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
        /// Draw callback for the display.
        /// </summary>
        private void DrawToDisplay(IDrawingContext drawingContext)
        {
            Checks.AssertNotNull(drawingContext, nameof(drawingContext));

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

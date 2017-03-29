//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System;
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents event arguments for the <see cref="RenderingCanvas.Rendering"/> event.
    /// </summary>
    public class RenderingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingEventArgs"/> class.
        /// </summary>
        public RenderingEventArgs(IDrawingHandler drawingHandler)
        {
            ArgumentChecks.AssertNotNull(drawingHandler, nameof(drawingHandler));

            this.DrawingHandler = drawingHandler;
        }

        /// <summary>
        /// Gets the <see cref="IDrawingHandler"/> to draw on during rendering.
        /// </summary>
        public IDrawingHandler DrawingHandler { get; }
    }
}

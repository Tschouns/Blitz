//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// A canvas which provides a <see cref="Rendering"/> event.
    /// </summary>
    public class RenderingCanvas : Canvas
    {
        /// <summary>
        /// Is fired when this canvas is rendered.
        /// </summary>
        public event EventHandler<RenderingEventArgs> Rendering;

        /// <summary>
        /// See <see cref="Canvas"/>.
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
                      
            if (this.Rendering != null)
            {
                IDrawingHandler drawingHandler = new DrawingContextDrawingHandler(dc);
                var eventArgs = new RenderingEventArgs(drawingHandler);

                this.Rendering(this, eventArgs);
            }
        }
    }
}

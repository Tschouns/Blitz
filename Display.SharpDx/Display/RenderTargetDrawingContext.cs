//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Display
{
    using System;
    using Base.RuntimeChecks;
    using Extensions;
    using Geometry.Elements;
    using SharpDX.Direct2D1;

    /// <summary>
    /// Implements <see cref="IDrawingContext"/>, as a wrapper of <see cref="RenderTarget"/>.
    /// </summary>
    public class RenderTargetDrawingContext : IDrawingContext
    {
        /// <summary>
        /// The render target to draw to.
        /// </summary>
        private RenderTarget _renderTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTargetDrawingContext"/> class.
        /// </summary>
        public RenderTargetDrawingContext(RenderTarget renderTarget)
        {
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));

            this._renderTarget = renderTarget;
        }

        /// <summary>
        /// See <see cref="IDrawingContext.DrawLine"/>.
        /// </summary>
        public void DrawLine(Point point1, Point point2, System.Drawing.Color color, float strokeWidth)
        {
            Checks.AssertIsStrictPositive(strokeWidth, nameof(strokeWidth));

            if (this._renderTarget.IsDisposed)
            {
                throw new InvalidOperationException("The RenderTarget has already been disposed.");
            }

            using (var brush = new SolidColorBrush(this._renderTarget, color.ToSharpDxColor()))
            {
                this._renderTarget.DrawLine(
                    point1.ToSharpDxVector2(),
                    point2.ToSharpDxVector2(),
                    brush,
                    strokeWidth);
            }
        }
    }
}

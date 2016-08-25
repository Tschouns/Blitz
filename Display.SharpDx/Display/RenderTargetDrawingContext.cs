//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Display
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Base.RuntimeChecks;
    using Extensions;
    using Geometry.Elements;
    using SharpDX.Direct2D1;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// Implements <see cref="IDrawingContext"/>, as a wrapper of <see cref="RenderTarget"/>.
    /// </summary>
    public class RenderTargetDrawingContext : IDrawingContext
    {
        /// <summary>
        /// Stores a reference to the render target.
        /// </summary>
        private readonly RenderTarget _renderTarget;

        /// <summary>
        /// The render target height; used to vertically "flip" positions.
        /// </summary>
        private readonly double _renderTargetHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTargetDrawingContext"/> class.
        /// </summary>
        public RenderTargetDrawingContext(
            RenderTarget renderTarget,
            double renderTargetHeight)
        {
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));
            Checks.AssertIsStrictPositive(renderTargetHeight, nameof(renderTargetHeight));

            this._renderTarget = renderTarget;
            this._renderTargetHeight = renderTargetHeight;
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
                    point1.ToSharpDxVector2Flipped(this._renderTargetHeight),
                    point2.ToSharpDxVector2Flipped(this._renderTargetHeight),
                    brush,
                    strokeWidth);
            }
        }

        /// <summary>
        /// See <see cref="IDrawingContext.DrawPolygon"/>.
        /// </summary>
        public void DrawPolygon(IEnumerable<Point> points, System.Drawing.Color color, float strokeWidth)
        {
            Checks.AssertNotNull(points, nameof(points));
            Checks.AssertIsStrictPositive(strokeWidth, nameof(strokeWidth));

            if (this._renderTarget.IsDisposed)
            {
                throw new InvalidOperationException("The RenderTarget has already been disposed.");
            }

            this.DrawPathInternal(points, true, color, strokeWidth);
        }

        /// <summary>
        /// See <see cref="IDrawingContext.DrawCircle"/>.
        /// </summary>
        public void DrawCircle(Point center, double radius, Color color, float strokeWidth)
        {
            var ellipse = new Ellipse(center.ToSharpDxVector2Flipped(this._renderTargetHeight), (float)radius, (float)radius);

            using (var brush = new SolidColorBrush(this._renderTarget, color.ToSharpDxColor()))
            {
                this._renderTarget.DrawEllipse(ellipse, brush, strokeWidth);
            }
        }

        /// <summary>
        /// Internal implementation: draws a path or a closed path, i.e. a polygon.
        /// </summary>
        private void DrawPathInternal(IEnumerable<Point> points, bool closed, System.Drawing.Color color, float strokeWidth)
        {
            Point? origin = null;
            Point? lastPoint = null;

            foreach (var point in points)
            {
                if (!lastPoint.HasValue)
                {
                    origin = point;
                    lastPoint = point;
                    continue;
                }

                this.DrawLine(lastPoint.Value, point, color, strokeWidth);
                lastPoint = point;
            }

            // Draw closing segment.
            if (closed && lastPoint.HasValue && !object.Equals(lastPoint.Value, origin.Value))
            {
                this.DrawLine(lastPoint.Value, origin.Value, color, strokeWidth);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using Base.RuntimeChecks;
    using Geometry.Elements;

    /// <summary>
    /// See <see cref="IDrawingHandler"/>.
    /// </summary>
    public class DrawingContextDrawingHandler : IDrawingHandler
    {
        /// <summary>
        /// Stores the <see cref="DrawingContext"/> to draw on.
        /// </summary>
        private readonly DrawingContext _drawingContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingContextDrawingHandler"/> class.
        /// </summary>
        public DrawingContextDrawingHandler(DrawingContext drawingContext)
        {
            this._drawingContext = drawingContext;
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawDot"/>.
        /// </summary>
        public void DrawDot(Point point)
        {
            var radius = 5;

            this._drawingContext.DrawEllipse(
                Brushes.Orange,
                new Pen(Brushes.OrangeRed, 2),
                new System.Windows.Point(point.X, point.Y),
                radius,
                radius);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawLineSegment"/>.
        /// </summary>
        public void DrawLineSegment(Line line)
        {
            ArgumentChecks.AssertNotNull(line, nameof(line));

            this.DrawLineSegment(line, Brushes.Black);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawLineSegment"/>.
        /// </summary>
        public void DrawLineSegment(Line line, Brush brush)
        {
            ArgumentChecks.AssertNotNull(line, nameof(line));
            ArgumentChecks.AssertNotNull(brush, nameof(brush));

            this._drawingContext.DrawLine(
                new Pen(brush, 2),
                new System.Windows.Point(line.Point1.X, line.Point1.Y),
                new System.Windows.Point(line.Point2.X, line.Point2.Y));
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPath"/>.
        /// </summary>
        public void DrawPath(IEnumerable<Point> points)
        {
            this.DrawPathInternal(points, false, Brushes.Black);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPath"/>.
        /// </summary>
        public void DrawPath(IEnumerable<Point> points, Brush brush)
        {
            this.DrawPathInternal(points, false, brush);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPolygon"/>.
        /// </summary>
        public void DrawPolygon(IEnumerable<Point> points)
        {
            this.DrawPathInternal(points, true, Brushes.Black);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPolygon"/>.
        /// </summary>
        public void DrawPolygon(IEnumerable<Point> points, Brush brush)
        {
            this.DrawPathInternal(points, true, brush);
        }

        /// <summary>
        /// Internal implementation: draws a path or a closed path, i.e. a polygon.
        /// </summary>
        private void DrawPathInternal(IEnumerable<Point> points, bool closed, Brush brush)
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

                this.DrawLineSegment(new Line(lastPoint.Value, point), brush);
                lastPoint = point;
            }

            // Draw closing segment.
            if (closed && lastPoint.HasValue && !object.Equals(lastPoint.Value, origin.Value))
            {
                this.DrawLineSegment(new Line(lastPoint.Value, origin.Value), brush);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
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
        private readonly DrawingContext drawingContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingContextDrawingHandler"/> class.
        /// </summary>
        public DrawingContextDrawingHandler(DrawingContext drawingContext)
        {
            this.drawingContext = drawingContext;
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawDot"/>.
        /// </summary>
        public void DrawDot(Point point)
        {
            var radius = 5;

            this.drawingContext.DrawEllipse(
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
            Checks.AssertNotNull(line, nameof(line));

            this.drawingContext.DrawLine(
                new Pen(Brushes.Black, 2),
                new System.Windows.Point(line.Point1.X, line.Point1.Y),
                new System.Windows.Point(line.Point2.X, line.Point2.Y));
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPath"/>.
        /// </summary>
        public void DrawPath(IEnumerable<Point> points)
        {
            this.DrawPathInternal(points, false);
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPolygon"/>.
        /// </summary>
        public void DrawPolygon(IEnumerable<Point> points)
        {
            this.DrawPathInternal(points, true);
        }

        /// <summary>
        /// Internal implementation: draws a path or a closed path, i.e. a polygon.
        /// </summary>
        private void DrawPathInternal(IEnumerable<Point> points, bool closed)
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

                this.DrawLineSegment(new Line(lastPoint.Value, point));
                lastPoint = point;
            }

            // Draw closing segment.
            if (closed && lastPoint.HasValue && !object.Equals(lastPoint.Value, origin.Value))
            {
                this.DrawLineSegment(new Line(lastPoint.Value, origin.Value));
            }
        }
    }
}

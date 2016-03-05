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
    using Geometry;
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
        /// See <see cref="DrawingContext.DrawDot"/>.
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
        /// See <see cref="DrawingContext.DrawLineSegment"/>.
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
        /// See <see cref="DrawingContext.DrawPath"/>.
        /// </summary>
        public void DrawPath(IEnumerable<Point> points)
        {
            throw new NotImplementedException();
        }
    }
}

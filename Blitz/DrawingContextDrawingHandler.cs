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
    using Geometry;

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
        public void DrawLineSegment(Point point1, Point point2)
        {
            throw new NotImplementedException();
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

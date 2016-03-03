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

    /// <summary>
    /// See <see cref="IDrawingHandler"/>.
    /// </summary>
    public class CanvasDrawingHandler : IDrawingHandler
    {
        /// <summary>
        /// Stores the <see cref="IOpenCanvas"/> to draw on.
        /// </summary>
        private readonly IOpenCanvas canvas;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasDrawingHandler"/> class.
        /// </summary>
        public CanvasDrawingHandler(IOpenCanvas canvas)
        {
            Checks.AssertNotNull(canvas, nameof(canvas));

            this.canvas = canvas;
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawDot"/>.
        /// </summary>
        public void DrawDot(Point point)
        {
            var drawing = new DrawingVisual();

            this.canvas.AddDrawing(drawing);

            using (DrawingContext drawingContext = drawing.RenderOpen())
            {
                var radius = 5;

                drawingContext.DrawEllipse(
                    Brushes.Orange,
                    new Pen(Brushes.OrangeRed, 2),
                    new System.Windows.Point(point.X, point.Y),
                    radius,
                    radius);
            }
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawLineSegment"/>.
        /// </summary>
        public void DrawLineSegment(Point point1, Point point2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IDrawingHandler.DrawPath"/>.
        /// </summary>
        public void DrawPath(IEnumerable<Point> points)
        {
            throw new NotImplementedException();
        }
    }
}

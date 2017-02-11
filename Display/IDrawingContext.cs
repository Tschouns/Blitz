//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using System.Collections.Generic;
    using Geometry.Elements;

    /// <summary>
    /// Provides an interface for drawing a frame.
    /// </summary>
    public interface IDrawingContext
    {
        /// <summary>
        /// Draws a line.
        /// </summary>
        void DrawLine(Point point1, Point point2, System.Drawing.Color color, float strokeWidth);

        /// <summary>
        /// Draws a polygon, defined by the specified set of points. The last point is connected back
        /// to the first by a line segment.
        /// </summary>
        void DrawPolygon(IEnumerable<Point> points, System.Drawing.Color color, float strokeWidth);

        /// <summary>
        /// Draws a cicle, defined by the center and its radius.
        /// </summary>
        void DrawCircle(Point center, double radius, System.Drawing.Color color, float strokeWidth);
    }
}

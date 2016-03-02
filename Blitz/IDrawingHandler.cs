//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using Geometry;
    
    /// <summary>
    /// Draws stuff.
    /// </summary>
    public interface IDrawingHandler
    {
        /// <summary>
        /// Draws a dot at the specified point.
        /// </summary>
        void DrawDot(Point point);

        /// <summary>
        /// Draws a line segment, from the first specified point to the second specified point.
        /// </summary>
        void DrawLineSegment(Point point1, Point point2);

        /// <summary>
        /// Draws a path, defined by the specified set of points.
        /// </summary>
        void DrawPath(IEnumerable<Point> points);
    }
}

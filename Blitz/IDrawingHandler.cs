//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using Geometry;
    using Geometry.Elements;

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
        /// Draws a line segment.
        /// </summary>
        void DrawLineSegment(Line line);

        /// <summary>
        /// Draws a path, defined by the specified set of points.
        /// </summary>
        void DrawPath(IEnumerable<Point> points);
    }
}

﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using Geometry;
    using Geometry.Elements;
    using System.Windows.Media;

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
        /// Draws a line segment. Uses the specified brush.
        /// </summary>
        void DrawLineSegment(Line line, Brush brush);

        /// <summary>
        /// Draws a path, defined by the specified set of points. The path starts with first point
        /// and ends with the last point.
        /// </summary>
        void DrawPath(IEnumerable<Point> points);

        /// <summary>
        /// Draws a path, defined by the specified set of points. The path starts with first point
        /// and ends with the last point. Uses the specified brush.
        /// </summary>
        void DrawPath(IEnumerable<Point> points, Brush brush);

        /// <summary>
        /// Draws a polygon, defined by the specified set of points. The last point is connected back
        /// to the first by a line segment.
        /// </summary>
        void DrawPolygon(IEnumerable<Point> points);

        /// <summary>
        /// Draws a polygon, defined by the specified set of points. The last point is connected back
        /// to the first by a line segment. Uses the specified brush.
        /// </summary>
        void DrawPolygon(IEnumerable<Point> points, Brush brush);
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using System.Collections.Generic;
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to perform different point transformations.
    /// </summary>
    public interface IPointTransformationHelper
    {
        /// <summary>
        /// Rotates the specified point, around the specified origin, by the specified angle (in radians).
        /// </summary>
        Point RotatePoint(Point origin, double angle, Point point);

        /// <summary>
        /// Rotates all the specified points, around the specified origin, by the specified angle (in radians).
        /// </summary>
        IEnumerable<Point> RotatePoints(Point origin, double angle, Point[] points);
    }
}

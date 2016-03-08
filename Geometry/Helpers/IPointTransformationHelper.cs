//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to perform different point transformations.
    /// </summary>
    public interface IPointTransformationHelper
    {
        /// <summary>
        /// Translates the specified point by a specified offset.
        /// </summary>
        Point TranslatePoint(Vector2 offset, Point point);

        /// <summary>
        /// Translates the specified points by a specified offset.
        /// </summary>
        Point[] TranslatePoints(Vector2 offset, Point[] points);

        /// <summary>
        /// Rotates the specified point, around the specified origin, by the specified angle (in radians).
        /// </summary>
        Point RotatePoint(Point origin, double angle, Point point);

        /// <summary>
        /// Rotates all the specified points, around the specified origin, by the specified angle (in radians).
        /// </summary>
        Point[] RotatePoints(Point origin, double angle, Point[] points);
    }
}

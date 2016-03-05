//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Base.Results;
    using Elements;

    /// <summary>
    /// Provides methods to calculate intersections between lines or line segments.
    /// </summary>
    public interface ILineIntersectionHelper
    {
        /// <summary>
        /// Gets a value indicating whether the lines (or line segments) A and be are parallel.
        /// </summary>
        bool AreLinesParallel(Line lineA, Line lineB);

        /// <summary>
        /// Gets the point where the infinite lines A and B intersect, or <c>null</c> if the
        /// lines do not intersect at all, i.e. are parallel.
        /// </summary>
        Point? GetLineIntersection(Line lineA, Line lineB);

        /// <summary>
        /// Gets the point where the line segments A and B intersect, or <c>null</c> if the
        /// line segments do not intersect.
        /// </summary>
        Point? GetLineSegmentIntersection(Line lineSegmentA, Line lineSegmentB);
    }
}

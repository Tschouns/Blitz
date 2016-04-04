//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Elements;

    /// <summary>
    /// Provides methods to calculate the distance between lines and points.
    /// </summary>
    public interface ILineCalculationHelper
    {
        /// <summary>
        /// The the point where the specified line intersects with its perpendicular through
        /// the origin.
        /// </summary>
        Point GetIntersectionWithPerpendicularThroughOrigin(Line line);

        /// <summary>
        /// The the point where the specified line intersects with its perpendicular through
        /// a specified point.
        /// </summary>
        Point GetIntersectionWithPerpendicularThroughPoint(Line line, Point point);
    }
}

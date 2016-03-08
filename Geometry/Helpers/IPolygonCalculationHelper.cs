//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to calculate different properties of polygons.
    /// </summary>
    public interface IPolygonCalculationHelper
    {
        /// <summary>
        /// Calculates the area of the specified polygon.
        /// </summary>
        double CalculateArea(Polygon polygon);

        /// <summary>
        /// Determines the centroid of the specified polygon. Does not work for non-simple polygons!
        /// </summary>
        Point DetermineCentroid(Polygon polygon);

        /// <summary>
        /// Determines whether the specified polygon is a non-simple polygon, i.e. has intersecting segments.
        /// </summary>
        bool IsNonsimplePolygon(Polygon polygon);
    }
}

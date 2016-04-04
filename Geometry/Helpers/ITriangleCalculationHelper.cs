//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Elements;
    using Result;

    /// <summary>
    /// Provides methods to perform calculations concerning triangles.
    /// </summary>
    public interface ITriangleCalculationHelper
    {
        /// <summary>
        /// Determines whether the specified point is within the specified triangle
        /// </summary>
        bool IsPointWithinTriangle(Triangle triangle, Point point);

        /// <summary>
        /// Calculates the barycentric coordinates of the specified point with respect to
        /// the specified triangle.
        /// </summary>
        BarycentricCoordinates CalculateBarycentricCoordinatesOfPoint(Triangle triangle, Point point);
    }
}

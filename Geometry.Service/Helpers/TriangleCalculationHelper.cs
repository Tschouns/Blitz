//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Helpers
{
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
    using Geometry.Helpers;
    using Geometry.Helpers.Result;

    /// <summary>
    /// See <see cref="ITriangleCalculationHelper"/>.
    /// </summary>
    public class TriangleCalculationHelper : ITriangleCalculationHelper
    {
        /// <summary>
        /// See <see cref="ITriangleCalculationHelper.IsPointWithinTriangle"/>.
        /// </summary>
        public bool IsPointWithinTriangle(Triangle triangle, Point point)
        {
            Checks.AssertNotNull(triangle, nameof(triangle));

            var barycentricCoordinates = this.CalculateBarycentricCoordinatesOfPoint(triangle, point);

            return barycentricCoordinates.U >= 0.0 &&
                   barycentricCoordinates.V >= 0.0 &&
                   barycentricCoordinates.W >= 0.0;
        }

        /// <summary>
        /// See <see cref="ITriangleCalculationHelper.CalculateBarycentricCoordinatesOfPoint"/>.
        /// Based on <see cref="http://gamedev.stackexchange.com/questions/23743/whats-the-most-efficient-way-to-find-barycentric-coordinates"/>.
        /// </summary>
        public BarycentricCoordinates CalculateBarycentricCoordinatesOfPoint(Triangle triangle, Point point)
        {
            Checks.AssertNotNull(triangle, nameof(triangle));

            // Compute barycentric coordinates(u, v, w) for point p with respect to triangle (a, b, c).
            var vector0 = triangle.B.GetOffsetFrom(triangle.A);
            var vector1 = triangle.C.GetOffsetFrom(triangle.A);
            var vector2 = point.GetOffsetFrom(triangle.A);

            var d00 = vector0.Dot(vector0);
            var d01 = vector0.Dot(vector1);
            var d11 = vector1.Dot(vector1);
            var d20 = vector2.Dot(vector0);
            var d21 = vector2.Dot(vector1);

            var denominatorInv = 1.0 / ((d00 * d11) - (d01 * d01));

            var v = ((d11 * d20) - (d01 * d21)) * denominatorInv;
            var w = ((d00 * d21) - (d01 * d20)) * denominatorInv;
            var u = 1.0 - v - w;

            return new BarycentricCoordinates(u, v, w);
        }
    }
}

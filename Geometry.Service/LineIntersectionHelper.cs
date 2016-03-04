//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service
{
    using System;
    using Base.Results;
    using Base.RuntimeChecks;

    /// <summary>
    /// See <see cref="ILineIntersectionHelper"/>.
    /// </summary>
    public class LineIntersectionHelper : ILineIntersectionHelper
    {
        /// <summary>
        /// See <see cref="ILineIntersectionHelper.GetLineIntersection"/>.
        /// </summary>
        public NullableResult<Point> GetLineIntersection(Line lineA, Line lineB)
        {
            Checks.AssertNotNull(lineA, nameof(lineA));
            Checks.AssertNotNull(lineB, nameof(lineB));

            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ILineIntersectionHelper.GetLineSegmentIntersection"/>.
        /// </summary>
        public NullableResult<Point> GetLineSegmentIntersection(Line lineSegmentA, Line lineSegmentB)
        {
            Checks.AssertNotNull(lineSegmentA, nameof(lineSegmentA));
            Checks.AssertNotNull(lineSegmentB, nameof(lineSegmentB));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the denominator for <c>ua</c> or <c>ub</c>, respectively. The algebra behind this is
        /// explained in the following articles:
        /// - <see cref="http://devmag.org.za/2009/04/13/basic-collision-detection-in-2d-part-1/"/>.
        /// - <see cref="http://devmag.org.za/2009/04/17/basic-collision-detection-in-2d-part-2/"/>
        /// </summary>
        private static double CalculateDenominatorOfUaOrUb(Point a1, Point a2, Point b1, Point b2)
        {
            double denominator = ((b2.Y - b1.Y) * (a2.X - a1.X)) - ((b2.X - b1.X) * (a2.Y - a1.Y));

            return denominator;
        }
    }
}

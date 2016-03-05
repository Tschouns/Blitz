//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service.Helpers
{
    using Base.RuntimeChecks;
    using Elements;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="ILineIntersectionHelper"/>.
    /// </summary>
    public class LineIntersectionHelper : ILineIntersectionHelper
    {
        /// <summary>
        /// See <see cref="ILineIntersectionHelper.AreLinesParallel"/>.
        /// </summary>
        public bool AreLinesParallel(Line lineA, Line lineB)
        {
            double denominator = CalculateDenominatorOfUaOrUb(
                lineA.Point1,
                lineA.Point2,
                lineB.Point1,
                lineB.Point2);

            return denominator == 0;
        }

        /// <summary>
        /// See <see cref="ILineIntersectionHelper.GetLineIntersection"/>.
        /// </summary>
        public Point? GetLineIntersection(Line lineA, Line lineB)
        {
            Checks.AssertNotNull(lineA, nameof(lineA));
            Checks.AssertNotNull(lineB, nameof(lineB));

            double denominator = CalculateDenominatorOfUaOrUb(
                lineA.Point1,
                lineA.Point2,
                lineB.Point1,
                lineB.Point2);

            if (denominator == 0)
            {
                return null;
            }

            double numeratorOfUa = CalculateNumeratorOfUa(
                lineA.Point1,
                lineA.Point2,
                lineB.Point1,
                lineB.Point2);

            double ua = numeratorOfUa / denominator;

            // Calculate intersection point, based on line A and ua.
            var intersectionPoint = new Point(
                lineA.Point1.X + (ua * (lineA.Point2.X - lineA.Point1.X)),
                lineA.Point1.Y + (ua * (lineA.Point2.Y - lineA.Point1.Y)));

            return intersectionPoint;
        }

        /// <summary>
        /// See <see cref="ILineIntersectionHelper.GetLineSegmentIntersection"/>.
        /// </summary>
        public Point? GetLineSegmentIntersection(Line lineSegmentA, Line lineSegmentB)
        {
            Checks.AssertNotNull(lineSegmentA, nameof(lineSegmentA));
            Checks.AssertNotNull(lineSegmentB, nameof(lineSegmentB));

            double denominator = CalculateDenominatorOfUaOrUb(
                lineSegmentA.Point1,
                lineSegmentA.Point2,
                lineSegmentB.Point1,
                lineSegmentB.Point2);

            if (denominator == 0)
            {
                return null;
            }

            double numeratorOfUa = CalculateNumeratorOfUa(
                lineSegmentA.Point1,
                lineSegmentA.Point2,
                lineSegmentB.Point1,
                lineSegmentB.Point2);

            double ua = numeratorOfUa / denominator;

            // We need ub only to check whether the intersection is between the end points of each respective segment.
            double numeratorOfUb = CalculateNumeratorOfUb(
                lineSegmentA.Point1,
                lineSegmentA.Point2,
                lineSegmentB.Point1,
                lineSegmentB.Point2);

            double ub = numeratorOfUb / denominator;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1)
            {
                return null;
            }

            // Calculate intersection point, based on line A and ua (same as for infinity lines; ub is not needed).
            var intersectionPoint = new Point(
                lineSegmentA.Point1.X + (ua * (lineSegmentA.Point2.X - lineSegmentA.Point1.X)),
                lineSegmentA.Point1.Y + (ua * (lineSegmentA.Point2.Y - lineSegmentA.Point1.Y)));

            return intersectionPoint;
        }

        /// <summary>
        /// Calculates the denominator for <c>ua</c> or <c>ub</c>, respectively. The algebra behind this is
        /// explained in the following articles:
        /// - <see cref="http://devmag.org.za/2009/04/13/basic-collision-detection-in-2d-part-1/"/>
        /// - <see cref="http://devmag.org.za/2009/04/17/basic-collision-detection-in-2d-part-2/"/>
        /// </summary>
        private static double CalculateDenominatorOfUaOrUb(Point a1, Point a2, Point b1, Point b2)
        {
            double denominator = ((b2.Y - b1.Y) * (a2.X - a1.X)) - ((b2.X - b1.X) * (a2.Y - a1.Y));

            return denominator;
        }

        /// <summary>
        /// Calculates the numerator for <c>ua</c>.
        /// </summary>
        private static double CalculateNumeratorOfUa(Point a1, Point a2, Point b1, Point b2)
        {
            double numerator = ((b2.X - b1.X) * (a1.Y - b1.Y)) - ((b2.Y - b1.Y) * (a1.X - b1.X));

            return numerator;
        }

        /// <summary>
        /// Calculates the numerator for <c>ub</c>.
        /// </summary>
        private static double CalculateNumeratorOfUb(Point a1, Point a2, Point b1, Point b2)
        {
            double numerator = ((a2.X - a1.X) * (a1.Y - b1.Y)) - ((a2.Y - a1.Y) * (a1.X - b1.X));

            return numerator;
        }
    }
}

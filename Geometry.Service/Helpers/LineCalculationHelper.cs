//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Helpers
{
    using Base.RuntimeChecks;
    using Elements;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="ILineCalculationHelper"/>.
    /// </summary>
    public class LineCalculationHelper : ILineCalculationHelper
    {
        /// <summary>
        /// See <see cref="ILineCalculationHelper.GetIntersectionWithPerpendicularThroughOrigin"/>.
        /// </summary>
        public Point GetIntersectionWithPerpendicularThroughOrigin(Line line)
        {
            // TODO: Perhaps this can be done faster...
            return this.GetIntersectionWithPerpendicularThroughPoint(line, GeometryConstants.Origin);
        }

        /// <summary>
        /// See <see cref="ILineCalculationHelper.GetIntersectionWithPerpendicularThroughPoint"/>.
        /// Based on: <see cref="http://stackoverflow.com/questions/1811549/perpendicular-on-a-line-from-a-given-point"/>
        /// </summary>
        public Point GetIntersectionWithPerpendicularThroughPoint(Line line, Point point)
        {
            ArgumentChecks.AssertNotNull(line, nameof(line));

            //// k = ((y2 - y1) * (x3 - x1) - (x2 - x1) * (y3 - y1)) / ((y2 - y1) ^ 2 + (x2 - x1) ^ 2)
            //// x4 = x3 - k * (y2 - y1)
            //// y4 = y3 + k * (x2 - x1)

            var numerator =
                ((line.Point2.Y - line.Point1.Y) * (point.X - line.Point1.X)) -
                ((line.Point2.X - line.Point1.X) * (point.Y - line.Point1.Y));

            var denominator =
                ((line.Point2.Y - line.Point1.Y) * (line.Point2.Y - line.Point1.Y)) +
                ((line.Point2.X - line.Point1.X) * (line.Point2.X - line.Point1.X));

            var intermediateResult = numerator / denominator;

            var intersectionX = point.X - (intermediateResult * (line.Point2.Y - line.Point1.Y));
            var intersectionY = point.Y + (intermediateResult * (line.Point2.X - line.Point1.X));

            return new Point(intersectionX, intersectionY);
        }
    }
}

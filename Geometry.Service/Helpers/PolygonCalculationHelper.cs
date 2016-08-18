//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base.RuntimeChecks;
    using Elements;
    using Geometry.Helpers;
    
    /// <summary>
    /// See <see cref="IPolygonCalculationHelper"/>.
    /// </summary>
    public class PolygonCalculationHelper : IPolygonCalculationHelper
    {
        /// <summary>
        /// Used to check for intersections between polygon segment.
        /// </summary>
        private readonly ILineIntersectionHelper _lineIntersectionHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonCalculationHelper"/> class.
        /// </summary>
        public PolygonCalculationHelper(ILineIntersectionHelper lineIntersectionHelper)
        {
            Checks.AssertNotNull(lineIntersectionHelper, nameof(lineIntersectionHelper));

            this._lineIntersectionHelper = lineIntersectionHelper;
        }

        /// <summary>
        /// See <see cref="IPolygonCalculationHelper.CalculateArea"/>.
        /// </summary>
        public double CalculateArea(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var area = Math.Abs(this.CalculateAreaPossibleNegative(polygon));

            return area;
        }

        /// <summary>
        /// See <see cref="IPolygonCalculationHelper.DetermineCentroid"/>.
        /// </summary>
        public Point DetermineCentroid(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var numberOfCorners = polygon.Corners.Count();
            var corners = polygon.Corners.ToList();

            // We add the copy the first corner to the end, as this allows to
            // always acces the next corner in the following fashion: [i + 1]
            corners.Add(corners.First());
            
            double intermediateSumX = 0.0;
            double intermediateSumY = 0.0;

            for (var i = 0; i < numberOfCorners; i++)
            {
                double intermediateFactor =
                    (corners[i].X * corners[i + 1].Y) -
                    (corners[i + 1].X * corners[i].Y);

                intermediateSumX +=
                    (corners[i].X + corners[i + 1].X) *
                    intermediateFactor;

                intermediateSumY +=
                    (corners[i].Y + corners[i + 1].Y) *
                    intermediateFactor;
            }

            // If the corners of the polygon are arranged clockwise the area will be negative. We use this to directly
            // correct the sign of the centroid X and Y (which would otherwise also be wrong).
            double area = this.CalculateAreaPossibleNegative(polygon);

            double centroidX = intermediateSumX / (6 * area);
            double centroidY = intermediateSumY / (6 * area);

            var centroid = new Point(centroidX, centroidY);

            return centroid;
        }

        /// <summary>
        /// See <see cref="IPolygonCalculationHelper.IsNonsimplePolygon"/>.
        /// </summary>
        public bool IsNonsimplePolygon(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var segments = this.GetSegments(polygon);
            foreach (var currentSegment in segments)
            {
                // If any of the segments intersect with the current segment the polygon considered a "non-simple" polygon.
                if (segments.Any(aX => this.DoSegmentsIntersect(aX, currentSegment)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates the area. The formulas used in the calculation are described here: <c>https://de.wikipedia.org/wiki/Geometrischer_Schwerpunkt</c>.
        /// If the corners are arranged clockwise the area will turn out negative. This internal
        /// implementation ignores this allows for "negative areas", so the centroid calculation
        /// can make use of that that to correct the sign of the centroid X and Y.
        /// </summary>
        public double CalculateAreaPossibleNegative(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var numberOfCorners = polygon.Corners.Count();
            var corners = polygon.Corners.ToList();

            // We add the copy the first corner to the end, as this allows to
            // always acces the next corner in the following fashion: [i + 1]
            corners.Add(corners.First());

            double intermediateSum = 0.0;
            for (var i = 0; i < numberOfCorners; i++)
            {
                intermediateSum +=
                    (corners[i].X * corners[i + 1].Y) -
                    (corners[i + 1].X * corners[i].Y);
            }

            double area = intermediateSum / 2;

            return area;
        }

        /// <summary>
        /// Checks whether two specified segments intersect, other than in the end points.
        /// </summary>
        private bool DoSegmentsIntersect(Line segment1, Line segment2)
        {
            Checks.AssertNotNull(segment1, nameof(segment1));
            Checks.AssertNotNull(segment2, nameof(segment2));

            var intersection = this._lineIntersectionHelper.GetLineSegmentIntersection(segment1, segment2);

            if (intersection.HasValue &&
                !intersection.Value.Equals(segment1.Point1) &&
                !intersection.Value.Equals(segment1.Point2) &&
                !intersection.Value.Equals(segment2.Point1) &&
                !intersection.Value.Equals(segment2.Point2))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets all the segments of a polygon.
        /// </summary>
        private IEnumerable<Line> GetSegments(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            IList<Line> segments = new List<Line>();

            var lastCorner = polygon.Corners.Last();

            foreach (var currentCorner in polygon.Corners)
            {
                var segment = new Line(lastCorner, currentCorner);
                segments.Add(segment);

                lastCorner = currentCorner;
            }

            return segments;
        }
    }
}
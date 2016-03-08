//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service.Helpers
{
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
        private readonly ILineIntersectionHelper lineIntersectionHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonCalculationHelper"/> class.
        /// </summary>
        public PolygonCalculationHelper(ILineIntersectionHelper lineIntersectionHelper)
        {
            Checks.AssertNotNull(lineIntersectionHelper, nameof(lineIntersectionHelper));

            this.lineIntersectionHelper = lineIntersectionHelper;
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
        /// Checks whether two specified segments intersect, other than in the end points.
        /// </summary>
        private bool DoSegmentsIntersect(Line segment1, Line segment2)
        {
            Checks.AssertNotNull(segment1, nameof(segment1));
            Checks.AssertNotNull(segment2, nameof(segment2));

            var intersection = this.lineIntersectionHelper.GetLineSegmentIntersection(segment1, segment2);

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

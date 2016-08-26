//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Algorithms.SupportFunctions
{
    using System.Linq;
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
    using Geometry.Algorithms;
    using Geometry.Helpers;

    /// <summary>
    /// Implements <see cref="ISupportFunctions{TFigure}"/> for <see cref="Polygon"/>.
    /// </summary>
    public class PolygonSupportFunctions : ISupportFunctions<Polygon>
    {
        /// <summary>
        /// Used to get the point on a line closest to a specific position.
        /// </summary>
        private readonly ILineCalculationHelper _lineCalculationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSupportFunctions"/> class.
        /// </summary>
        public PolygonSupportFunctions(ILineCalculationHelper lineCalculationHelper)
        {
            Checks.AssertNotNull(lineCalculationHelper, nameof(lineCalculationHelper));

            this._lineCalculationHelper = lineCalculationHelper;
        }

        /// <summary>
        /// See <see cref="ISupportFunctions{TFigure}.GetSupportPoint"/>.
        /// </summary>
        public Point GetSupportPoint(Polygon figure, Vector2 direction)
        {
            Checks.AssertNotNull(figure, nameof(figure));

            var point = new Point(0, 0);
            var maxDot = double.MinValue;

            foreach (var corner in figure.Corners)
            {
                var dot = corner.AsVector().Dot(direction);
                if (dot > maxDot)
                {
                    maxDot = dot;
                    point = corner;
                }
            }

            return point;
        }

        /// <summary>
        /// See <see cref="ISupportFunctions{TFigure}.GetFigureOutlinePointClosestToPosition(TFigure, Point)"/>.
        /// </summary>
        public Point GetFigureOutlinePointClosestToPosition(Polygon figure, Point position)
        {
            Checks.AssertNotNull(figure, nameof(figure));

            var cornersOrderedByDistance = figure.Corners
                .OrderBy(x => x.GetOffsetFrom(position).SquaredMagnitude())
                .ToList();

            var closestCorner = cornersOrderedByDistance[0];
            var secondClosestCorner = cornersOrderedByDistance[1];

            // If the offset of the closes corner and the line segment between the two closest corners are within 90 degrees,...
            if (position.GetOffsetFrom(closestCorner).IsDirectionWithin90Degrees(secondClosestCorner.GetOffsetFrom(closestCorner)))
            {
                // ... then the closest point is somewhere on the line segment.
                var closestPointOnLineSegment = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughPoint(
                    new Line(closestCorner, secondClosestCorner),
                    position);

                return closestPointOnLineSegment;
            }

            // Otherwise, the closest corner is the closest point.
            return closestCorner;
        }
    }
}

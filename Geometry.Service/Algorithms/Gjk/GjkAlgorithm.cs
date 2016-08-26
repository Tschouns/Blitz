//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Algorithms.Gjk
{
    using System;
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
    using Geometry.Algorithms;
    using Geometry.Algorithms.Gjk;
    using Geometry.Helpers;

    /// <summary>
    /// Generic implementation of <see cref="IGjkAlgorithm{TFigure1,TFigure2}"/>.
    /// </summary>
    /// <typeparam name="TFigure1">
    /// Type of figure 1
    /// </typeparam>
    /// <typeparam name="TFigure2">
    /// Type of figure 2
    /// </typeparam>
    public class GjkAlgorithm<TFigure1, TFigure2> : IGjkAlgorithm<TFigure1, TFigure2>
        where TFigure1 : class, IFigure
        where TFigure2 : class, IFigure
    {
        /// <summary>
        /// The support functions for <see cref="TFigure1"/>.
        /// </summary>
        private readonly ISupportFunctions<TFigure1> _figure1SupportFunctions;

        /// <summary>
        /// The support functions for <see cref="TFigure2"/>.
        /// </summary>
        private readonly ISupportFunctions<TFigure2> _figure2SupportFunctions;

        /// <summary>
        /// Used to get the search direction from a line towards the origin.
        /// </summary>
        private ILineCalculationHelper _lineCalculationHelper;

        /// <summary>
        /// Used to check whether the simplex (triangle) encloses the origin.
        /// </summary>
        private ITriangleCalculationHelper _triangleCalculationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GjkAlgorithm{TFigure1,TFigure2}"/> class.
        /// </summary>
        public GjkAlgorithm(
            ISupportFunctions<TFigure1> figure1SupportFunctions,
            ISupportFunctions<TFigure2> figure2SupportFunctions,
            ILineCalculationHelper lineCalculationHelper,
            ITriangleCalculationHelper triangleCalculationHelper)
        {
            Checks.AssertNotNull(figure1SupportFunctions, nameof(figure1SupportFunctions));
            Checks.AssertNotNull(figure2SupportFunctions, nameof(figure2SupportFunctions));
            Checks.AssertNotNull(lineCalculationHelper, nameof(lineCalculationHelper));
            Checks.AssertNotNull(triangleCalculationHelper, nameof(triangleCalculationHelper));

            this._figure1SupportFunctions = figure1SupportFunctions;
            this._figure2SupportFunctions = figure2SupportFunctions;
            this._lineCalculationHelper = lineCalculationHelper;
            this._triangleCalculationHelper = triangleCalculationHelper;
        }

        /// <summary>
        /// See <see cref="IGjkAlgorithm{TFigure1,TFigure2}.DoFiguresIntersect"/>.
        /// </summary>
        public FigureIntersectionResult DoFiguresIntersect(TFigure1 figure1, TFigure2 figure2)
        {
            Checks.AssertNotNull(figure1, nameof(figure1));
            Checks.AssertNotNull(figure2, nameof(figure2));

            var figureIntersectionResult = this.Gfk2DInternal(figure1, figure2);

            return figureIntersectionResult;
        }

        /// <summary>
        /// Internal implementation. TODO: extend return value so it can determine the distance and penetration depth etc...
        /// </summary>
        private FigureIntersectionResult Gfk2DInternal(TFigure1 figure1, TFigure2 figure2)
        {
            Checks.AssertNotNull(figure1, nameof(figure1));
            Checks.AssertNotNull(figure2, nameof(figure2));

            // We only need a simplex 2, i.e. a triangle, in 2D.
            IList<Point> simplexPoints = new List<Point>(3);

            // We choose any direction and get any point in the Minkowsi difference, using the support function.
            var direction = new Vector2(1, 0);
            var firstPoint = this.GetSupportPointInMinkowskyDifference(figure1, figure2, direction);
            simplexPoints.Add(firstPoint);

            FigureIntersectionResult result = null;

            while (result == null)
            {
                result = this.UpdateSimplexAndDirection(simplexPoints, ref direction);

                var nextSupportPoint = this.GetSupportPointInMinkowskyDifference(figure1, figure2, direction);
                if (nextSupportPoint.AsVector().Dot(direction) < 0)
                {
                    // There is no intersection.
                    return new FigureIntersectionResult(false, null);
                }

                simplexPoints.Add(nextSupportPoint);
            }

            return result;
        }

        /// <summary>
        /// Helper method. Updates the simplex and the direction.
        /// </summary>
        /// <returns>
        /// - <c>null</c>, while the simplex is not yet a triangle
        /// - otherwise, the result
        /// </returns>
        private FigureIntersectionResult UpdateSimplexAndDirection(IList<Point> simplexPoints, ref Vector2 direction)
        {
            switch (simplexPoints.Count)
            {
                // Point
                case 1:
                    // We just update the search direction from the point towards the origin.
                    direction = simplexPoints[0].AsVector().Invert();

                    return null;

                // Line segments
                case 2:
                    var directionFromNewPointBackward = simplexPoints[0].GetOffsetFrom(simplexPoints[1]);
                    var directionFromNewPointTowardsOrigin = simplexPoints[1].AsVector().Invert();

                    if (this.IsDirectionWithin90Degree(directionFromNewPointBackward, directionFromNewPointTowardsOrigin))
                    {
                        // The line is closest to the origin, so just update the search direction.
                        var line = new Line(simplexPoints[0], simplexPoints[1]);
                        var lineIntersectionWithPerpendicular = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughOrigin(line);
                        var directionFromLineTowardsOrigin = lineIntersectionWithPerpendicular.AsVector().Invert();
                        direction = directionFromLineTowardsOrigin;
                    }
                    else
                    {
                        // The new point is closest to the origin, so we remove the old point.
                        simplexPoints.RemoveAt(0);
                        direction = directionFromNewPointTowardsOrigin;
                    }

                    return null;
                
                // Triangle
                case 3:
                    var triangle = new Triangle(simplexPoints[0], simplexPoints[1], simplexPoints[2]);
                    var doFiguresIntersect = this._triangleCalculationHelper.IsPointWithinTriangle(triangle, GeometryConstants.Origin);

                    if (doFiguresIntersect)
                    {
                        return new FigureIntersectionResult(doFiguresIntersect, triangle);
                    }

                    // The last line segment must be closest to the origin... (is that so?)
                    var lastLineSegment = new Line(simplexPoints[1], simplexPoints[2]);
                    var lastLineSegmentintersectionWithPerpendicular = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughOrigin(lastLineSegment);
                    var directionFromLastLineSegmentTowardsOrigin = lastLineSegmentintersectionWithPerpendicular.AsVector().Invert();
                    direction = directionFromLastLineSegmentTowardsOrigin;

                    return null;
                
                // Tetragon (but we check only the "newer" half... which is a triangle again)
                case 4:
                    var newerTriangle = new Triangle(simplexPoints[1], simplexPoints[2], simplexPoints[3]);
                    doFiguresIntersect = this._triangleCalculationHelper.IsPointWithinTriangle(newerTriangle, GeometryConstants.Origin);

                    return new FigureIntersectionResult(doFiguresIntersect, new Polygon(simplexPoints[0], simplexPoints[1], simplexPoints[2], simplexPoints[3]));

                // Any other number of point...
                default:
                    throw new ArgumentException($"{nameof(simplexPoints)} contains an unexpected number of points: {simplexPoints.Count}");
            }
        }

        /// <summary>
        /// Determines whether the specified directions are within 90 degree (left or right) of one-another.
        /// </summary>
        private bool IsDirectionWithin90Degree(Vector2 direction1, Vector2 direction2)
        {
            var dotProduct = direction1.Dot(direction2);

            return dotProduct >= 0;
        }

        /// <summary>
        /// Gets the support point in the <c>Minkowski</c> difference between the specified figure 1 and figure 2, in
        /// the specified direction. 
        /// </summary>
        private Point GetSupportPointInMinkowskyDifference(TFigure1 figure1, TFigure2 figure2, Vector2 direction)
        {
            var supportPoint1 = this._figure1SupportFunctions.GetSupportPoint(figure1, direction);
            var supportPoint2Negative = this._figure2SupportFunctions.GetSupportPoint(figure2, direction.Invert());
            ////var supportPoint2Negative = this._figure2SupportFunctions.GetSupportPoint(figure2, direction);

            var supportPointInMinkowskyDifference = supportPoint1.SubtactVector(supportPoint2Negative.AsVector());

            return supportPointInMinkowskyDifference;
        }
    }
}

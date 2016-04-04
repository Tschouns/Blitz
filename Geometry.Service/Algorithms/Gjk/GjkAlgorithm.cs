//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service.Algorithms.Gjk
{
    using System;
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
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
        /// Initializes a new instance of the <see cref="GjkAlgorithm{TFigure1,TFigure2}"/> class.
        /// </summary>
        public GjkAlgorithm(
            ISupportFunctions<TFigure1> figure1SupportFunctions,
            ISupportFunctions<TFigure2> figure2SupportFunctions,
            ILineCalculationHelper lineCalculationHelper)
        {
            Checks.AssertNotNull(figure1SupportFunctions, nameof(figure1SupportFunctions));
            Checks.AssertNotNull(figure2SupportFunctions, nameof(figure2SupportFunctions));
            Checks.AssertNotNull(lineCalculationHelper, nameof(lineCalculationHelper));

            this._figure1SupportFunctions = figure1SupportFunctions;
            this._figure2SupportFunctions = figure2SupportFunctions;
            this._lineCalculationHelper = lineCalculationHelper;
        }

        /// <summary>
        /// See <see cref="IGjkAlgorithm{TFigure1,TFigure2}.DoFiguresIntersect"/>.
        /// </summary>
        public bool DoFiguresIntersect(TFigure1 figure1, TFigure2 figure2)
        {
            Checks.AssertNotNull(figure1, nameof(figure1));
            Checks.AssertNotNull(figure2, nameof(figure2));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Internal implementation. TODO: extend return value so it can determine the distance and penetration depth etc...
        /// </summary>
        private bool Gfk2dInternal(TFigure1 figure1, TFigure2 figure2)
        {
            Checks.AssertNotNull(figure1, nameof(figure1));
            Checks.AssertNotNull(figure2, nameof(figure2));

            // We only need a simplex 2, i.e. a triangle, in 2D.
            IList<Point> simplexPoints = new List<Point>(3);

            // We choose any direction and get first point in the Minkowsy difference.
            var direction = new Vector2(1, 0);
            var firstPoint = this.GetSupportPointInMinkowskyDifference(figure1, figure2, direction);
            simplexPoints.Add(firstPoint);
            
            while (true)
            {
                this.UpdateSimplexAndDirection(simplexPoints, ref direction);
                
                //// TODO: Check if simplex encloses the origin; means that there is an intersection.

                var nextSupportPoint = this.GetSupportPointInMinkowskyDifference(figure1, figure2, direction);
                if (nextSupportPoint.AsVector().Dot(direction) < 0)
                {
                    // There is no intersection.
                    return false;
                }

                simplexPoints.Add(nextSupportPoint);
            }
        }

        /// <summary>
        /// Helper method. Updates the simplex and the direction.
        /// </summary>
        private void UpdateSimplexAndDirection(IList<Point> simplexPoints, ref Vector2 direction)
        {
            switch (simplexPoints.Count)
            {
                // Point
                case 1:
                    // We just update the search direction from the point towards the origin.
                    direction = simplexPoints[0].AsVector().Invert();

                    break;

                // Line segments
                case 2:
                    var directionFromNewPointBackward = simplexPoints[0].GetOffsetFrom(simplexPoints[1]);
                    var directionFromNewPointTowardsOrigin = simplexPoints[1].AsVector().Invert();

                    if (this.IsDirectionWithin90Degree(directionFromNewPointBackward, directionFromNewPointTowardsOrigin))
                    {
                        // The line is closest to the origin, so just update the search direction.
                        var line = new Line(simplexPoints[0], simplexPoints[1]);
                        var intersectionWithPerpendicular = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughOrigin(line);
                        var directionFromLineTowardsOrigin = intersectionWithPerpendicular.AsVector().Invert();
                        direction = directionFromLineTowardsOrigin;
                    }
                    else
                    {
                        // The new point is closest to the origin, so we remove the old point.
                        simplexPoints.RemoveAt(0);
                        direction = directionFromNewPointTowardsOrigin;
                    }

                    break;
                
                // Triangle
                case 3:
                    //// TODO: Check if the origin is enclosed in the triangle.

                    break;

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
        /// Gets the support point in the <c>Minkowsky</c> difference between the specified figure 1 and figure 2, in
        /// the specified direction. 
        /// </summary>
        private Point GetSupportPointInMinkowskyDifference(TFigure1 figure1, TFigure2 figure2, Vector2 direction)
        {
            var supportPoint1 = this._figure1SupportFunctions.GetSupportPoint(figure1, direction);
            var supportPoint2Negative = this._figure2SupportFunctions.GetSupportPoint(figure2, direction.Invert());

            var supportPointInMinkowskyDifference = supportPoint1.SubtactVector(supportPoint2Negative.AsVector());

            return supportPointInMinkowskyDifference;
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Base.RuntimeChecks;
using Geometry.Elements;

namespace Geometry.Algorithms.Gjk
{
    /// <summary>
    /// Represents the result of a <see cref="IGjkAlgorithm{TFigure1, TFigure2}.DoFiguresIntersect(TFigure1, TFigure2)"/> call.
    /// </summary>
    public class FigureIntersectionResult
    {
        public FigureIntersectionResult(bool doFiguresIntersect, Polygon magicPolygonNullable)
        {
            ////Checks.AssertNotNull(magicTriangle, nameof(MagicTriangle));

            this.DoFiguresIntersect = doFiguresIntersect;
            this.MagicPolygonNullable = magicPolygonNullable;
        }

        /// <summary>
        /// Gets a value indicating whether the two figures intersect or not.
        /// </summary>
        public bool DoFiguresIntersect { get; }

        /// <summary>
        /// Gets the magic triangle which contains the origin if the figures intersect, or <c>null</c>, if the polygon was not even created.
        /// </summary>
        public Polygon MagicPolygonNullable { get; }
    }
}

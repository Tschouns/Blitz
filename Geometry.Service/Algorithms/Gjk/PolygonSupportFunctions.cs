//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Algorithms.Gjk
{
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
    using Geometry.Algorithms.Gjk;

    /// <summary>
    /// Implements <see cref="ISupportFunctions{TFigure}"/> for <see cref="Polygon"/>.
    /// </summary>
    public class PolygonSupportFunctions : ISupportFunctions<Polygon>
    {
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
    }
}

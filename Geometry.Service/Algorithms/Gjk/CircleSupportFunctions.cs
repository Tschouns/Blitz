//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Algorithms.Gjk
{
    using Base.RuntimeChecks;
    using Geometry.Algorithms.Gjk;
    using Geometry.Elements;
    using Geometry.Extensions;

    /// <summary>
    /// Implements <see cref="ISupportFunctions{TFigure}"/> for <see cref="Circle"/>.
    /// </summary>
    public class CircleSupportFunctions : ISupportFunctions<Circle>
    {
        /// <summary>
        /// See <see cref="ISupportFunctions{TFigure}.GetSupportPoint"/>.
        /// </summary>
        public Point GetSupportPoint(Circle figure, Vector2 direction)
        {
            Checks.AssertNotNull(figure, nameof(figure));

            var normalizedDirection = direction.Norm();
            var supportPointOffsetFromCenter = normalizedDirection.Multiply(figure.Radius);

            var supportPoint = figure.Center.AddVector(supportPointOffsetFromCenter);

            return supportPoint;
        }
    }
}

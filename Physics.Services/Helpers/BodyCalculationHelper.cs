//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Helpers
{
    using System;
    using System.Linq;
    using Base.RuntimeChecks;
    using Geometry;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="IBodyCalculationHelper"/>
    /// </summary>
    public class BodyCalculationHelper : IBodyCalculationHelper
    {
        /// <summary>
        /// Used to translate polygons.
        /// </summary>
        private readonly IPolygonTransformationHelper polygonTransformationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyCalculationHelper"/> class.
        /// </summary>
        public BodyCalculationHelper(IPolygonTransformationHelper polygonTransformationHelper)
        {
            ArgumentChecks.AssertNotNull(polygonTransformationHelper, nameof(polygonTransformationHelper));

            this.polygonTransformationHelper = polygonTransformationHelper;
        }

        /// <summary>
        /// See <see cref="IBodyCalculationHelper.CalculateMomentOfInertia"/>.
        /// </summary>
        public double CalculateMomentOfInertia(Polygon polygon, Point axis, double mass)
        {
            ArgumentChecks.AssertNotNull(polygon, nameof(polygon));

            // We translate the polygon, so that the axis is aligned with the origin (in order to make the formula work).
            var originOffset = GeometryConstants.Origin.GetOffsetFrom(axis);
            var translatedPolygon = this.polygonTransformationHelper.TranslatePolygon(originOffset, polygon);

            // We can now calculate the moment of inertia for a rotation about the origin.
            var momentOfInertia = this.CalculateMomentOfInertiaAboutOrigin(translatedPolygon, mass);

            return momentOfInertia;
        }

        /// <summary>
        /// See <see cref="IBodyCalculationHelper.CalculateMomentOfInertiaAboutOrigin"/>.
        /// The formula this calculation is based on can be found here: <c>https://en.wikipedia.org/wiki/List_of_moments_of_inertia</c>
        /// </summary>
        public double CalculateMomentOfInertiaAboutOrigin(Polygon polygon, double mass)
        {
            ArgumentChecks.AssertNotNull(polygon, nameof(polygon));

            var numberOfCorners = polygon.Corners.Count();
            var corners = polygon.Corners.ToList();

            // We add the copy the first corner to the end, as this allows to
            // always acces the next corner in the following fashion: [i + 1]
            corners.Add(corners.First());

            double intermediateNumerator = 0.0;
            double intermediateDenominator = 0.0;

            for (var i = 0; i < numberOfCorners; i++)
            {
                var a = corners[i];
                var b = corners[i + 1];

                intermediateNumerator += Math.Abs(b.Cross(a)) *
                                         (a.Dot(a) + a.Dot(b) + b.Dot(b));

                intermediateDenominator += Math.Abs(b.Cross(a));
            }

            double momentOfInertia =
                (mass / 6) * (intermediateNumerator / intermediateDenominator);

            return momentOfInertia;
        }

        /// <summary>
        /// See <see cref="IBodyCalculationHelper.CalculateTorque"/>.
        /// </summary>
        public double CalculateTorque(Vector2 force, Vector2 offset)
        {
            return offset.Cross(force);
        }
    }
}

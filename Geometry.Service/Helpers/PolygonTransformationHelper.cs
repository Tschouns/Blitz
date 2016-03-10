//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service.Helpers
{
    using System.Linq;
    using Base.RuntimeChecks;
    using Elements;
    using Extensions;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="IPolygonTransformationHelper"/>.
    /// </summary>
    public class PolygonTransformationHelper : IPolygonTransformationHelper
    {
        /// <summary>
        /// Used to transform the polygon corners.
        /// </summary>
        private readonly IPointTransformationHelper pointTransformationHelper;

        /// <summary>
        /// Used to calculate the centroid of a polygon.
        /// </summary>
        private readonly IPolygonCalculationHelper polygonCalculationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonTransformationHelper"/> class.
        /// </summary>
        public PolygonTransformationHelper(
            IPointTransformationHelper pointTransformationHelper,
            IPolygonCalculationHelper polygonCalculationHelper)
        {
            Checks.AssertNotNull(pointTransformationHelper, nameof(pointTransformationHelper));
            Checks.AssertNotNull(polygonCalculationHelper, nameof(polygonCalculationHelper));

            this.pointTransformationHelper = pointTransformationHelper;
            this.polygonCalculationHelper = polygonCalculationHelper;
        }

        /// <summary>
        /// See <see cref="IPolygonTransformationHelper.TranslatePolygon"/>.
        /// </summary>
        public Polygon TranslatePolygon(Vector2 offset, Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var newPolygonCorners = this.pointTransformationHelper.TranslatePoints(
                offset,
                polygon.Corners.ToArray());

            return new Polygon(newPolygonCorners);
        }

        /// <summary>
        /// See <see cref="IPolygonTransformationHelper.RotatePolygon"/>.
        /// </summary>
        public Polygon RotatePolygon(Point origin, double angle, Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var newPolygonCorners = this.pointTransformationHelper.RotatePoints(
                origin,
                angle,
                polygon.Corners.ToArray());

            return new Polygon(newPolygonCorners);
        }

        /// <summary>
        /// See <see cref="IPolygonTransformationHelper.CenterOnOrigin"/>.
        /// </summary>
        public Polygon CenterOnOrigin(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var centroid = this.polygonCalculationHelper.DetermineCentroid(polygon);

            // Get the origin's offset, relative to the centroid.
            var originOffset = GeometryConstants.Origin.GetOffsetFrom(centroid);
            var centeredPolygon = this.TranslatePolygon(originOffset, polygon);

            return centeredPolygon;
        }
    }
}

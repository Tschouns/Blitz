//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
    using Base.RuntimeChecks;
    using Geometry;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Geometry.Helpers;
    using Physics.Services.Helpers;

    /// <summary>
    /// See <see cref="IShapeFactory"/>.
    /// </summary>
    public class ShapeFactory : IShapeFactory
    {
        /// <summary>
        /// Used to calculate properties of a polygon, such as the area.
        /// </summary>
        private readonly IPolygonCalculationHelper _polygonCalculationHelper;

        /// <summary>
        /// Used to center and transform polygons.
        /// </summary>
        private readonly IPolygonTransformationHelper _polygonTransformationHelper;

        /// <summary>
        /// Used to calculate the moment of inertia.
        /// </summary>
        private readonly IBodyCalculationHelper _bodyCalculationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeFactory"/> class.
        /// </summary>
        public ShapeFactory(
            IPolygonTransformationHelper polygonTransformationHelper,
            IPolygonCalculationHelper polygonCalculationHelper,
            IBodyCalculationHelper bodyCalculationHelper)
        {
            ArgumentChecks.AssertNotNull(polygonTransformationHelper, nameof(polygonTransformationHelper));
            ArgumentChecks.AssertNotNull(polygonCalculationHelper, nameof(polygonCalculationHelper));
            ArgumentChecks.AssertNotNull(bodyCalculationHelper, nameof(bodyCalculationHelper));

            this._polygonTransformationHelper = polygonTransformationHelper;
            this._polygonCalculationHelper = polygonCalculationHelper;
            this._bodyCalculationHelper = bodyCalculationHelper;
        }

        /// <summary>
        /// See <see cref="IShapeFactory.CreateOriginalPolygonShape"/>.
        /// </summary>
        public IRigidShape<Polygon> CreateOriginalPolygonShape(Polygon polygon)
        {
            ArgumentChecks.AssertNotNull(polygon, nameof(polygon));

            var areaRepresentingTheVolume = this._polygonCalculationHelper.CalculateArea(polygon);
            var originCenteredPolygon = this._polygonTransformationHelper.CenterOnOrigin(polygon);

            return new RigidPolygonShape(
                this._polygonTransformationHelper,
                this._bodyCalculationHelper,
                areaRepresentingTheVolume,
                originCenteredPolygon);
        }
    }
}

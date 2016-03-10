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
    using Physics.Elements.Shape;

    /// <summary>
    /// See <see cref="IShapeFactory"/>.
    /// </summary>
    public class ShapeFactory : IShapeFactory
    {
        /// <summary>
        /// Used to calculate properties of a polygon, such as the area.
        /// </summary>
        private readonly IPolygonCalculationHelper polygonCalculationHelper;

        /// <summary>
        /// Used to center and transform polygons.
        /// </summary>
        private readonly IPolygonTransformationHelper polygonTransformationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeFactory"/> class.
        /// </summary>
        public ShapeFactory(
            IPolygonTransformationHelper polygonTransformationHelper,
            IPolygonCalculationHelper polygonCalculationHelper)
        {
            Checks.AssertNotNull(polygonTransformationHelper, nameof(polygonTransformationHelper));
            Checks.AssertNotNull(polygonCalculationHelper, nameof(polygonCalculationHelper));

            this.polygonTransformationHelper = polygonTransformationHelper;
            this.polygonCalculationHelper = polygonCalculationHelper;
        }

        /// <summary>
        /// See <see cref="IShapeFactory.CreateOriginalPolygonShape"/>.
        /// </summary>
        public IPolygonShape CreateOriginalPolygonShape(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var areaRepresentingTheVolume = this.polygonCalculationHelper.CalculateArea(polygon);
            var originCenteredPolygon = this.polygonTransformationHelper.CenterOnOrigin(polygon);

            return new PolygonShape(
                areaRepresentingTheVolume,
                GeometryConstants.Origin,              
                originCenteredPolygon);
        }

        /// <summary>
        /// See <see cref="IShapeFactory.CreateTransformedPolygonShape"/>.
        /// </summary>
        public IPolygonShape CreateTransformedPolygonShape(
            IPolygonShape originalPolygonShape,
            Point targetPosition,
            double orientation)
        {
            Checks.AssertNotNull(originalPolygonShape, nameof(originalPolygonShape));

            // Rotate around the center of mass.
            var rotatedPolygon = this.polygonTransformationHelper.RotatePolygon(
                originalPolygonShape.CenterOfMass,
                orientation,
                originalPolygonShape.Polygon);

            // Translate by the offset of the target position, relative to the center of mass which is its "original position".
            var targetPositionOffset = targetPosition.GetOffsetFrom(originalPolygonShape.CenterOfMass);
            var translatedAndRotatedPolygon = this.polygonTransformationHelper.TranslatePolygon(
                targetPositionOffset,
                rotatedPolygon);

            // Create new instance. The volume has not changed.
            var transformedPolygonShape = new PolygonShape(
                originalPolygonShape.Volume,
                targetPosition,
                translatedAndRotatedPolygon);

            return transformedPolygonShape;
        }
    }
}

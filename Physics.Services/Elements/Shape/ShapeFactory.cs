//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Helpers;
    using Physics.Elements.Shape;

    /// <summary>
    /// See <see cref="IShapeFactory"/>.
    /// </summary>
    public class ShapeFactory : IShapeFactory
    {
        /// <summary>
        /// Used to center and transform polygons.
        /// </summary>
        private readonly IPolygonTransformationHelper polygonTransformationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeFactory"/> class.
        /// </summary>
        public ShapeFactory(IPolygonTransformationHelper polygonTransformationHelper)
        {
            Checks.AssertNotNull(polygonTransformationHelper, nameof(polygonTransformationHelper));

            this.polygonTransformationHelper = polygonTransformationHelper;
        }

        /// <summary>
        /// See <see cref="IShapeFactory.CreateOriginalPolygonShape"/>.
        /// </summary>
        public IPolygonShape CreateOriginalPolygonShape(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            var centeredPolygon = this.polygonTransformationHelper.CenterOnOrigin(polygon);

            return new PolygonShape(centeredPolygon);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Creates shapes.
    /// </summary>
    public interface IShapeFactory
    {
        /// <summary>
        /// Creates a <see cref="IRigidShape{Polygon}"/> based on the specified <see cref="Polygon"/>.
        /// The centroid of the resulting <see cref="IShape{TGeometricFigure}"/> will be aligned with
        /// the origin.
        /// </summary>
        IRigidShape<Polygon> CreateOriginalPolygonShape(Polygon polygon);
    }
}

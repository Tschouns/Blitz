//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements.Shape
{
    using Geometry.Elements;

    /// <summary>
    /// Creates shapes.
    /// </summary>
    public interface IShapeFactory
    {
        /// <summary>
        /// Creates a <see cref="IPolygonShape"/> based on the specified <see cref="Polygon"/>.
        /// The centroid of the resulting <see cref="IPolygonShape"/> will be aligned with
        /// the origin.
        /// </summary>
        IPolygonShape CreateOriginalPolygonShape(Polygon polygon);
    }
}

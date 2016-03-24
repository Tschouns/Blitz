//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements.Shape
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a polygon shape.
    /// </summary>
    public interface IPolygonShape : IShape
    {
        /// <summary>
        /// Gets the polygon.
        /// </summary>
        Polygon Polygon { get; }
    }
}

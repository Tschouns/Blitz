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
    /// TODO:
    /// This shall be used to represent the "original" as well as the fully "transformed"
    /// (moved, rotated, perhaps deformed) shape of a body.
    /// Additional properties might be provided: area, centroid (=position),...
    /// </summary>
    public interface IPolygonShape : IShape
    {
        /// <summary>
        /// Gets the polygon.
        /// </summary>
        Polygon Polygon { get; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements.Shape
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a shape, as a property of a "physical body".
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets the volume of the "physical body" (which is of course the area... because, you know, 2D).
        /// </summary>
        double Volume { get; }

        /// <summary>
        /// Gets the center of mass.
        /// </summary>
        Point CenterOfMass { get; }
    }
}

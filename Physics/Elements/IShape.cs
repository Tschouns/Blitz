//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a shape, as a property of a "physical body".
    /// </summary>
    /// <typeparam name="TFigure">
    /// Type of the actual geometric figure which represents the shape of the body
    /// </typeparam>
    public interface IShape<TFigure>
        where TFigure : class, IFigure
    {
        /// <summary>
        /// Gets the volume of the "physical body" (which is of course the area... because, you know, 2D).
        /// </summary>
        double Volume { get; }

        /// <summary>
        /// Gets the original shape of the body, with its center of mass identical
        /// to the origin.
        /// The original shape will not change over the lifecycle of a body.
        /// </summary>
        TFigure Original { get; }

        /// <summary>
        /// Gets the current shape of the body, fully transformed based on the body's
        /// current position and orientation in space.
        /// </summary>
        TFigure Current { get; }
    }
}

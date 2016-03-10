//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Physics.Elements.Shape;

    /// <summary>
    /// Represents a body in the "physical world".
    /// </summary>
    /// <typeparam name="TShape">
    /// The type of shape that defines the body
    /// </typeparam>
    public interface IBody<TShape> : IPhysicalObject
        where TShape : IShape
    {
        /// <summary>
        /// Gets the inertia.
        /// </summary>
        double Inertia { get; }

        /// <summary>
        /// Gets the original shape of the body, with its center of mass identical
        /// to the origin.
        /// The original shape instance will not change over the lifecycle of
        /// the <see cref="IBody{TShape}"/>.
        /// </summary>
        TShape OriginalShape { get; }

        /// <summary>
        /// Gets the current state of the body.
        /// </summary>
        BodyState CurrentState { get; }

        /// <summary>
        /// Gets the current shape of the body, fully transformed based on its current
        /// position and orientation in space.
        /// The current shape instance is created when this method is called.
        /// </summary>
        TShape GetCurrentShape();
    }
}

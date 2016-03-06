//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    /// <summary>
    /// Represents a rigid body in the "physical world".
    /// </summary>
    public interface IRigidBody
    {
        /// <summary>
        /// Gets the mass.
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// Gets the shape.
        /// </summary>
        IShape Shape { get; }

        /// <summary>
        /// Gets the inertia.
        /// </summary>
        double Inertia { get; }

        /// <summary>
        /// Gets the current state of the body.
        /// </summary>
        RigidBodyState CurrentState { get; }
    }
}

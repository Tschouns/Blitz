//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    /// <summary>
    /// Represents a particle in the "physical world".
    /// </summary>
    public interface IParticle
    {
        /// <summary>
        /// Gets the mass.
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// Gets the current state of the particle.
        /// </summary>
        ParticleState CurrentState { get; }
    }
}

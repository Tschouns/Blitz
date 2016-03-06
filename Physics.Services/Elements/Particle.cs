//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Implementation of <see cref="IParticle"/>.
    /// </summary>
    public class Particle : IParticle
    {
        /// <summary>
        /// Stores the mass of this particle.
        /// </summary>
        private readonly double mass;

        /// <summary>
        /// Stores the current state of this particle.
        /// </summary>
        private ParticleState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle(double mass, Point initialPosition, Vector2 initialVelocity)
        {
            this.mass = mass;
            this.state = new ParticleState()
            {
                Position = initialPosition,
                Velocity = initialVelocity
            };
        }

        /// <summary>
        /// Gets... see <see cref="IParticle.Mass"/>.
        /// </summary>
        public double Mass => this.mass;

        /// <summary>
        /// Gets... see <see cref="IParticle.CurrentState"/>.
        /// </summary>
        public ParticleState CurrentState => this.state;
    }
}

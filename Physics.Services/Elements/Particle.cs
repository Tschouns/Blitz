//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Physics.Elements;

    /// <summary>
    /// Implementation of <see cref="IParticle"/>.
    /// </summary>
    public class Particle : IParticle, IPhysicalObject
    {
        /// <summary>
        /// Stores the mass of this particle.
        /// </summary>
        private readonly double mass;

        /// <summary>
        /// Stores the currently applied force.
        /// </summary>
        private Vector2 appliedForce;

        /// <summary>
        /// Stores the current state of this particle.
        /// </summary>
        private ParticleState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle(double mass, Point initialPosition, Vector2 initialVelocity)
        {
            Checks.AssertIsPositive(mass, nameof(mass));

            this.mass = mass;
            this.appliedForce = new Vector2();
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

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void AddForce(Vector2 force)
        {
            this.appliedForce.AddVector(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void Step(double time)
        {
            // TODO: refactor, extract logic!!
            var acceleration = new Vector2(
                this.appliedForce.X / this.mass,
                this.appliedForce.Y / this.mass);

            this.state.Velocity = new Vector2(
                this.state.Velocity.X + (acceleration.X * time),
                this.state.Velocity.Y + (acceleration.Y * time));

            this.state.Position = new Point(
                this.state.Position.X + (this.state.Velocity.X * time),
                this.state.Position.Y + (this.state.Velocity.Y * time));
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ResetAppliedForce"/>.
        /// </summary>
        public void ResetAppliedForce()
        {
            this.appliedForce = new Vector2();
        }
    }
}

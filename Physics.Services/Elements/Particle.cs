//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Helpers;
    using Physics.Elements;

    /// <summary>
    /// Implementation of <see cref="IParticle"/>.
    /// </summary>
    public class Particle : IParticle
    {
        /// <summary>
        /// Used to calculate acceleration, velocity and position.
        /// </summary>
        private readonly ICalculationHelper helper;

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
        public Particle(
            ICalculationHelper calculationHelper,
            double mass,
            Point initialPosition,
            Vector2 initialVelocity)
        {
            Checks.AssertNotNull(calculationHelper, nameof(calculationHelper));
            Checks.AssertIsStrictPositive(mass, nameof(mass));

            this.helper = calculationHelper;
            this.Mass = mass;
            this.appliedForce = new Vector2();
            this.state = new ParticleState
            {
                Position = initialPosition,
                Velocity = initialVelocity
            };
        }

        /// <summary>
        /// Gets... see <see cref="IPhysicalObject.Mass"/>.
        /// </summary>
        public double Mass { get; }

        /// <summary>
        /// Gets... see <see cref="IParticle.CurrentState"/>.
        /// </summary>
        public ParticleState CurrentState => this.state;

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void AddForce(Vector2 force)
        {
            this.appliedForce = this.appliedForce.AddVector(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForceAtOffset"/>.
        /// </summary>
        public void AddForceAtOffset(Vector2 force, Vector2 offset)
        {
            // A particle behaves the same way, no matter what the offset is.
            this.AddForce(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void Step(double time)
        {
            var acceleration = this.helper.CalculateAcceleration(this.appliedForce, this.Mass);
            this.state.Velocity = this.helper.CalculateVelocity(this.state.Velocity, acceleration, time);
            this.state.Position = this.helper.CalculatePosition(this.state.Position, this.state.Velocity, time);
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

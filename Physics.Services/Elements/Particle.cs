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
        private readonly IIsaacNewtonHelper _helper;

        /// <summary>
        /// Stores the currently applied force.
        /// </summary>
        private Vector2 _appliedForce;

        /// <summary>
        /// Stores the current state of this particle.
        /// </summary>
        private ParticleState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle(
            IIsaacNewtonHelper helper,
            double mass,
            Point initialPosition,
            Vector2 initialVelocity)
        {
            Checks.AssertNotNull(helper, nameof(helper));
            Checks.AssertIsStrictPositive(mass, nameof(mass));

            this._helper = helper;
            this.Mass = mass;
            this._appliedForce = new Vector2();
            this._state = new ParticleState
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
        public ParticleState CurrentState => this._state;

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void AddForce(Vector2 force)
        {
            this._appliedForce = this._appliedForce.AddVector(force);
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
            var acceleration = this._helper.CalculateAcceleration(
                this._appliedForce,
                this.Mass);

            this._state.Velocity = this._helper.CalculateVelocity(
                this._state.Velocity,
                acceleration,
                time);

            this._state.Position = this._helper.CalculatePosition(
                this._state.Position,
                this._state.Velocity,
                time);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ResetAppliedForce"/>.
        /// </summary>
        public void ResetAppliedForce()
        {
            this._appliedForce = new Vector2();
        }
    }
}

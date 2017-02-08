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
        /// Stores the currently applied acceleration.
        /// </summary>
        private Vector2 _appliedAcceleration;

        /// <summary>
        /// Stores the velocity which shall override the current velocity.
        /// </summary>
        private Vector2? _overrideVelocity;

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
            ParticleState initalState)
        {
            Checks.AssertNotNull(helper, nameof(helper));
            Checks.AssertIsStrictPositive(mass, nameof(mass));

            this._helper = helper;
            this.Mass = mass;

            this._appliedForce = new Vector2();
            this._appliedAcceleration = new Vector2();
            this._overrideVelocity = null;
            
            this._state = initalState;
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
        /// See <see cref="IPhysicalObject.ApplyForce(Vector2)"/>.
        /// </summary>
        public void ApplyForce(Vector2 force)
        {
            this._appliedForce = this._appliedForce.AddVector(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ApplyForceAtOffset(Vector2, Vector2)"/>.
        /// </summary>
        public void ApplyForceAtOffset(Vector2 force, Vector2 offset)
        {
            // A particle behaves the same way, no matter what the offset is.
            this.ApplyForce(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ApplyForceAtPointInSpace(Vector2, Point)"/>.
        /// </summary>
        public void ApplyForceAtPointInSpace(Vector2 force, Point pointInSpace)
        {
            // A particle behaves the same way, no matter what the offset is.
            this.ApplyForce(force);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ApplyAcceleration(Vector2)"/>.
        /// </summary>
        public void ApplyAcceleration(Vector2 acceleration)
        {
            this._appliedAcceleration = this._appliedAcceleration.AddVector(acceleration);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.SetVelocity(Vector2)"/>.
        /// </summary>
        public void SetVelocity(Vector2 velocity)
        {
            this._overrideVelocity = this._overrideVelocity.HasValue ? this._overrideVelocity.Value.AddVector(velocity) : velocity;
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.SetPosition"/>
        /// </summary>
        public void SetPosition(Point position)
        {
            this._state.Position = position;
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.AddForce"/>.
        /// </summary>
        public void Step(double time)
        {
            if (this._overrideVelocity.HasValue)
            {
                this._state.Velocity = this._overrideVelocity.Value;
            }
            else
            {
                var acceleration = this._helper.CalculateAcceleration(
                        this._appliedForce,
                        this.Mass)
                    .AddVector(this._appliedAcceleration);

                this._state.Velocity = this._helper.CalculateVelocity(
                        this._state.Velocity,
                        acceleration,
                        time);

                this._state.Position = this._helper.CalculatePosition(
                    this._state.Position,
                    this._state.Velocity,
                    time);
            }
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ResetAppliedPhysicalQuantities"/>.
        /// </summary>
        public void ResetAppliedPhysicalQuantities()
        {
            this._appliedForce = new Vector2();
            this._appliedAcceleration = new Vector2();

            this._overrideVelocity = null;
        }
    }
}

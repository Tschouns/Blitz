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
    using Physics.Elements.Shape;

    /// <summary>
    /// Implementation of <see cref="IBody{IPolygonShape}"/>. Implements the behavior of a "rigid body".
    /// </summary>
    public class RigidBody : IBody<IPolygonShape>
    {
        /// <summary>
        /// Used to create the <see cref="IPolygonShape"/>, based on a <see cref="Polygon"/>.
        /// TODO: Check whether it's really needed as a member, or whether the creation of
        /// the shape shall be done in this class at all.
        /// </summary>
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Used to calculate the moment of inertia.
        /// </summary>
        private readonly IBodyCalculationHelper _bodyCalculationHelper;

        /// <summary>
        /// Used to calculate the acceleration, velocity and position.
        /// </summary>
        private readonly IIsaacNewtonHelper _isaacNewtonHelper;

        /// <summary>
        /// Stores the currently applied force.
        /// </summary>
        private Vector2 _appliedForce;

        /// <summary>
        /// Stores the currently applied torque.
        /// </summary>
        private double _appliedTorque;

        /// <summary>
        /// Stores the current state of this rigid body.
        /// </summary>
        private BodyState _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="RigidBody"/> class.
        /// </summary>
        public RigidBody(
            IShapeFactory shapeFactory,
            IBodyCalculationHelper bodyCalculationHelper,
            IIsaacNewtonHelper isaacNewtonHelper,
            double mass,
            Polygon polygon,
            BodyState initialBodyState)
        {
            Checks.AssertNotNull(shapeFactory, nameof(shapeFactory));
            Checks.AssertNotNull(bodyCalculationHelper, nameof(bodyCalculationHelper));
            Checks.AssertNotNull(isaacNewtonHelper, nameof(isaacNewtonHelper));
            Checks.AssertIsStrictPositive(mass, nameof(mass));
            Checks.AssertNotNull(polygon, nameof(polygon));

            // Helpers
            this._shapeFactory = shapeFactory;
            this._bodyCalculationHelper = bodyCalculationHelper;
            this._isaacNewtonHelper = isaacNewtonHelper;

            // Static properties
            this.Mass = mass;

            // (The "original shape" is created once, and won't change over the lifecycle of the class.)
            this.OriginalShape = this._shapeFactory.CreateOriginalPolygonShape(polygon);
            this.Inertia = this._bodyCalculationHelper.CalculateMomentOfInertiaAboutOrigin(
                this.OriginalShape.Polygon,
                this.Mass);

            // Dynamic properties
            this._appliedForce = new Vector2();
            this._appliedTorque = 0;
            this._state = initialBodyState;
        }

        /// <summary>
        /// Gets... see <see cref="IPhysicalObject.Mass"/>.
        /// </summary>
        public double Mass { get; }

        /// <summary>
        /// Gets... see <see cref="IBody{TShape}.Inertia"/>.
        /// </summary>
        public double Inertia { get; }

        /// <summary>
        /// Gets... see <see cref="IBody{TShape}.OriginalShape"/>.
        /// </summary>
        public IPolygonShape OriginalShape { get; }

        /// <summary>
        /// See <see cref="IBody{TShape}.CurrentState"/>.
        /// </summary>
        public BodyState CurrentState => this._state;

        /// <summary>
        /// See <see cref="IBody{TShape}.GetCurrentShape"/>.
        /// </summary>
        public IPolygonShape GetCurrentShape()
        {
            var currentShape = this._shapeFactory.CreateTransformedPolygonShape(
                this.OriginalShape,
                this._state.Position,
                this._state.Orientation);

            return currentShape;
        }

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
            // The force will result in linear acceleration...
            this.AddForce(force);

            // ...as well as angular acceleration.
            var torque = this._bodyCalculationHelper.CalculateTorque(force, offset);
            this._appliedTorque += torque;
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            // Linear motion
            var acceleration = this._isaacNewtonHelper.CalculateAcceleration(
                this._appliedForce,
                this.Mass);

            this._state.Velocity = this._isaacNewtonHelper.CalculateVelocity(
                this._state.Velocity,
                acceleration,
                time);

            this._state.Position = this._isaacNewtonHelper.CalculatePosition(
                this._state.Position,
                this._state.Velocity,
                time);

            // Rotation
            var angularAcceleration = this._isaacNewtonHelper.CalculateAngularAcceleration(
                this._appliedTorque,
                this.Inertia);

            this._state.AngularVelocity = this._isaacNewtonHelper.CalculateAngularVelocity(
                this._state.AngularVelocity,
                angularAcceleration,
                time);

            this._state.Orientation = this._isaacNewtonHelper.CalculateOrientation(
                this._state.Orientation,
                this._state.AngularVelocity,
                time);
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ResetAppliedForce"/>.
        /// </summary>
        public void ResetAppliedForce()
        {
            this._appliedForce = new Vector2();
            this._appliedTorque = 0;
        }
    }
}

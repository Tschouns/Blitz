﻿//-----------------------------------------------------------------------
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
    using Physics.Services.Elements.Shape;

    /// <summary>
    /// Implementation of <see cref="IBody{TShapeFigure}"/>. Implements the behavior of a "rigid body".
    /// </summary>
    /// <typeparam name="TShapeFigure">
    /// See <see cref="IBody{TShapeFigure}"/>.
    /// </typeparam>
    public class RigidBody<TShapeFigure> : IBody<TShapeFigure>
        where TShapeFigure : class, IFigure
    {
        /// <summary>
        /// Used to calculate the torque.
        /// </summary>
        private readonly IBodyCalculationHelper _bodyCalculationHelper;

        /// <summary>
        /// Used to calculate the acceleration, velocity and position.
        /// </summary>
        private readonly IIsaacNewtonHelper _isaacNewtonHelper;

        /// <summary>
        /// Stores the shape of this body.
        /// </summary>
        private readonly IRigidShape<TShapeFigure> _shape;

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
        /// Initializes a new instance of the <see cref="RigidBody{TShapeFigure}"/> class.
        /// TODO:
        /// * Make this class generic, i.e. unaware of the specific shape. It shall take
        ///   the shape as an argument on construction.
        /// * Move all shape-dependent code to the shape class/interface, such as inertia
        ///   calculation (would take the mass as input).
        /// * Perhaps create an internal shape interface.
        /// * The <see cref="IBody{TShape}"/> shall only know one shape, as opposed to
        ///   "original" and "current". The shape shall be able to "update" (transform) itself,
        ///   taking position and orientation as input. The shape then provides the "original"
        ///   and "current" representation of itself. This way, it can be guaranteed that the
        ///   actual transformation is only performed once per step and body/shape. 
        /// </summary>
        public RigidBody(
            IBodyCalculationHelper bodyCalculationHelper,
            IIsaacNewtonHelper isaacNewtonHelper,
            double mass,
            IRigidShape<TShapeFigure> shape,
            BodyState initialBodyState)
        {
            Checks.AssertNotNull(bodyCalculationHelper, nameof(bodyCalculationHelper));
            Checks.AssertNotNull(isaacNewtonHelper, nameof(isaacNewtonHelper));
            Checks.AssertIsStrictPositive(mass, nameof(mass));
            Checks.AssertNotNull(shape, nameof(shape));

            // Helpers
            this._bodyCalculationHelper = bodyCalculationHelper;
            this._isaacNewtonHelper = isaacNewtonHelper;

            // Static properties
            this.Mass = mass;
            this._shape = shape;
            this.Inertia = this._shape.CalculateInertia(this.Mass);

            // Dynamic properties
            this._appliedForce = new Vector2();
            this._appliedTorque = 0;
            this._state = initialBodyState;

            // Update the shape, based on the initial state
            this._shape.Update(this._state.Position, this._state.Orientation);
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
        /// Gets... see <see cref="IBody{TShapeFigure}.Shape"/>.
        /// </summary>
        public IShape<TShapeFigure> Shape => this._shape;

        /// <summary>
        /// See <see cref="IBody{TShape}.CurrentState"/>.
        /// </summary>
        public BodyState CurrentState => this._state;

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
        /// See <see cref="IPhysicalObject.AddForceAtPointInSpace"/>.
        /// </summary>
        public void AddForceAtPointInSpace(Vector2 force, Point pointInSpace)
        {
            var offset = pointInSpace.GetOffsetFrom(this._state.Position);

            this.AddForceAtOffset(force, offset);
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

            // Update the shape
            this._shape.Update(this._state.Position, this._state.Orientation);
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

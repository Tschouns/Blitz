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
    using Physics.Elements;
    using Physics.Elements.Shape;

    /// <summary>
    /// Implementation of <see cref="IBody"/>. Implements the behavior of a "rigid body".
    /// </summary>
    public class RigidBody : IBody<IPolygonShape>
    {
        /// <summary>
        /// Used to create the <see cref="IPolygonShape"/>, based on a <see cref="Polygon"/>.
        /// TODO: Check whether it's really needed as a member, or whether the creation of
        /// the shape shall be done in this class at all.
        /// </summary>
        private readonly IShapeFactory shapeFactory;

        /// <summary>
        /// Stores the currently applied force.
        /// </summary>
        private Vector2 appliedForce;

        /// <summary>
        /// Stores the current state of this rigid body.
        /// </summary>
        private BodyState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="RigidBody"/> class.
        /// </summary>
        public RigidBody(
            IShapeFactory shapeFactory,
            double mass,
            Polygon polygon,
            Point initialPosition,
            double initialOrientation,
            Vector2 initialVelocity)
        {
            Checks.AssertNotNull(shapeFactory, nameof(shapeFactory));
            Checks.AssertIsStrictPositive(mass, nameof(mass));
            Checks.AssertNotNull(polygon, nameof(polygon));

            this.shapeFactory = shapeFactory;
            this.Mass = mass;

            // The "original shape" is created one, and won't change over the lifecycle of the class.
            this.OriginalShape = this.shapeFactory.CreateOriginalPolygonShape(polygon);

            this.appliedForce = new Vector2();
            this.state = new BodyState
            {
                Position = initialPosition,
                Velocity = new Vector2(0, 0),
                Orientation = initialOrientation,
                AngularVelocity = 0
            };
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
        /// See <see cref="IBody{TShape}.Shape"/>.
        /// </summary>
        public BodyState CurrentState => this.state;

        /// <summary>
        /// See <see cref="IBody{TShape}.GetCurrentShape"/>.
        /// </summary>
        public IPolygonShape GetCurrentShape()
        {
            var currentShape = this.shapeFactory.CreateTransformedPolygonShape(
                this.OriginalShape,
                this.state.Position,
                this.state.Orientation);

            return currentShape;
        }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.ResetAppliedForce"/>.
        /// </summary>
        public void ResetAppliedForce()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IPhysicalObject.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            throw new NotImplementedException();
        }
    }
}

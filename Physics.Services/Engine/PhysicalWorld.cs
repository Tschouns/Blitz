//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Engine
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Elements;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Engine;

    /// <summary>
    /// See <see cref="IPhysicalWorld"/>.
    /// </summary>
    public class PhysicalWorld : IPhysicalWorld
    {
        /// <summary>
        /// Holds all "physical" objects in this world.
        /// </summary>
        private readonly IList<IPhysicalObject> physicalObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalWorld"/> class.
        /// </summary>
        public PhysicalWorld()
        {
            this.physicalObjects = new List<IPhysicalObject>();
        }

        /// <summary>
        /// See <see cref="IPhysicalWorld.CreateParticle"/>.
        /// </summary>
        public IParticle CreateParticle(double mass, Point position)
        {
            Checks.AssertIsPositive(mass, nameof(mass));

            var particle = new Particle(
                10,
                PhysicsGlobalConstants.WorldOrigin,
                new Vector2());

            this.physicalObjects.Add(particle);

            return particle;
        }

        /// <summary>
        /// See <see cref="IPhysicalWorld.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            Checks.AssertIsPositive(time, nameof(time));

            this.ApplyGravity();

            foreach (var physicalObject in this.physicalObjects)
            {
                physicalObject.Step(time);
            }

            foreach (var physicalObject in this.physicalObjects)
            {
                physicalObject.ResetAppliedForce();
            }
        }

        /// <summary>
        /// Applies earth's gravity to all "physical objects".
        /// </summary>
        private void ApplyGravity()
        {
            var gravityForce = new Vector2(0, -PhysicsGlobalConstants.EarthGravityAcceleration);

            foreach (var physicalObject in this.physicalObjects)
            {
                physicalObject.AddForce(gravityForce);
            }
        }
    }
}

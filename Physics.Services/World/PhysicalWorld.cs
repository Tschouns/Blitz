//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.World
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Forces;
    using Physics.World;

    /// <summary>
    /// See <see cref="IPhysicalWorld"/>.
    /// </summary>
    public class PhysicalWorld : IPhysicalWorld
    {
        /// <summary>
        /// Used to create different elements.
        /// </summary>
        private readonly IElementFactory elementFactory;

        /// <summary>
        /// Used to create different physical forces.
        /// </summary>
        private readonly IForceFactory forceFactory;

        /// <summary>
        /// Stores the "physical space" which contains all the "physical objects" of this world.
        /// </summary>
        private readonly IPhysicalSpace physicalSpace;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalWorld"/> class.
        /// </summary>
        public PhysicalWorld(
            IElementFactory elementFactory,
            IForceFactory forceFactory)
        {
            Checks.AssertNotNull(elementFactory, nameof(elementFactory));
            Checks.AssertNotNull(forceFactory, nameof(forceFactory));

            this.elementFactory = elementFactory;
            this.forceFactory = forceFactory;

            this.physicalSpace = this.elementFactory.CreateSpace();

            // Configure world, add gravity (this might get a little more flexible in the future).
            var gravity = this.forceFactory.CreateGravity(PhysicsGlobalConstants.EarthGravityAcceleration);
            this.physicalSpace.AddGlobalForce(gravity);
        }

        /// <summary>
        /// See <see cref="IPhysicalWorld.CreateParticle"/>.
        /// </summary>
        public IParticle CreateParticle(double mass, Point position)
        {
            Checks.AssertIsStrictPositive(mass, nameof(mass));

            var particle = this.elementFactory.CreateParticle(mass, position);
            this.physicalSpace.AddParticle(particle);

            return particle;
        }

        /// <summary>
        /// See <see cref="IPhysicalWorld.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            Checks.AssertIsPositive(time, nameof(time));

            this.physicalSpace.Step(time);
        }
    }
}

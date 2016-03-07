//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.World
{
    using Base.RuntimeChecks;
    using Physics.Elements;
    using Physics.Forces;
    using Physics.World;

    /// <summary>
    /// See <see cref="IPhysicsFactory"/>.
    /// </summary>
    public class PhysicsFactory : IPhysicsFactory
    {
        /// <summary>
        /// Stores the <see cref="IElementFactory"/>.
        /// </summary>
        private readonly IElementFactory elementFactory;

        /// <summary>
        /// Stores the <see cref="IForceFactory"/>.
        /// </summary>
        private readonly IForceFactory forceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsFactory"/> class.
        /// </summary>
        public PhysicsFactory(
            IElementFactory elementFactory,
            IForceFactory forceFactory)
        {
            Checks.AssertNotNull(elementFactory, nameof(elementFactory));
            Checks.AssertNotNull(forceFactory, nameof(forceFactory));

            this.elementFactory = elementFactory;
            this.forceFactory = forceFactory;
        }

        /// <summary>
        /// See <see cref="IPhysicsFactory.CreatePhysicalWorld"/>.
        /// </summary>
        public IPhysicalWorld CreatePhysicalWorld()
        {
            var physicalWorld = new PhysicalWorld(
                this.elementFactory,
                this.forceFactory);

            return physicalWorld;
        }
    }
}

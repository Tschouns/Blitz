//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.World
{
    using System;
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
        private readonly IElementFactory _elementFactory;

        /// <summary>
        /// Stores the <see cref="IForceFactory"/>.
        /// </summary>
        private readonly IForceFactory _forceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsFactory"/> class.
        /// </summary>
        public PhysicsFactory(
            IElementFactory elementFactory,
            IForceFactory forceFactory)
        {
            Checks.AssertNotNull(elementFactory, nameof(elementFactory));
            Checks.AssertNotNull(forceFactory, nameof(forceFactory));

            this._elementFactory = elementFactory;
            this._forceFactory = forceFactory;
        }

        /// <summary>
        /// See <see cref="IPhysicsFactory.Forces"/>.
        /// </summary>
        public IForceFactory Forces => this._forceFactory;

        /// <summary>
        /// See <see cref="IPhysicsFactory.CreatePhysicalWorld"/>.
        /// </summary>
        public IPhysicalWorld CreatePhysicalWorld()
        {
            var physicalWorld = new PhysicalWorld(
                this._elementFactory,
                this._forceFactory);

            return physicalWorld;
        }
    }
}

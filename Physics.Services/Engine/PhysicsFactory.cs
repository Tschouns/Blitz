//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Engine
{
    using System;
    using Physics.Elements;
    using Physics.Engine;

    /// <summary>
    /// See <see cref="IPhysicsFactory"/>.
    /// </summary>
    public class PhysicsFactory : IPhysicsFactory
    {
        /// <summary>
        /// See <see cref="IPhysicsFactory.CreatePhysicalWorld"/>.
        /// </summary>
        public IPhysicalWorld CreatePhysicalWorld()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IPhysicsFactory.CreateParticle"/>.
        /// </summary>
        public IParticle CreateParticle()
        {
            throw new NotImplementedException();
        }
    }
}

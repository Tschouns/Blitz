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
    /// See <see cref="IPhysicalWorld"/>.
    /// </summary>
    public class PhysicalWorld : IPhysicalWorld
    {
        /// <summary>
        /// See <see cref="IPhysicalWorld.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IPhysicalWorld.AddParticle"/>.
        /// </summary>
        public void AddParticle(IParticle particle)
        {
            throw new NotImplementedException();
        }
    }
}

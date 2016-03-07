//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Engine
{
    using Base.RuntimeChecks;
    using Elements;
    using Geometry.Elements;
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
            var world = new PhysicalWorld();

            return world;
        }
    }
}

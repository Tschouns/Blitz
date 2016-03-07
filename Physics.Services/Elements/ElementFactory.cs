//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// See <see cref="IElementFactory"/>.
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        /// <summary>
        /// See <see cref="IElementFactory.CreateSpace"/>.
        /// </summary>
        public IPhysicalSpace CreateSpace()
        {
            return new PhysicalSpace();
        }

        /// <summary>
        /// See <see cref="IElementFactory.CreateParticle"/>.
        /// </summary>
        public IParticle CreateParticle(double mass, Point position)
        {
            Checks.AssertIsPositive(mass, nameof(mass));

            var particle = new Particle(
                mass,
                position,
                new Vector2());
            
            return particle;
        }
    }
}

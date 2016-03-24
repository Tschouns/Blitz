//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Helpers;
    using Physics.Elements;

    /// <summary>
    /// See <see cref="IElementFactory"/>.
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        /// <summary>
        /// Stores the <see cref="IIsaacNewtonHelper"/>.
        /// </summary>
        private readonly IIsaacNewtonHelper _calculationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementFactory"/> class.
        /// </summary>
        public ElementFactory(IIsaacNewtonHelper calculationHelper)
        {
            Checks.AssertNotNull(calculationHelper, nameof(calculationHelper));

            this._calculationHelper = calculationHelper;
        }

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
            Checks.AssertIsStrictPositive(mass, nameof(mass));

            var particle = new Particle(
                this._calculationHelper,
                mass,
                position,
                new Vector2());
            
            return particle;
        }
    }
}

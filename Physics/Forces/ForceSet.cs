//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Contains a set of forces, one for each known type of "physical object".
    /// </summary>
    public class ForceSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForceSet"/> class.
        /// </summary>
        public ForceSet(
            IForce<IParticle> forParticles,
            IForce<IBody<Polygon>> forBodies)
        {
            Checks.AssertNotNull(forParticles, nameof(forParticles));
            Checks.AssertNotNull(forBodies, nameof(forBodies));

            this.ForParticles = forParticles;
            this.ForBodies = forBodies;
        }

        /// <summary>
        /// Gets the force to apply to particles.
        /// </summary>
        public IForce<IParticle> ForParticles { get; }

        /// <summary>
        /// Gets the force to apply to polygon bodies.
        /// </summary>
        public IForce<IBody<Polygon>> ForBodies { get; }
    }
}

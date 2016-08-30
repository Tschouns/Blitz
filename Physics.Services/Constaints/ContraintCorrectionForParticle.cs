//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Constaints
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Represents a correction to apply to a body in order to satisfy a contraint.
    /// </summary>
    public class ContraintCorrectionForParticle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContraintCorrectionForParticle"/> class.
        /// </summary>
        public ContraintCorrectionForParticle(
            IParticle particle,
            Vector2 correctionVelocity)
        {
            Checks.AssertNotNull(particle, nameof(particle));

            this.Particle = particle;
            this.CorrectionVelocity = correctionVelocity;
        }

        /// <summary>
        /// Gets the particle.
        /// </summary>
        public IParticle Particle { get; }

        /// <summary>
        /// Gets the correction velocity to add to the body.
        /// </summary>
        public Vector2 CorrectionVelocity { get; }
    }
}

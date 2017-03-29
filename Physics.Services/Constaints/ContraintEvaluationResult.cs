//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Constaints
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// The result of a contraint evaluation.
    /// </summary>
    public class ContraintEvaluationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContraintEvaluationResult"/> class.
        /// </summary>
        public ContraintEvaluationResult(
            bool isSatisfied,
            IEnumerable<IParticle> correctionsForParticles,
            IEnumerable<IBody<Polygon>> correctionsForPolygonBodies)
        {
            ArgumentChecks.AssertNotNull(correctionsForParticles, nameof(correctionsForParticles));
            ArgumentChecks.AssertNotNull(correctionsForPolygonBodies, nameof(correctionsForPolygonBodies));

            this.IsSatisfied = isSatisfied;
            this.CorrectionsForParticles = correctionsForParticles;
            this.CorrectionsForPolygonBodies = correctionsForPolygonBodies;
        }

        /// <summary>
        /// Gets a value indicating whether the contraint is satisfied.
        /// </summary>
        public bool IsSatisfied { get; }

        /// <summary>
        /// Gets a set of corrections to apply to particles.
        /// </summary>
        public IEnumerable<IParticle> CorrectionsForParticles { get; }

        /// <summary>
        /// Gets a set of corrections to apply to polygon bodies.
        /// </summary>
        public IEnumerable<IBody<Polygon>> CorrectionsForPolygonBodies { get; }
    }
}

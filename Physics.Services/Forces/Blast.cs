//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using System;
    using Base.RuntimeChecks;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// Simulates a blast, which pushes surrounding objects away.
    /// </summary>
    public class Blast : IGlobalForce
    {
        /// <summary>
        /// The force induced by this blast.
        /// </summary>
        private readonly double _force;
        private readonly double _blastRadius;
        private readonly double _expansionSpeed;
        private double blablablbaFinishThis!!

        /// <summary>
        /// Initializes a new instance of the <see cref="Blast"/> class.
        /// </summary>
        public Blast(
            double force,
            double blastRadius,
            double expansionSpeed)
        {
            Checks.AssertIsPositive(blastRadius, nameof(blastRadius));
            Checks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));


        }

        public void ApplyToObject(IPhysicalObject physicalObject)
        {
            throw new NotImplementedException();
        }

        public void Step(double time)
        {
            throw new NotImplementedException();
        }
    }
}

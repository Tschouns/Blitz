//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// See <see cref="IForceFactory"/>.
    /// </summary>
    public class ForceFactory : IForceFactory
    {
        /// <summary>
        /// See <see cref="IForceFactory.CreateGravityForParticles(double)"/>.
        /// </summary>
        public IGlobalForce<IParticle> CreateGravityForParticles(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            return new Gravity<IParticle>(acceleration);
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateGravityForBodies(double)"/>.
        /// </summary>
        public IGlobalForce<IBody<Polygon>> CreateGravityForBodies(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            return new Gravity<IBody<Polygon>>(acceleration);
        }
    }
}

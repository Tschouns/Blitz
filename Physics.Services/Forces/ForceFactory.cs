//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using Base.RuntimeChecks;
    using Physics.Forces;

    /// <summary>
    /// See <see cref="IForceFactory"/>.
    /// </summary>
    public class ForceFactory : IForceFactory
    {
        /// <summary>
        /// See <see cref="IForceFactory.CreateGravity"/>.
        /// </summary>
        public IGlobalForce CreateGravity(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            return new Gravity(acceleration);
        }
    }
}

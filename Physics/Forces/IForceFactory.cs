//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    /// <summary>
    /// Creates forces.
    /// </summary>
    public interface IForceFactory
    {
        /// <summary>
        /// Creates a <see cref="IGlobalForce"/> which simulates "gravity".
        /// </summary>
        IGlobalForce CreateGravity(double acceleration);
    }
}

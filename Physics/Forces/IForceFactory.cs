//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    using Elements;
    using Geometry.Elements;

    /// <summary>
    /// Creates forces.
    /// </summary>
    public interface IForceFactory
    {
        /// <summary>
        /// Creates "gravity".
        /// </summary>
        ForceSet CreateGravity(double acceleration);

        /// <summary>
        /// Creates a blast which pushes away surrounding objects.
        /// </summary>
        ForceSet CreateBlast(
            Point position,
            double force,
            double blastRadius,
            double expansionSpeed);
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics
{
    using Geometry.Elements;

    /// <summary>
    /// Provides some global constants.
    /// </summary>
    public static class PhysicsGlobalConstants
    {
        /// <summary>
        /// Earth's gravity acceleration, in m/s^2.
        /// </summary>
        public static readonly double EarthGravityAcceleration = 9.81;

        /// <summary>
        /// The "physical world's" origin.
        /// </summary>
        public static readonly Point WorldOrigin = new Point();
    }
}

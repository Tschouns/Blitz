//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Helpers
{
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to perform calculations based on Newton's laws.
    /// </summary>
    public interface IIsaacNewtonHelper
    {
        /// <summary>
        /// Calculates the acceleration of a "physical object", based on the applied force and mass.
        /// </summary>
        Vector2 CalculateAcceleration(Vector2 appliedForce, double mass);

        /// <summary>
        /// Calculates the velocity of a "physical object", based on the current velocity, the acceleration and elapsed time (in seconds).
        /// </summary>
        Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 acceleration, double time);

        /// <summary>
        /// Calculates the position of a "physical object", based on the current position, the velocity and elapsed time (in seconds).
        /// </summary>
        Point CalculatePosition(Point currentPosition, Vector2 velocity, double time);
    }
}

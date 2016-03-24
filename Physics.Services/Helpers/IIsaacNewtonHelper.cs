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
        /// Calculates the acceleration of a "physical object", based on the applied
        /// force and mass.
        /// </summary>
        Vector2 CalculateAcceleration(Vector2 appliedForce, double mass);

        /// <summary>
        /// Calculates the velocity of a "physical object", based on the current velocity,
        /// the acceleration and elapsed time (in seconds).
        /// </summary>
        Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 acceleration, double time);

        /// <summary>
        /// Calculates the position of a "physical object", based on the current position,
        /// the velocity and elapsed time (in seconds).
        /// </summary>
        Point CalculatePosition(Point currentPosition, Vector2 velocity, double time);

        /// <summary>
        /// Calculates the angular acceleration (in radians/second^2) of a "physical object",
        /// based on the applied torque and inertia.
        /// </summary>
        double CalculateAngularAcceleration(double torque, double inertia);

        /// <summary>
        /// Calculates the angular velocity (in radians/second) of a "physical object", based
        /// on the current angular velocity, the angular acceleration and elapsed
        /// time (in seconds).
        /// </summary>
        double CalculateAngularVelocity(double currentAngularVelocity, double angularAcceleration, double time);

        /// <summary>
        /// Calculates the orientation (in radians) of a "physical object", based on the current orientation,
        /// the angular velocity and elapsed time (in seconds).
        /// </summary>
        double CalculateOrientation(double currentOrientation, double angularVelocity, double time);
    }
}

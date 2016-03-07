//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Helpers
{
    using System;
    using Geometry.Elements;

    /// <summary>
    /// See <see cref="ICalculationHelper"/>.
    /// </summary>
    public class CalculationHelper : ICalculationHelper
    {
        /// <summary>
        /// See <see cref="ICalculationHelper.CalculateAcceleration"/>.
        /// </summary>
        public Vector2 CalculateAcceleration(Vector2 appliedForce, double mass)
        {
            return new Vector2(
                appliedForce.X / mass,
                appliedForce.Y / mass);
        }

        /// <summary>
        /// See <see cref="ICalculationHelper.CalculateVelocity"/>.
        /// </summary>
        public Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 acceleration, double time)
        {
            return new Vector2(
                currentVelocity.X + (acceleration.X * time),
                currentVelocity.Y + (acceleration.Y * time));
        }

        /// <summary>
        /// See <see cref="ICalculationHelper.CalculatePosition"/>.
        /// </summary>
        public Point CalculatePosition(Point currentPosition, Vector2 velocity, double time)
        {
            return new Point(
                currentPosition.X + (velocity.X * time),
                currentPosition.Y + (velocity.Y * time));
        }
    }
}

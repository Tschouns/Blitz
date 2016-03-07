//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// Simulates gravity.
    /// </summary>
    public class Gravity : IGlobalForce
    {
        /// <summary>
        /// Stores the actual force vector.
        /// </summary>
        private readonly double gravityAcceleration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gravity"/> class.
        /// </summary>
        public Gravity(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            this.gravityAcceleration = acceleration;
        }

        /// <summary>
        /// See <see cref="IGlobalForce.ApplyToObject"/>.
        /// </summary>
        public void ApplyToObject(IPhysicalObject physicalObject)
        {
            Checks.AssertNotNull(physicalObject, nameof(physicalObject));

            var force = new Vector2(0, -(this.gravityAcceleration * physicalObject.Mass));

            physicalObject.AddForce(force);
        }

        /// <summary>
        /// See <see cref="IGlobalForce.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            // Gravity doesn't change over time.
        }
    }
}

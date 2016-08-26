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
    /// Simulates gravity, for any <see cref="TPhysicalObject"/>.
    /// </summary>
    /// <typeparam name="TPhysicalObject">
    /// Type of "physical object" this force can be applied to
    /// </typeparam>
    public class GenericGravity<TPhysicalObject> : IForce<TPhysicalObject>
        where TPhysicalObject : class, IPhysicalObject
    {
        /// <summary>
        /// Stores the actual force vector.
        /// </summary>
        private readonly double _gravityAcceleration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gravity"/> class.
        /// </summary>
        public GenericGravity(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            this._gravityAcceleration = acceleration;
        }

        /// <summary>
        /// See <see cref="IGlobalForce.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(TPhysicalObject physicalObject)
        {
            Checks.AssertNotNull(physicalObject, nameof(physicalObject));

            var force = new Vector2(0, -(this._gravityAcceleration * physicalObject.Mass));

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

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using System;
    using Geometry.Elements;
    using Physics.Forces;
    using Physics.Elements;
    using Base.RuntimeChecks;
    using Geometry.Extensions;
    /// <summary>
    /// Simulates flow resistance to rotation. It slows down the rotation of bodies bodies based on their velocity, angular velocity and volume.
    /// </summary>
    public class BodyRotationalFlowResistance<TShapeFigure> : IForce<IBody<TShapeFigure>>
        where TShapeFigure : class, IFigure
    {
        /// <summary>
        /// The density of the fluid or gas.
        /// </summary>
        private readonly double _density;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyFlowResistance"/> class.
        /// </summary>
        public BodyRotationalFlowResistance(
            double density)
        {
            Checks.AssertIsPositive(density, nameof(density));
            
            this._density = density;
        }

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.IsDepleted"/>. This force is never depleted.
        /// </summary>
        public bool IsDepleted => false;

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.Step(double)"/>.
        /// </summary>
        public void Step(double time)
        {
            // Flow resistance does not change over time.
        }

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(IBody<TShapeFigure> physicalObject)
        {
            Checks.AssertNotNull(physicalObject, nameof(physicalObject));

            // Determine torque.
            var angularVelocity = physicalObject.CurrentState.AngularVelocity;
            var torque = physicalObject.Shape.Volume * physicalObject.Shape.Volume * this._density * angularVelocity * angularVelocity;

            if (angularVelocity > 0)
            {
                torque = -torque;
            }

            var linearVelocity = physicalObject.CurrentState.Velocity.Magnitude();
            if (linearVelocity > 0)
            {
                torque = torque / linearVelocity;
            }

            // Apply torque.
            physicalObject.ApplyTorque(torque);
        }
    }
}

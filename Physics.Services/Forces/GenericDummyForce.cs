//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// Has no effect.
    /// </summary>
    /// <typeparam name="TPhysicalObject">
    /// Type of "physical object" this force can be applied to
    /// </typeparam>
    public class GenericDummyForce<TPhysicalObject> : IForce<TPhysicalObject>
        where TPhysicalObject : class, IPhysicalObject
    {
        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.IsDepleted"/>.
        /// </summary>
        public bool IsDepleted => true;

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.Step(double)"/>.
        /// </summary>
        public void Step(double time)
        {
            // Has no effect.
        }

        /// <summary>
        /// See <see cref="IForce.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(TPhysicalObject physicalObject)
        {
            // Has no effect.
        }
    }
}

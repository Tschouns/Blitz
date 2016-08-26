//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    using Physics.Elements;

    /// <summary>
    /// Represents a force which can be applied to <see cref="TPhysicalObject"/>s within
    /// a "space", such as gravity, air resistance, wind, buoyant force, blasts,...
    /// </summary>
    /// <typeparam name="TPhysicalObject">
    /// Type of "physical object" this force can be applied to
    /// </typeparam>
    public interface IForce<TPhysicalObject>
        where TPhysicalObject : class, IPhysicalObject
    {
        /// <summary>
        /// Steps forward in time, as the force may be dynamic and change over time (like wind).
        /// </summary>
        void Step(double time);

        /// <summary>
        /// Applies the force to the specified <typeparamref name="TPhysicalObject"/>.
        /// </summary>
        void ApplyToObject(TPhysicalObject physicalObject);
    }
}

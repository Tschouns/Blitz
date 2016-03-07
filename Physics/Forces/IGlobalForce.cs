//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    using Physics.Elements;

    /// <summary>
    /// Represents a "global force" which is applied to all "physical object" within
    /// a "space", such as gravity, wind, buoyant force,...
    /// </summary>
    public interface IGlobalForce
    {
        /// <summary>
        /// Steps forward in time, as the force may be dynamic and change over time (like wind).
        /// </summary>
        void Step(double time);

        /// <summary>
        /// Applies the force to the specified "physical object".
        /// </summary>
        void ApplyToObject(IPhysicalObject physicalObject);
    }
}

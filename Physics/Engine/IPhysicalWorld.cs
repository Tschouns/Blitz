//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Engine
{
    using Physics.Elements;

    /// <summary>
    /// Represents the "physical world".
    /// </summary>
    public interface IPhysicalWorld
    {
        /// <summary>
        /// Steps forward in time, by the specified number (fraction) of seconds.
        /// </summary>
        void Step(double time);

        /// <summary>
        /// Adds a particle to the "physical world".
        /// </summary>
        void AddParticle(IParticle particle);
    }
}

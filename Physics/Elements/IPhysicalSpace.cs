//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Physics.Forces;

    /// <summary>
    /// Represents a "physical space", although 2D you know... not really a "space". But we'll call it that ;)
    /// </summary>
    public interface IPhysicalSpace
    {
        /// <summary>
        /// Adds a "global force" to the "physical space".
        /// </summary>
        void AddGlobalForce(IGlobalForce globalForce);

        /// <summary>
        /// Adds a particle to the "physical space".
        /// </summary>
        void AddParticle(IParticle particle);

        /// <summary>
        /// Steps forward in time, by the specified number (fraction) of seconds.
        /// </summary>
        void Step(double time);
    }
}

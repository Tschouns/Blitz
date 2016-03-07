//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.World
{
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Represents the "physical world".
    /// </summary>
    public interface IPhysicalWorld
    {
        /// <summary>
        /// Creates a particle in the "physical world".
        /// </summary>
        IParticle CreateParticle(double mass, Point position);

        /// <summary>
        /// Steps forward in time, by the specified number (fraction) of seconds.
        /// </summary>
        void Step(double time);
    }
}

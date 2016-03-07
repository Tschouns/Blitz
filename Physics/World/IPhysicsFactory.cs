//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.World
{
    /// <summary>
    /// Created the "physical world" and other "physical" objects.
    /// </summary>
    public interface IPhysicsFactory
    {
        /// <summary>
        /// Creates a "physical world".
        /// </summary>
        IPhysicalWorld CreatePhysicalWorld();
    }
}

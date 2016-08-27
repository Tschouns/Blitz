//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.World
{
    using Physics.Forces;

    /// <summary>
    /// Creates the "physical world" and other "physical" objects.
    /// </summary>
    public interface IPhysicsFactory
    {
        /// <summary>
        /// Gets the <see cref="IForceFactory"/>.
        /// </summary>
        IForceFactory Forces { get; }

        /// <summary>
        /// Creates a "physical world".
        /// </summary>
        IPhysicalWorld CreatePhysicalWorld();
    }
}

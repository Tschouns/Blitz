//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.World
{
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Elements.Shape;

    /// <summary>
    /// Represents the "physical world".
    /// </summary>
    public interface IPhysicalWorld
    {
        /// <summary>
        /// Spawns a particle in the "physical world".
        /// </summary>
        IParticle SpawnParticle(double mass, Point position);

        /// <summary>
        /// Spawns a rigid body in the "physical world".
        /// </summary>
        IBody<Polygon> SpawnRigidBody(double mass, Polygon polygon, Point position);

        /// <summary>
        /// Steps forward in time, by the specified number (fraction) of seconds.
        /// </summary>
        void Step(double time);
    }
}

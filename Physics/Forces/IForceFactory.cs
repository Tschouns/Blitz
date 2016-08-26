//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Forces
{
    using Geometry.Elements;
    using Elements;

    /// <summary>
    /// Creates forces.
    /// </summary>
    public interface IForceFactory
    {
        /// <summary>
        /// Creates a <see cref="IGlobalForce"/> which simulates "gravity" for particles.
        /// </summary>
        IGlobalForce<IParticle> CreateGravityForParticles(double acceleration);

        /// <summary>
        /// Creates a <see cref="IGlobalForce"/> which simulates "gravity" for bodies.
        /// </summary>
        IGlobalForce<IBody<Polygon>> CreateGravityForBodies(double acceleration);
    }
}

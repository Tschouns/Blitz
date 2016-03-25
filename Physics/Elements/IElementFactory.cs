//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Geometry.Elements;

    /// <summary>
    /// Creates elements such as the "physical space" or "physical objects".
    /// </summary>
    public interface IElementFactory
    {
        /// <summary>
        /// Creates a <see cref="IPhysicalSpace"/>.
        /// </summary>
        IPhysicalSpace CreateSpace();

        /// <summary>
        /// Creates a <see cref="IParticle"/> of the specified mass, at the specified position
        /// in space.
        /// </summary>
        IParticle CreateParticle(double mass, Point position);

        /// <summary>
        /// Creates a <see cref="IBody{Polygon}"/> of the specified mass and shape polygon,
        /// at the specified position in space.
        /// </summary>
        IBody<Polygon> CreateRigidBody(double mass, Polygon polygon, Point position);
    }
}

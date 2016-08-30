//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Collision
{
    using Physics.Elements;

    /// <summary>
    /// Detects collisions between a <typeparamref name="TPhysicalObject1"/> and <typeparamref name="TPhysicalObject2"/>.
    /// </summary>
    /// <typeparam name="TPhysicalObject1">
    /// Type of the first physical object
    /// </typeparam>
    /// <typeparam name="TPhysicalObject2">
    /// Type of the second physical object
    /// </typeparam>
    public interface ICollisionDetector<TPhysicalObject1, TPhysicalObject2>
        where TPhysicalObject1 : class, IPhysicalObject
        where TPhysicalObject2 : class, IPhysicalObject
    {
        /// <summary>
        /// Determines whether two specified physical objects collide, as well as additional information
        /// about the collision.
        /// </summary>
        CollisionDetectionResult DetectCollision(TPhysicalObject1 physicalObject1, TPhysicalObject2 PhysicalObject2);
    }
}

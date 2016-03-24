//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a "physical object" in the "physical world".
    /// </summary>
    public interface IPhysicalObject
    {
        /// <summary>
        /// Gets the mass.
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// Applies the specified force to a "physical object", at the origin (center of mass).
        /// </summary>
        void AddForce(Vector2 force);

        /// <summary>
        /// Applies the specified force to a "physical object", at a specific point, specified
        /// by an offset relative to the objects origin (center of mass).
        /// </summary>
        void AddForceAtOffset(Vector2 force, Vector2 offset);

        /// <summary>
        /// Applies the specified force to a "physical object", at a specific point in space.
        /// </summary>
        void AddForceAtPointInSpace(Vector2 force, Point pointInSpace);

        /// <summary>
        /// Steps forward in time, by the specified number (fraction) of seconds.
        /// Computes, based on applied force(s)/torque:
        /// 1) acceleration
        /// 2) velocity
        /// 3) angular velocity (for bodies)
        /// 4) position
        /// 5) orientation (for bodies)
        /// </summary>
        void Step(double time);

        /// <summary>
        /// Resets all applied force.
        /// </summary>
        void ResetAppliedForce();
    }
}

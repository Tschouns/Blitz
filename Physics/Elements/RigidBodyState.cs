//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    using Geometry.Elements;

    /// <summary>
    /// Represents the variable state of a rigid body at a specific point in time.
    /// </summary>
    public struct RigidBodyState
    {
        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the current velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the current angular orientation about the origin, in radians.
        /// </summary>
        public double Orientation { get; set; }

        /// <summary>
        /// Gets or sets the current angular velocity about the origin, in radians per second.
        /// </summary>
        public double AngularVelocity { get; set; }
    }
}

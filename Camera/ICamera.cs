//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a camera pointed at the game world. Helps with transforming positions
    /// between world and viewport coordinates.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets or sets current camera state.
        /// </summary>
        CameraState State { get; set; }

        /// <summary>
        /// Gets the resulting transformation object, which transforms positions between world
        /// and viewport coordinates.
        /// </summary>
        ICameraTransformation GetCameraTransformation();

        /// <summary>
        /// Determines whether the specified point (in world coordinates) is in view, based on the current
        /// camera state.
        /// </summary>
        bool IsInView(Point point);
    }
}

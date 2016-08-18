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
    /// between world and display coordinates.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets or sets the current position of the camera, i.e. the position in world coordinates that
        /// the camera points to.
        /// </summary>
        Point Position { get; set; }

        /// <summary>
        /// Gets or sets the current orientation of the camera, about the center (in radians).
        /// </summary>
        float Orientation { get; set; }

        /// <summary>
        /// Gets or sets the current camera scale.
        /// </summary>
        float Scale { get; set; }
    }
}

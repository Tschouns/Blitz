//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using Geometry.Elements;

    /// <summary>
    /// Represents the state of a camera.
    /// </summary>
    public struct CameraState
    {
        /// <summary>
        /// Gets or sets the position of the camera, i.e. the position in world coordinates that
        /// the camera points to.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the orientation of the camera, about the center (in radians).
        /// </summary>
        public double Orientation { get; set; }

        /// <summary>
        /// Gets or sets the camera scale.
        /// </summary>
        public double Scale { get; set; }
    }
}

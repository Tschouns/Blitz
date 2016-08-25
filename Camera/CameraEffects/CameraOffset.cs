//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.CameraEffects
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a camera offset, i.e. the difference between two <see cref="CameraState"/>s.
    /// </summary>
    public struct CameraOffset
    {
        /// <summary>
        /// Gets or sets the camera position offset.
        /// </summary>
        public Vector2 PositionOffset { get; set; }

        /// <summary>
        /// Gets or sets the camera orientation offset (in radians).
        /// </summary>
        public double OrientationOffset { get; set; }

        /// <summary>
        /// Gets or sets the camera scale offset.
        /// </summary>
        public double ScaleOffset { get; set; }
    }
}

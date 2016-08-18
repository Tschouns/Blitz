//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using Geometry.Elements;

    /// <summary>
    /// See <see cref="ICamera"/> .
    /// </summary>
    public class Camera : ICamera
    {
        /// <summary>
        /// See <see cref="ICamera.Position"/> .
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// See <see cref="ICamera.Orientation"/> .
        /// </summary>
        public float Orientation { get; set; }

        /// <summary>
        /// See <see cref="ICamera.Scale"/> .
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// See <see cref="ICamera.GetCameraTransformation"/> .
        /// </summary>
        public ICameraTransformation GetCameraTransformation()
        {
            throw new NotImplementedException();
        }
    }
}

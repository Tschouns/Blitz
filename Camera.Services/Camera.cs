//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;

    /// <summary>
    /// See <see cref="ICamera"/> .
    /// </summary>
    public class Camera : ICamera
    {
        /// <summary>
        /// The width of the viewport.
        /// </summary>
        private double _viewportWidth;

        /// <summary>
        /// The height of the viewport.
        /// </summary>
        private double _viewportHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera(int viewportWidth, int viewportHeight)
        {
            Checks.AssertIsStrictPositive(viewportWidth, nameof(viewportWidth));
            Checks.AssertIsStrictPositive(viewportHeight, nameof(viewportHeight));

            this._viewportWidth = viewportWidth;
            this._viewportHeight = viewportHeight;
        }

        /// <summary>
        /// See <see cref="ICamera.Position"/>.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// See <see cref="ICamera.Orientation"/>.
        /// </summary>
        public double Orientation { get; set; }

        /// <summary>
        /// See <see cref="ICamera.Scale"/>.
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// See <see cref="ICamera.GetCameraTransformation"/>.
        /// </summary>
        public ICameraTransformation GetCameraTransformation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="ICamera.IsInView"/>.
        /// </summary>
        public bool IsInView(Point point)
        {
            throw new NotImplementedException();
        }
    }
}

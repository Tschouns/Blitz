//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using Base.RuntimeChecks;

    /// <summary>
    /// See <see cref="ICameraFactory"/>.
    /// </summary>
    public class CameraFactory : ICameraFactory
    {
        /// <summary>
        /// Creates a <see cref="ICamera"/>.
        /// </summary>
        public ICamera CreateCamera(int viewportWidth, int viewportHeight)
        {
            Checks.AssertIsStrictPositive(viewportWidth, nameof(viewportWidth));
            Checks.AssertIsStrictPositive(viewportHeight, nameof(viewportHeight));

            return new Camera(viewportWidth, viewportHeight);
        }
    }
}

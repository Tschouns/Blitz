//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using Base.RuntimeChecks;
    using global::Camera.CameraEffects;

    /// <summary>
    /// See <see cref="ICameraFactory"/>.
    /// </summary>
    public class CameraFactory : ICameraFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFactory"/> class.
        /// </summary>
        public CameraFactory(ICameraEffectCreator cameraEffectCreator)
        {
            Checks.AssertNotNull(cameraEffectCreator, nameof(cameraEffectCreator));

            this.CameraEffectCreator = cameraEffectCreator;
        }

        /// <summary>
        /// See <see cref="ICameraFactory.CameraEffectCreator"/>.
        /// </summary>
        public ICameraEffectCreator CameraEffectCreator { get; }

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

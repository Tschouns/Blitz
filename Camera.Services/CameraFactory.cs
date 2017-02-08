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
    using Geometry.Transformation;

    /// <summary>
    /// See <see cref="ICameraFactory"/>.
    /// </summary>
    public class CameraFactory : ICameraFactory
    {
        private readonly ITransformationFactory _transformationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFactory"/> class.
        /// </summary>
        public CameraFactory(
            ITransformationFactory transformationFactory,
            ICameraEffectCreator cameraEffectCreator)
        {
            Checks.AssertNotNull(transformationFactory, nameof(transformationFactory));
            Checks.AssertNotNull(cameraEffectCreator, nameof(cameraEffectCreator));

            this._transformationFactory = transformationFactory;
            this.CameraEffectCreator = cameraEffectCreator;
        }

        /// <summary>
        /// See <see cref="ICameraFactory.CameraEffectCreator"/>.
        /// </summary>
        public ICameraEffectCreator CameraEffectCreator { get; }

        /// <summary>
        /// Creates a <see cref="ICameraFactory.CreateCamera(int, int)"/>.
        /// </summary>
        public ICamera CreateCamera(int viewportWidth, int viewportHeight)
        {
            Checks.AssertIsStrictPositive(viewportWidth, nameof(viewportWidth));
            Checks.AssertIsStrictPositive(viewportHeight, nameof(viewportHeight));

            return new Camera(this._transformationFactory, viewportWidth, viewportHeight);
        }

        /// <summary>
        /// Creates a <see cref="ICameraFactory.CreateCameraController(ICamera)"/>.
        /// </summary>
        public ICameraController CreateCameraController(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            return new CameraController(camera);
        }
    }
}

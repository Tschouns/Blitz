//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using Camera.CameraEffects;

    /// <summary>
    /// Creates instances of <see cref="ICamera"/>.
    /// </summary>
    public interface ICameraFactory
    {
        /// <summary>
        /// Gets the <see cref="ICameraEffectCreator"/>.
        /// </summary>
        ICameraEffectCreator CameraEffectCreator { get; }

        /// <summary>
        /// Creates a <see cref="ICamera"/>.
        /// </summary>
        ICamera CreateCamera(int viewportWidth, int viewportHeight);

        /// <summary>
        /// Creates a <see cref="ICameraController"/> for the specified camera.
        /// </summary>
        ICameraController CreateCameraController(ICamera camera);
    }
}

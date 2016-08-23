//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using Camera.CameraEffects;

    // Controls a camera, using a set of camera effects.
    public interface ICameraController
    {
        /// <summary>
        /// Gets the camera controlled by this camera controller.
        /// </summary>
        ICamera Camera { get; }

        /// <summary>
        /// Adds a camera effect, which is then updated and applied to the camera.
        /// </summary>
        void AddEffect(ICameraEffect cameraEffect);

        /// <summary>
        /// Updates the camera controller and its effects, potentially with regard to
        /// elapsed time. This is supposed to be called once in the game/sim update loop.
        /// </summary>
        void Update(double timeElapsed);
    }
}

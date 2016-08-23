//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base.RuntimeChecks;
    using global::Camera.CameraEffects;

    /// <summary>
    /// See <see cref="ICameraController"/>.
    /// </summary>
    public class CameraController : ICameraController
    {
        /// <summary>
        /// Stores all the registered camera effects.
        /// </summary>
        private readonly IList<ICameraEffect> _cameraEffects = new List<ICameraEffect>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraController"/> class.
        /// </summary>
        public CameraController(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            this.Camera = camera;
        }

        /// <summary>
        /// See <see cref="ICameraController.Camera"/>.
        /// </summary>
        public ICamera Camera { get; }

        /// <summary>
        /// See <see cref="ICameraController.AddEffect(ICameraEffect)"/>.
        /// </summary>
        public void AddEffect(ICameraEffect cameraEffect)
        {
            Checks.AssertNotNull(cameraEffect, nameof(cameraEffect));

            if (this._cameraEffects.Contains(cameraEffect))
            {
                throw new ArgumentException("The specified camera effect has already been added.");
            }

            this._cameraEffects.Add(cameraEffect);
        }

        /// <summary>
        /// See <see cref="ICameraController.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            // Remove expired effects.
            var expiredEffects = this._cameraEffects.Where(aX => aX.HasExpired).ToList();
            foreach(var expiredEffect in expiredEffects)
            {
                this._cameraEffects.Remove(expiredEffect);
            }

            // Update and apply remaining effects.
            foreach(var cameraEffect in this._cameraEffects)
            {
                cameraEffect.Update(timeElapsed);
                cameraEffect.ApplyToCamera(this.Camera);
            }
        }
    }
}

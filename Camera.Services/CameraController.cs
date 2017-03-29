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
    using Base.Extensions;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;
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
            ArgumentChecks.AssertNotNull(camera, nameof(camera));

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
            ArgumentChecks.AssertNotNull(cameraEffect, nameof(cameraEffect));

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
            var expiredEffects = this._cameraEffects.Where(x => x.HasExpired).ToList();
            foreach(var expiredEffect in expiredEffects)
            {
                this._cameraEffects.Remove(expiredEffect);
            }

            // Update and apply remaining effects.
            var totalOffset = new CameraOffset();
            foreach (var cameraEffect in this._cameraEffects)
            {
                var effectOffset = cameraEffect.GetCameraOffset(this.Camera.State, timeElapsed);

                totalOffset.PositionOffset = totalOffset.PositionOffset.AddVector(effectOffset.PositionOffset);
                totalOffset.OrientationOffset += effectOffset.OrientationOffset;
                totalOffset.ScaleOffset += effectOffset.ScaleOffset;
            }

            var newCameraState = new CameraState()
            {
                Position = this.Camera.State.Position.AddVector(totalOffset.PositionOffset),
                Orientation = this.Camera.State.Orientation + totalOffset.OrientationOffset,
                Scale = this.Camera.State.Scale + totalOffset.ScaleOffset
            };

            // Make values "safe"... is this the right place to do this?
            newCameraState.Position = new Point(newCameraState.Position.X.Safe(), newCameraState.Position.Y.Safe());
            newCameraState.Orientation = newCameraState.Orientation.Safe();
            newCameraState.Scale = newCameraState.Scale.Safe();

            this.Camera.State = newCameraState;
        }
    }
}

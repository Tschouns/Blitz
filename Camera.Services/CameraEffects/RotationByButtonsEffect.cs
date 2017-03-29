//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using global::Camera.CameraEffects;
    using Input;
    using Input.Button;
    using Input.InputAction;

    /// <summary>
    /// A camera effect which rotates the camera around its center axis, when the user holds the
    /// button for the corresponing direction.
    /// </summary>
    public class RotationByButtonsEffect : ICameraEffect
    {
        /// <summary>
        /// Action which makes the camera rotate left
        /// </summary>
        private readonly IInputAction _rotateCameraLeftAction;

        /// <summary>
        /// Action which makes the camera rotate right
        /// </summary>
        private readonly IInputAction _rotateCameraRightAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotationByButtonsEffect"/> class.
        /// </summary>
        public RotationByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton rotateCameraLeftAction,
            IButton rotateCameraRightAction,
            double rotationSpeed)
        {
            ArgumentChecks.AssertNotNull(rotateCameraLeftAction, nameof(rotateCameraLeftAction));
            ArgumentChecks.AssertNotNull(rotateCameraRightAction, nameof(rotateCameraRightAction));
            ArgumentChecks.AssertIsPositive(rotationSpeed, nameof(rotationSpeed));

            this._rotateCameraLeftAction = inputActionManager.RegisterButtonHoldAction(rotateCameraLeftAction);
            this._rotateCameraRightAction = inputActionManager.RegisterButtonHoldAction(rotateCameraRightAction);
            this.RotationSpeed = rotationSpeed;
        }

        /// <summary>
        /// Gets or sets the camera rotation speed.
        /// </summary>
        public double RotationSpeed { get; }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>. This effect does not expire.
        /// </summary>
        public bool HasExpired => false;

        /// <summary>
        /// See <see cref="ICameraEffect.GetCameraOffset(CameraState, double)"/>.
        /// </summary>
        public CameraOffset GetCameraOffset(CameraState cameraState, double timeElapsed)
        {
            var rotationAngle = this.RotationSpeed * timeElapsed;
            var orientationOffset = 0.0d;

            if (this._rotateCameraLeftAction.IsActive)
            {
                // Means we turn the world clockwise...
                orientationOffset = rotationAngle;
            }

            if (this._rotateCameraRightAction.IsActive)
            {
                // Means we turn the world clockwise...
                orientationOffset = -rotationAngle;
            }

            return new CameraOffset()
            {
                OrientationOffset = orientationOffset
            };
        }
    }
}

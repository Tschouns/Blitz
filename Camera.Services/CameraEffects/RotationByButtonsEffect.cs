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
        /// Action which makes the camera rotate clockwise (means the world "turns" counter-clockwise).
        /// </summary>
        private readonly IInputAction _rotateCameraClockwiseAction;

        /// <summary>
        /// Action which makes the camera rotate counter-clockwise (means the world "turns" clockwise).
        /// </summary>
        private readonly IInputAction _rotateCameraCounterClockwiseRightAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotationByButtonsEffect"/> class.
        /// </summary>
        public RotationByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton rotateCameraClockwiseAction,
            IButton rotateCameraCounterClockwiseRightAction,
            double rotationSpeed)
        {
            Checks.AssertNotNull(rotateCameraClockwiseAction, nameof(rotateCameraClockwiseAction));
            Checks.AssertNotNull(rotateCameraCounterClockwiseRightAction, nameof(rotateCameraCounterClockwiseRightAction));
            Checks.AssertIsPositive(rotationSpeed, nameof(rotationSpeed));

            this._rotateCameraClockwiseAction = inputActionManager.RegisterButtonHoldAction(rotateCameraClockwiseAction);
            this._rotateCameraCounterClockwiseRightAction = inputActionManager.RegisterButtonHoldAction(rotateCameraCounterClockwiseRightAction);
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

            if (this._rotateCameraClockwiseAction.IsActive)
            {
                orientationOffset = rotationAngle;
            }

            if (this._rotateCameraCounterClockwiseRightAction.IsActive)
            {
                orientationOffset = -rotationAngle;
            }

            return new CameraOffset()
            {
                OrientationOffset = orientationOffset
            };
        }
    }
}

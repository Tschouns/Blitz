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
    using Geometry.Transformation;

    /// <summary>
    /// A camera effect which moves the camera position along the axes, when the user
    /// holds the button for the corresponing direction.
    /// </summary>
    public class PositionAbsoluteByButtonsEffect : ICameraEffect
    {
        /// <summary>
        /// Action which makes the camera move up.
        /// </summary>
        private readonly IInputAction _moveCameraUpAction;

        /// <summary>
        /// Action which makes the camera move down.
        /// </summary>
        private readonly IInputAction _moveCameraDownAction;

        /// <summary>
        /// Action which makes the camera move left.
        /// </summary>
        private readonly IInputAction _moveCameraLeftAction;

        /// <summary>
        /// Action which makes the camera move right.
        /// </summary>
        private readonly IInputAction _moveCameraRightAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionAbsoluteByButtonsEffect"/> class.
        /// </summary>
        public PositionAbsoluteByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton moveCameraUp,
            IButton moveCameraDown,
            IButton moveCameraLeft,
            IButton moveCameraRight,
            double movingSpeed)
        {
            Checks.AssertNotNull(moveCameraUp, nameof(moveCameraUp));
            Checks.AssertNotNull(moveCameraDown, nameof(moveCameraDown));
            Checks.AssertNotNull(moveCameraLeft, nameof(moveCameraLeft));
            Checks.AssertNotNull(moveCameraRight, nameof(moveCameraRight));
            Checks.AssertIsPositive(movingSpeed, nameof(movingSpeed));

            this._moveCameraUpAction = inputActionManager.RegisterButtonHoldAction(moveCameraUp);
            this._moveCameraDownAction = inputActionManager.RegisterButtonHoldAction(moveCameraDown);
            this._moveCameraLeftAction = inputActionManager.RegisterButtonHoldAction(moveCameraLeft);
            this._moveCameraRightAction = inputActionManager.RegisterButtonHoldAction(moveCameraRight);
            this.MovingSpeed = movingSpeed;
        }

        /// <summary>
        /// Gets or sets the camera moving speed.
        /// </summary>
        public double MovingSpeed { get; set; }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>. This effect does not expire.
        /// </summary>
        public bool HasExpired => false;

        /// <summary>
        /// See <see cref="ICameraEffect.GetCameraOffset(CameraState, double)"/>.
        /// </summary>
        public CameraOffset GetCameraOffset(CameraState cameraState, double timeElapsed)
        {
            var movingDistance = this.MovingSpeed * timeElapsed;
            var relativePositionOffset = new Vector2();

            if (this._moveCameraUpAction.IsActive)
            {
                relativePositionOffset.Y += movingDistance;
            }

            if (this._moveCameraDownAction.IsActive)
            {
                relativePositionOffset.Y -= movingDistance;
            }

            if (this._moveCameraLeftAction.IsActive)
            {
                relativePositionOffset.X -= movingDistance;
            }

            if (this._moveCameraRightAction.IsActive)
            {
                relativePositionOffset.X += movingDistance;
            }

            // Now we account for the camera's current orientation, and rotate the offset vector accordingly...
            var rotation = Matrix3x3.CreateRotation(cameraState.Orientation);
            var absolutePositionOffset = TransformationUtils.TransformVector(relativePositionOffset, rotation);

            return new CameraOffset()
            {
                PositionOffset = absolutePositionOffset
            };
        }
    }
}

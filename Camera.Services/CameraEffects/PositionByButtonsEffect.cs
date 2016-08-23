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
    /// A camera effect which moves the camera position along the axes, when the user
    /// holds the button for the corresponing direction.
    /// </summary>
    public class PositionByButtonsEffect : ICameraEffect
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
        /// Stores the distance by which the camera is moved, if the effect is applied.
        /// </summary>
        private double _movingDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionByButtonsEffect"/> class.
        /// </summary>
        public PositionByButtonsEffect(
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
        /// See <see cref="ICameraEffect.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            this._movingDistance = this.MovingSpeed * timeElapsed;
        }

        /// <summary>
        /// See <see cref="ICameraEffect.ApplyToCamera(double)"/>.
        /// </summary>
        public void ApplyToCamera(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            ApplyAction(camera, this._moveCameraUpAction, p => new Point(p.X, p.Y + this._movingDistance));
            ApplyAction(camera, this._moveCameraDownAction, p => new Point(p.X, p.Y - this._movingDistance));
            ApplyAction(camera, this._moveCameraLeftAction, p => new Point(p.X - this._movingDistance, p.Y));
            ApplyAction(camera, this._moveCameraRightAction, p => new Point(p.X + this._movingDistance, p.Y));
        }

        /// <summary>
        /// Private helper method. Simplifies changing the camera position.
        /// </summary>
        private static void ApplyAction(ICamera camera, IInputAction action, Func<Point, Point> ChangePositionFunc)
        {
            if (action.IsActive)
            {
                camera.Position = ChangePositionFunc(camera.Position);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using Base.RuntimeChecks;
    using global::Camera.CameraEffects;
    using Input;
    using Input.Button;
    using Input.InputAction;

    /// <summary>
    /// A camera effect which increases and decreases the camera scale linearly, when
    /// the user holds the button for the corresponing direction.
    /// </summary>
    public class ScaleLinearByButtonsEffect : ICameraEffect
    {
        /// <summary>
        /// Action which increases the camera scale.
        /// </summary>
        private readonly IInputAction _increaseScaleAction;

        /// <summary>
        /// Action which decreases the camera scale.
        /// </summary>
        private readonly IInputAction _decreaseScaleAction;

        /// <summary>
        /// Represents the lower limit of the scale factor.
        /// </summary>
        private double _scaleLowerLimit;

        /// <summary>
        /// Represents the upper limit of the scale factor.
        /// </summary>
        private double _scaleUpperLimit;

        /// <summary>
        /// Stores the difference by which the scale factor increases or decreases (linearly).
        /// </summary>
        private double _scaleDifference;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleLinearByButtonsEffect"/> class.
        /// </summary>
        public ScaleLinearByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton increaseScale,
            IButton decreaseScale,
            double scaleLowerLimit,
            double scaleUpperLimit,
            double scaleSpeed)
        {
            Checks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            Checks.AssertNotNull(increaseScale, nameof(increaseScale));
            Checks.AssertNotNull(decreaseScale, nameof(decreaseScale));
            Checks.AssertIsStrictPositive(scaleLowerLimit, nameof(scaleLowerLimit));
            Checks.AssertIsStrictPositive(scaleUpperLimit, nameof(scaleUpperLimit));

            this._increaseScaleAction = inputActionManager.RegisterButtonHoldAction(increaseScale);
            this._decreaseScaleAction = inputActionManager.RegisterButtonHoldAction(decreaseScale);
            this._scaleLowerLimit = scaleLowerLimit;
            this._scaleUpperLimit = scaleUpperLimit;
            this.ScaleSpeed = scaleSpeed;
        }

        /// <summary>
        /// Gets or sets the amount by which the camera scale increases/decreases (linearly) per second.
        /// </summary>
        public double ScaleSpeed { get; set; }

        /// <summary>
        /// See <see cref="ICameraEffect.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            this._scaleDifference = this.ScaleSpeed * timeElapsed;
        }

        /// <summary>
        /// See <see cref="ICameraEffect.ApplyToCamera(double)"/>.
        /// </summary>
        public void ApplyToCamera(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            // Increase/decrease scale.
            if (this._increaseScaleAction.IsActive)
            {
                camera.Scale += this._scaleDifference;
            }

            if (this._decreaseScaleAction.IsActive)
            {
                camera.Scale -= this._scaleDifference;
            }

            // Apply limits.
            if (camera.Scale < this._scaleLowerLimit)
            {
                camera.Scale = this._scaleLowerLimit;
            }

            if (camera.Scale > this._scaleUpperLimit)
            {
                camera.Scale = this._scaleUpperLimit;
            }
        }
    }
}

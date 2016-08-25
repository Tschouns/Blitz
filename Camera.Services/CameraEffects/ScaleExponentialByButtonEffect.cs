//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;
    using Base.RuntimeChecks;
    using global::Camera.CameraEffects;
    using Input;
    using Input.Button;
    using Input.InputAction;

    /// <summary>
    /// A camera effect which increases and decreases the camera scale exponentially (which creates
    /// the impression of the camera changing its "altitude" linearly).
    /// </summary>
    public class ScaleExponentialByButtonEffect : ICameraEffect
    {
        /// <summary>
        /// Helper which provides various helper methods.
        /// </summary>
        private readonly ICameraEffectHelper _helper;

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
        /// Initializes a new instance of the <see cref="ScaleLinearByButtonsEffect"/> class.
        /// </summary>
        public ScaleExponentialByButtonEffect(
            ICameraEffectHelper helper,
            IInputActionManager inputActionManager,
            IButton increaseScale,
            IButton decreaseScale,
            double scaleLowerLimit,
            double scaleUpperLimit,
            double normScaleSpeed)
        {
            Checks.AssertNotNull(helper, nameof(helper));
            Checks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            Checks.AssertNotNull(increaseScale, nameof(increaseScale));
            Checks.AssertNotNull(decreaseScale, nameof(decreaseScale));
            Checks.AssertIsStrictPositive(scaleLowerLimit, nameof(scaleLowerLimit));
            Checks.AssertIsStrictPositive(scaleUpperLimit, nameof(scaleUpperLimit));

            this._helper = helper;
            this._increaseScaleAction = inputActionManager.RegisterButtonHoldAction(increaseScale);
            this._decreaseScaleAction = inputActionManager.RegisterButtonHoldAction(decreaseScale);
            this._scaleLowerLimit = scaleLowerLimit;
            this._scaleUpperLimit = scaleUpperLimit;
            this.NormScaleSpeed = normScaleSpeed;
        }

        /// <summary>
        /// Gets or sets the speed by which the camera scale increases/decreases at a scale of 1.0.
        /// </summary>
        public double NormScaleSpeed { get; set; }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>. This effect does not expire.
        /// </summary>
        public bool HasExpired => false;

        /// <summary>
        /// See <see cref="ICameraEffect.GetCameraOffset(CameraState, double)"/>.
        /// </summary>
        public CameraOffset GetCameraOffset(CameraState cameraState, double timeElapsed)
        {
            var scaleDifference = this.NormScaleSpeed * timeElapsed * cameraState.Scale;

            var offset = new CameraOffset();

            // Increase/decrease scale.
            if (this._increaseScaleAction.IsActive)
            {
                offset.ScaleOffset += scaleDifference;
            }

            if (this._decreaseScaleAction.IsActive)
            {
                offset.ScaleOffset -= scaleDifference;
            }

            // Apply correction according limits.
            var expectedLimitViolation = this._helper.OutOfBounds(offset.ScaleOffset + cameraState.Scale, this._scaleLowerLimit, this._scaleUpperLimit);
            offset.ScaleOffset -= expectedLimitViolation;

            return offset;
        }
    }
}

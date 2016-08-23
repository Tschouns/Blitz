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
    /// A camera effect which increases and decreases the camera scale exponentially (which creates
    /// the impression of the camera changing its "altitude" linearly).
    /// </summary>
    public class ScaleExponentialByButtonEffect : ICameraEffect
    {
        /// <summary>
        /// The actual effect, which is wrapped and slightly modified by this class.
        /// </summary>
        private readonly ScaleLinearByButtonsEffect _scaleLinearByButtonEffect;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleLinearByButtonsEffect"/> class.
        /// </summary>
        public ScaleExponentialByButtonEffect(
            ScaleLinearByButtonsEffect scaleByButtonEffect,
            double normScaleSpeed)
        {
            Checks.AssertNotNull(scaleByButtonEffect, nameof(scaleByButtonEffect));

            this._scaleLinearByButtonEffect = scaleByButtonEffect;
            this.NormScaleSpeed = normScaleSpeed;

            // We initialize the effect with the norm scale speed, which is strictly not correct, as the camera scale is not known. But it won't matter much.
            this._scaleLinearByButtonEffect.ScaleSpeed = this.NormScaleSpeed;
        }

        /// <summary>
        /// Gets or sets the speed by which the camera scale increases/decreases at a scale of 1.0.
        /// </summary>
        public double NormScaleSpeed { get; set; }

        /// <summary>
        /// See <see cref="ICameraEffect.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            this._scaleLinearByButtonEffect.Update(timeElapsed);
        }

        /// <summary>
        /// See <see cref="ICameraEffect.ApplyToCamera(double)"/>.
        /// </summary>
        public void ApplyToCamera(ICamera camera)
        {
            this._scaleLinearByButtonEffect.ApplyToCamera(camera);
            
            // Update the effect based on the current camera scale.
            this._scaleLinearByButtonEffect.ScaleSpeed = this.NormScaleSpeed * camera.Scale;
        }
    }
}

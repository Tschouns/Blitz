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
    using Geometry.Extensions;
    using global::Camera.CameraEffects;

    /// <summary>
    /// A camera effect which applies an oscillation to the camera position, as caused by a "blow".
    /// </summary>
    public class PositionBlowOscillationEffect : ICameraEffect
    {
        /// <summary>
        /// Stores the blow oscillation, which does the actual calculation.
        /// </summary>
        private readonly IBlowOscillation _blowOscillation;

        /// <summary>
        /// Stores the current distance and direction by which the camera is moved, if the effect is applied.
        /// </summary>
        private Vector2 _movingDistance;

        /// <summary>
        /// Initializes a new instance if the <see cref="PositionBlowOscillationEffect"/> class.
        /// </summary>
        /// <param name="blowOscillation"></param>
        public PositionBlowOscillationEffect(IBlowOscillation blowOscillation)
        {
            Checks.AssertNotNull(blowOscillation, nameof(blowOscillation));

            this._blowOscillation = blowOscillation;
            this._movingDistance = new Vector2();
        }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>.
        /// </summary>
        public bool HasExpired => this._blowOscillation.HasDepleted;

        /// <summary>
        /// See <see cref="ICameraEffect.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            var oscillationBefore = this._blowOscillation.CurrentOscillation;
            this._blowOscillation.Update(timeElapsed);
            var oscillationNow = this._blowOscillation.CurrentOscillation;

            this._movingDistance = oscillationNow.SubtactVector(oscillationBefore);
        }

        /// <summary>
        /// See <see cref="ICameraEffect.ApplyToCamera(ICamera)"/>.
        /// </summary>
        public void ApplyToCamera(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            camera.Position = camera.Position.AddVector(this._movingDistance);
        }
    }
}

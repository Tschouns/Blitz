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
        /// Initializes a new instance if the <see cref="PositionBlowOscillationEffect"/> class.
        /// </summary>
        public PositionBlowOscillationEffect(IBlowOscillation blowOscillation)
        {
            ArgumentChecks.AssertNotNull(blowOscillation, nameof(blowOscillation));

            this._blowOscillation = blowOscillation;
        }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>.
        /// </summary>
        public bool HasExpired => this._blowOscillation.HasDepleted;

        /// <summary>
        /// See <see cref="ICameraEffect.GetCameraOffset(CameraState, double)"/>.
        /// </summary>
        public CameraOffset GetCameraOffset(CameraState cameraState, double timeElapsed)
        {
            var oscillationBefore = this._blowOscillation.CurrentOscillation;
            this._blowOscillation.Update(timeElapsed);
            var oscillationNow = this._blowOscillation.CurrentOscillation;

            var positionOffset = oscillationNow.SubtactVector(oscillationBefore);

            return new CameraOffset()
            {
                PositionOffset = positionOffset
            };
        }
    }
}

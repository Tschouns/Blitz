//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using global::Camera.CameraEffects;
    using Input.Button;
    using Input.InputAction;

    /// <summary>
    /// See <see cref="ICameraEffectCreator"/>.
    /// </summary>
    public class CameraEffectCreator : ICameraEffectCreator
    {
        /// <summary>
        /// Stores the <see cref="ICameraEffectHelper"/> which is used by various effects.
        /// </summary>
        private readonly ICameraEffectHelper _cameraEffectHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraEffectCreator"/> class.
        /// </summary>
        public CameraEffectCreator(ICameraEffectHelper cameraEffectHelper)
        {
            Checks.AssertNotNull(cameraEffectHelper, nameof(cameraEffectHelper));

            this._cameraEffectHelper = cameraEffectHelper;
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreatePositionByButtonsEffect(IInputAction, IInputAction, IInputAction, IInputAction, double)"/>.
        /// </summary>
        public ICameraEffect CreatePositionByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton moveCameraUp,
            IButton moveCameraDown,
            IButton moveCameraLeft,
            IButton moveCameraRight,
            double movingSpeed)
        {
            Checks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            Checks.AssertNotNull(moveCameraUp, nameof(moveCameraUp));
            Checks.AssertNotNull(moveCameraDown, nameof(moveCameraDown));
            Checks.AssertNotNull(moveCameraLeft, nameof(moveCameraLeft));
            Checks.AssertNotNull(moveCameraRight, nameof(moveCameraRight));

            return new PositionAbsoluteByButtonsEffect(
                inputActionManager,
                moveCameraUp,
                moveCameraDown,
                moveCameraLeft,
                moveCameraRight,
                movingSpeed);
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreateScaleLinearByButtonsEffect(IInputActionManager, IButton, IButton, double, double, double)"/>.
        /// </summary>
        public ICameraEffect CreateScaleLinearByButtonsEffect(
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

            return new ScaleLinearByButtonsEffect(
                this._cameraEffectHelper,
                inputActionManager,
                increaseScale,
                decreaseScale,
                scaleLowerLimit,
                scaleUpperLimit,
                scaleSpeed);
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreateScaleExponentialByButtonsEffect(IInputActionManager, IButton, IButton, double, double, double)" />.
        /// </summary>
        public ICameraEffect CreateScaleExponentialByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton increaseScale, 
            IButton decreaseScale, 
            double scaleLowerLimit,
            double scaleUpperLimit,
            double normScaleSpeed)
        {
            Checks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            Checks.AssertNotNull(increaseScale, nameof(increaseScale));
            Checks.AssertNotNull(decreaseScale, nameof(decreaseScale));
            Checks.AssertIsStrictPositive(scaleLowerLimit, nameof(scaleLowerLimit));
            Checks.AssertIsStrictPositive(scaleUpperLimit, nameof(scaleUpperLimit));
            
            return new ScaleExponentialByButtonEffect(
                this._cameraEffectHelper,
                inputActionManager,
                increaseScale,
                decreaseScale,
                scaleLowerLimit,
                scaleUpperLimit,
                normScaleSpeed);
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreatePositionBlowOscillationEffect(Vector2, double, double)"/>.
        /// </summary>
        public ICameraEffect CreatePositionBlowOscillationEffect(
            Vector2 startOscillation,
            double duration,
            double frequency)
        {
            var blowOscillation = new BlowOscillation(
                startOscillation,
                duration,
                frequency);

            return new PositionBlowOscillationEffect(blowOscillation);
        }
    }
}

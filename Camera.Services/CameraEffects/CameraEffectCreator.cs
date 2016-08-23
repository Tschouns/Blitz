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
    using global::Camera.CameraEffects;
    using Input.Button;
    using Input.InputAction;

    /// <summary>
    /// See <see cref="ICameraEffectCreator"/>.
    /// </summary>
    public class CameraEffectCreator : ICameraEffectCreator
    {
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

            return new PositionByButtonsEffect(
                inputActionManager,
                moveCameraUp,
                moveCameraDown,
                moveCameraLeft,
                moveCameraRight,
                movingSpeed);
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreateScaleByButtonsEffect(IInputActionManager, IButton, IButton, double, double, double)"/>.
        /// </summary>
        public ICameraEffect CreateScaleByButtonsEffect(
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

            return new ScaleByButtonsEffect(
                inputActionManager,
                increaseScale,
                decreaseScale,
                scaleLowerLimit,
                scaleUpperLimit,
                scaleSpeed);
        }
    }
}

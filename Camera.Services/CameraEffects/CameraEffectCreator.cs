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
            IInputAction moveCameraUp,
            IInputAction moveCameraDown,
            IInputAction moveCameraLeft,
            IInputAction moveCameraRight,
            double movingSpeed)
        {
            Checks.AssertNotNull(moveCameraUp, nameof(moveCameraUp));
            Checks.AssertNotNull(moveCameraDown, nameof(moveCameraDown));
            Checks.AssertNotNull(moveCameraLeft, nameof(moveCameraLeft));
            Checks.AssertNotNull(moveCameraRight, nameof(moveCameraRight));
            Checks.AssertIsPositive(movingSpeed, nameof(movingSpeed));

            return new PositionByButtonsAxisAlignedEffect(
                moveCameraUp,
                moveCameraDown,
                moveCameraLeft,
                moveCameraRight,
                movingSpeed);
        }
    }
}

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
            throw new NotImplementedException();
        }
    }
}

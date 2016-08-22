//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.CameraEffects
{
    using Input.InputAction;

    /// <summary>
    /// Creates camera effects.
    /// </summary>
    public interface ICameraEffectCreator
    {
        /// <summary>
        /// Creates a camera effect which moves the camera position along the axes, when the user
        /// holds the button for the corresponing direction.
        /// </summary>
        ICameraEffect CreatePositionByButtonsEffect(
            IInputAction moveCameraUp,
            IInputAction moveCameraDown,
            IInputAction moveCameraLeft,
            IInputAction moveCameraRight,
            double movingSpeed);
    }
}

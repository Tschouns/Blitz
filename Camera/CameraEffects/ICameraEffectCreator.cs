//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.CameraEffects
{
    using Input.Button;
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
            IInputActionManager inputActionManager,
            IButton moveCameraUp,
            IButton moveCameraDown,
            IButton moveCameraLeft,
            IButton moveCameraRight,
            double movingSpeed);

        /// <summary>
        /// Creates a camera effect which increases and decreases the camera scale linearly, when
        /// the user holds the button for the corresponing direction.
        /// </summary>
        ICameraEffect CreateScaleLinearByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton increaseScale,
            IButton decreaseScale,
            double scaleLowerLimit,
            double scaleUpperLimit,
            double scaleSpeed);

        /// <summary>
        /// Creates a camera effect which increases and decreases the camera scale exponentially (which creates
        /// the impression of the camera changing its "altitude" linearly).
        /// </summary>
        ICameraEffect CreateScaleExponentialByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton increaseScale,
            IButton decreaseScale,
            double scaleLowerLimit,
            double scaleUpperLimit,
            double normScaleSpeed);
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.CameraEffects
{
    using System;
    using Geometry.Elements;
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
        ICameraEffect CreatePositionAbsoluteByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton moveCameraUp,
            IButton moveCameraDown,
            IButton moveCameraLeft,
            IButton moveCameraRight,
            double movingSpeed);

        /// <summary>
        /// Creates a camera effect which makes the camera follow an object or character in the world.
        /// </summary>
        /// <typeparam name="TFollowed">
        /// The type of the object the camera is supposed to follow
        /// </typeparam>
        ICameraEffect CreatePositionFollowEffect<TFollowed>(
            TFollowed followedObject,
            Func<TFollowed, Point> retrieveCurrentFollowedObjectPositionFunc,
            Func<bool> determineIsExpiredFunc)
            where TFollowed : class;

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

        /// <summary>
        /// Creates a camera effect which applies an oscillation to the camera position, as caused by a "blow".
        /// </summary>
        ICameraEffect CreatePositionBlowOscillationEffect(
            Vector2 startOscillation,
            double duration,
            double frequency);

        /// <summary>
        /// Creates a camera effect which rotates the camera around its center axis, when the user
        /// holds the button for the corresponing direction.
        /// </summary>
        /// <returns></returns>
        ICameraEffect CreateRotationByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton rotateCameraLeftAction,
            IButton rotateCameraRightRightAction,
            double rotationSpeed);
    }
}

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
            ArgumentChecks.AssertNotNull(cameraEffectHelper, nameof(cameraEffectHelper));

            this._cameraEffectHelper = cameraEffectHelper;
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreatePositionAbsoluteByButtonsEffect(IInputActionManager, IButton, IButton, IButton, IButton, double)"/>.
        /// </summary>
        public ICameraEffect CreatePositionAbsoluteByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton moveCameraUp,
            IButton moveCameraDown,
            IButton moveCameraLeft,
            IButton moveCameraRight,
            double movingSpeed)
        {
            ArgumentChecks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            ArgumentChecks.AssertNotNull(moveCameraUp, nameof(moveCameraUp));
            ArgumentChecks.AssertNotNull(moveCameraDown, nameof(moveCameraDown));
            ArgumentChecks.AssertNotNull(moveCameraLeft, nameof(moveCameraLeft));
            ArgumentChecks.AssertNotNull(moveCameraRight, nameof(moveCameraRight));

            return new PositionAbsoluteByButtonsEffect(
                inputActionManager,
                moveCameraUp,
                moveCameraDown,
                moveCameraLeft,
                moveCameraRight,
                movingSpeed);
        }

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreatePositionFollowEffect{TFollowed}(TFollowed, Func{TFollowed, Point}, Func{bool})"/>.
        /// </summary>
        public ICameraEffect CreatePositionFollowEffect<TFollowed>(
            TFollowed followedObject,
            Func<TFollowed, Point> retrieveCurrentFollowedObjectPositionFunc,
            Func<bool> determineIsExpiredFunc)
            where TFollowed : class
        {
            ArgumentChecks.AssertNotNull(followedObject, nameof(followedObject));
            ArgumentChecks.AssertNotNull(retrieveCurrentFollowedObjectPositionFunc, nameof(retrieveCurrentFollowedObjectPositionFunc));
            ArgumentChecks.AssertNotNull(determineIsExpiredFunc, nameof(determineIsExpiredFunc));

            return new PositionFollowEffect<TFollowed>(
                followedObject,
                retrieveCurrentFollowedObjectPositionFunc,
                determineIsExpiredFunc);
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
            ArgumentChecks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            ArgumentChecks.AssertNotNull(increaseScale, nameof(increaseScale));
            ArgumentChecks.AssertNotNull(decreaseScale, nameof(decreaseScale));
            ArgumentChecks.AssertIsStrictPositive(scaleLowerLimit, nameof(scaleLowerLimit));
            ArgumentChecks.AssertIsStrictPositive(scaleUpperLimit, nameof(scaleUpperLimit));

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
            ArgumentChecks.AssertNotNull(inputActionManager, nameof(inputActionManager));
            ArgumentChecks.AssertNotNull(increaseScale, nameof(increaseScale));
            ArgumentChecks.AssertNotNull(decreaseScale, nameof(decreaseScale));
            ArgumentChecks.AssertIsStrictPositive(scaleLowerLimit, nameof(scaleLowerLimit));
            ArgumentChecks.AssertIsStrictPositive(scaleUpperLimit, nameof(scaleUpperLimit));
            
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

        /// <summary>
        /// See <see cref="ICameraEffectCreator.CreateRotationByButtonsEffect"/>.
        /// </summary>
        public ICameraEffect CreateRotationByButtonsEffect(
            IInputActionManager inputActionManager,
            IButton rotateCameraLeftAction,
            IButton rotateCameraRightAction,
            double rotationSpeed)
        {
            return new RotationByButtonsEffect(
                inputActionManager,
                rotateCameraLeftAction,
                rotateCameraRightAction,
                rotationSpeed);
        }
    }
}

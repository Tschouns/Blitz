//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services.InputAction
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// See <see cref="IInputAction"/>. This type of action becomes active for a single update cycle when
    /// a certain button is pressed. The user has to release and press the button anew in order for
    /// the action to become active again.
    /// </summary>
    public class KeyboardButtonHitInputAction : IInputActionInternal
    {
        /// <summary>
        /// Stores the key this action represents.
        /// </summary>
        private readonly Key _key;

        /// <summary>
        /// Becomes true when the pressing of a button has been processed, i.e. when the action has been
        /// active for an update cycle. It is reset when the button is released.
        /// </summary>
        private bool _isAlreadyProcessed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButtonHitInputAction"/> class.
        /// </summary>
        public KeyboardButtonHitInputAction(Key key)
        {
            this._key = key;
        }

        /// <summary>
        /// See <see cref="IInputAction.IsActive"/>.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// See <see cref="IInputActionInternal"/>.
        /// </summary>
        public void Update(double realTimeElapsed)
        {
            if (Keyboard.IsKeyDown(this._key))
            {
                this.IsActive = !this._isAlreadyProcessed;
                this._isAlreadyProcessed = true;
            }
            else
            {
                this.IsActive = false;
                this._isAlreadyProcessed = false;
            }
        }
    }
}

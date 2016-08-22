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
    /// See <see cref="IInputAction"/>. This type of action is active while a certain keyboard button is pressed.
    /// </summary>
    public class KeyboardButtonHoldInputAction : IInputActionInternal
    {
        /// <summary>
        /// Stores the key this action represents.
        /// </summary>
        private readonly Key _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButtonHoldInputAction"/> class.
        /// </summary>
        public KeyboardButtonHoldInputAction(Key key)
        {
            this._key = key;
        }

        /// <summary>
        /// See <see cref="IInputAction"/>.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// See <see cref="IInputActionInternal"/>.
        /// </summary>
        public void Update(double realTimeElapsed)
        {
            this.IsActive = Keyboard.IsKeyDown(this._key);
        }
    }
}

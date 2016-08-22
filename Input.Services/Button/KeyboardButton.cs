//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services.Button
{
    using System.Windows.Input;
    using Input.Button;

    /// <summary>
    /// See <see cref="IButton"/>.
    /// </summary>
    public class KeyboardButton : IButton
    {
        /// <summary>
        /// The actual key represented by this object.
        /// </summary>
        private readonly Key _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButton"/>.
        /// </summary>
        public KeyboardButton(Key key)
        {
            this._key = key;
        }

        /// <summary>
        /// See <see cref="IButton.IsPressed"/>.
        /// </summary>
        public bool IsPressed
        {
            get
            {
                return Keyboard.IsKeyDown(this._key);
            }
        }
    }
}

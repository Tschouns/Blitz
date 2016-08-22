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
    /// See <see cref="IKeyboardButtonCreator"/>.
    /// </summary>
    public class KeyboardButtonCreator : IKeyboardButtonCreator
    {
        /// <summary>
        /// See <see cref="IKeyboardButtonCreator.Create(Key)"/>.
        /// </summary>
        public IButton Create(Key key)
        {
            return new KeyboardButton(key);
        }
    }
}

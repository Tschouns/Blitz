//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Windows.Input;

namespace Input.Button
{
    /// <summary>
    /// Creates <see cref="IButton"/> objects representing keyboard buttons.
    /// </summary>
    public interface IKeyboardButtonCreator
    {
        /// <summary>
        /// Creates a button.
        /// </summary>
        IButton Create(Key key);
    }
}

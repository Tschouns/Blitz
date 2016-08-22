//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Button
{
    /// <summary>
    /// Represents a button, e.g. on a keyboard or a mouse.
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// Gets a value indicating whether the button is currently being pressed.
        /// </summary>
        bool IsPressed { get; }
    }
}

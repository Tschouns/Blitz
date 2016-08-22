//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input
{
    /// <summary>
    /// Represents a user input action, such as a keyboard button which is pressed.
    /// </summary>
    public interface IInputAction
    {
        /// <summary>
        /// Gets a value indicating whether the input action is currently active.
        /// </summary>
        bool IsActive { get; }
    }
}

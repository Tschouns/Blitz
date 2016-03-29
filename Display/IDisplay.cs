//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using System;

    /// <summary>
    /// Represents the a display.
    /// </summary>
    public interface IDisplay : IDisposable
    {
        /// <summary>
        /// Shows the display.
        /// </summary>
        void Show();

        /// <summary>
        /// Draws a frame, calls the call-back delegate. Returns a value indicating whether the display
        /// still exists.
        /// </summary>
        bool DrawFrame();
    }
}

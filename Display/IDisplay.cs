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
        /// Draws a frame, calls the call-back delegate.
        /// </summary>
        void Draw();
    }
}

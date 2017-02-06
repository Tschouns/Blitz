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
        /// Gets the display properties.
        /// </summary>
        DisplayProperties Properties { get; }

        /// <summary>
        /// Gets the <see cref="ISpriteManager"/>, which allows to load and draw sprites to the display
        /// while the frame is being drawn.
        /// </summary>
        ISpriteManager SpriteManager { get; }

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

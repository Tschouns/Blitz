//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using System.Drawing;

    /// <summary>
    /// Defines a set of properties of an <see cref="IDisplay"/> instance.
    /// </summary>
    public struct DisplayProperties
    {
        /// <summary>
        /// Gets or sets the display title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        public Size Resolution { get; set; }
    }
}

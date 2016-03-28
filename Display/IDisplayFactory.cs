//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using System;

    /// <summary>
    /// Creates the <see cref="IDisplay"/>.
    /// </summary>
    public interface IDisplayFactory
    {
        /// <summary>
        /// Creates a <see cref="IDisplay"/>.
        /// </summary>
        IDisplay CreateDisplay(DisplayProperties properties, Action<IDrawingContext> drawCallback);
    }
}

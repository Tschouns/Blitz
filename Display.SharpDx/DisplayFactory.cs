//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx
{
    using System;
    using Base.RuntimeChecks;

    /// <summary>
    /// See <see cref="IDisplayFactory"/>.
    /// </summary>
    public class DisplayFactory : IDisplayFactory
    {
        /// <summary>
        /// See <see cref="IDisplayFactory.CreateDisplay"/>.
        /// </summary>
        public IDisplay CreateDisplay(DisplayProperties properties, Action<IDrawingContext> drawCallback)
        {
            Checks.AssertNotNull(drawCallback, nameof(drawCallback));

            return new Display(properties, drawCallback);
        }
    }
}

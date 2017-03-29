//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Display
{
    using System;
    using Base.RuntimeChecks;
    using Sprites;

    /// <summary>
    /// See <see cref="IDisplayFactory"/>.
    /// </summary>
    public class DisplayFactory : IDisplayFactory
    {
        private readonly IBitmapLoader _bitmapLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayFactory"/> class.
        /// </summary>
        public DisplayFactory(IBitmapLoader bitmapLoader)
        {
            ArgumentChecks.AssertNotNull(bitmapLoader, nameof(bitmapLoader));

            this._bitmapLoader = bitmapLoader;
        }

        /// <summary>
        /// See <see cref="IDisplayFactory.CreateDisplay"/>.
        /// </summary>
        public IDisplay CreateDisplay(DisplayProperties properties, Action<IDrawingContext> drawCallback)
        {
            ArgumentChecks.AssertNotNull(drawCallback, nameof(drawCallback));

            return new Display(
                this._bitmapLoader,
                properties,
                drawCallback);
        }
    }
}

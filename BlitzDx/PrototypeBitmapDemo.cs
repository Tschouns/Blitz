//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using System.Drawing;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Display;

    /// <summary>
    /// A prototype to test bitmaps.
    /// </summary>
    public class PrototypeBitmapDemo : IDisposable
    {
        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeBitmapDemo"/> class.
        /// </summary>
        public PrototypeBitmapDemo()
            : this(Ioc.Container.Resolve<IDisplayFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeBitmapDemo"/> class.
        /// </summary>
        public PrototypeBitmapDemo(IDisplayFactory displayFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));

            var displayProperties = new DisplayProperties()
            {
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(displayProperties, this.Draw);
        }

        /// <summary>
        /// Runs the test.
        /// </summary>
        public void Run()
        {
            this._display.Show();

            do
            {
                // Do stuff.
            }
            while (this._display.DrawFrame());
        }

        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            this._display.Dispose();
        }

        /// <summary>
        /// The draw callback.
        /// </summary>
        private void Draw(IDrawingContext drawingContext)
        {
            ////drawingContext.DrawBitmap(Bitmaps.SampleBitmapUno);
        }
    }
}

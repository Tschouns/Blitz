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
    public class PrototypeBitmapTest : IDisposable
    {
        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// Stores the sample sprite.
        /// </summary>
        private readonly ISprite _sampleSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeBitmapTest"/> class.
        /// </summary>
        public PrototypeBitmapTest()
            : this(Ioc.Container.Resolve<IDisplayFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeBitmapTest"/> class.
        /// </summary>
        public PrototypeBitmapTest(IDisplayFactory displayFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));

            var displayProperties = new DisplayProperties()
            {
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(displayProperties, this.Draw);

            // Load bitmap.
            this._sampleSprite = this._display.SpriteManager.LoadFromDrawingBitmap(Bitmaps.SampleBitmapUno);
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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._display.Dispose();
            }
        }

        /// <summary>
        /// The draw callback.
        /// </summary>
        private void Draw(IDrawingContext drawingContext)
        {
            this._sampleSprite.Draw();
        }
    }
}

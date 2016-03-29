//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Display;

    /// <summary>
    /// A prototype to test the <see cref="IDisplay"/> interface.
    /// </summary>
    public sealed class PrototypeDisplayDemo : IDisposable
    {
        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDisplayDemo"/> class.
        /// </summary>
        public PrototypeDisplayDemo()
            : this(Ioc.Container.Resolve<IDisplayFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDisplayDemo"/> class.
        /// </summary>
        public PrototypeDisplayDemo(IDisplayFactory displayFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));

            var properties = new DisplayProperties()
            {
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(properties, this.Draw);
        }

        /// <summary>
        /// Runs the prototype.
        /// </summary>
        public void Run()
        {
            this._display.Show();

            do
            {
                Application.DoEvents();

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
        /// Is called to actually draw the frame using the <see cref="IDrawingContext"/>.
        /// </summary>
        private void Draw(IDrawingContext drawingContext)
        {
            // Draw stuff.
        }
    }
}

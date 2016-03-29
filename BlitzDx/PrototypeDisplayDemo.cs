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
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// A prototype to test the <see cref="IDisplay"/> interface.
    /// </summary>
    public sealed class PrototypeDisplayDemo : IDisposable
    {
        /// <summary>
        /// The display properties.
        /// </summary>
        private readonly DisplayProperties _displayProperties;

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

            this._displayProperties = new DisplayProperties()
            {
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(this._displayProperties, this.Draw);
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
            Checks.AssertNotNull(drawingContext, nameof(drawingContext));

            var width = this._displayProperties.Resolution.Width;
            var height = this._displayProperties.Resolution.Height;

            drawingContext.DrawLine(
                new Point(0, 0), 
                new Point(width, height),
                Color.Brown,
                2);

            drawingContext.DrawLine(
                new Point(0, height),
                new Point(width, 0),
                Color.Brown,
                2);
        }
    }
}

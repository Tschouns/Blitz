//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base.RuntimeChecks;

    /// <summary>
    /// Interaction logic for <see cref="ClickyDrawingControl"/>.
    /// </summary>
    public partial class ClickyDrawingControl : UserControl
    {
        /// <summary>
        /// Stores the actual canvas to draw on.
        /// </summary>
        private readonly OpenCanvas canvas;

        /// <summary>
        /// Stores the handler which draws on the canvas.
        /// </summary>
        private readonly IDrawingHandler drawingHandler;

        /// <summary>
        /// Stores all the "dots" created by the user.
        /// </summary>
        private readonly IList<Geometry.Point> dots;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyDrawingControl"/> class.
        /// </summary>
        public ClickyDrawingControl()
        {
            this.InitializeComponent();

            // canvas
            this.canvas = new OpenCanvas();

            this.AddVisualChild(this.canvas);

            this.canvas.Margin = new Thickness(0);
            this.canvas.MouseDown += this.OpenCanvas_MouseDown;
                        
            // drawing handler
            this.drawingHandler = new CanvasDrawingHandler(this.canvas);

            // dots list
            this.dots = new List<Geometry.Point>();
        }

        /// <summary>
        /// Draws the objects, created by the user, on the canvas.
        /// </summary>
        private void Draw()
        {
            this.canvas.Clear();

            foreach (var dot in this.dots)
            {
                this.drawingHandler.DrawDot(dot);
            }
        }

        /// <summary>
        /// Handles the <see cref="Canvas.MouseDown"/> event.
        /// </summary>
        private void OpenCanvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            var position = eventArgs.GetPosition(this);
            var dot = new Geometry.Point(position.X, position.Y);

            this.dots.Add(dot);

            this.Draw();
        }
    }
}

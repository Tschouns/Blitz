//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base.RuntimeChecks;

    /// <summary>
    /// Interaction logic for <see cref="ClickyDrawingControl"/>.
    /// </summary>
    public partial class ClickyDrawingControl : UserControl
    {
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
                        
            this.dots = new List<Geometry.Point>();

            // canvas
            this.canvas.Background = System.Windows.Media.Brushes.CornflowerBlue;
            this.canvas.MouseDown += this.Canvas_MouseDown;
            this.canvas.Rendering += this.RenderigCanvas_Rendering;
        }

        /// <summary>
        /// Handles the <see cref="Canvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            var position = eventArgs.GetPosition(this);
            var dot = new Geometry.Point(position.X, position.Y);

            this.dots.Add(dot);

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));
            
            foreach (var dot in this.dots)
            {
                eventArgs.DrawingHandler.DrawDot(dot);
            }
        }
    }
}

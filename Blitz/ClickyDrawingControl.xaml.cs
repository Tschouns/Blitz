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
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Helpers;

    /// <summary>
    /// Interaction logic for <see cref="ClickyDrawingControl"/>.
    /// </summary>
    public partial class ClickyDrawingControl : UserControl
    {
        /// <summary>
        /// Used to detect line intersections.
        /// </summary>
        private readonly ILineIntersectionHelper lineIntersectionHelper;

        /// <summary>
        /// Stores all the "dots" created by the user.
        /// </summary>
        private readonly IList<Point> dots;

        /// <summary>
        /// Stores all the lines created by the user.
        /// </summary>
        private readonly IList<Line> lineSegments;

        /// <summary>
        /// Stores all the intersection points between the line segments.
        /// </summary>
        private readonly IList<Point> lineIntersections;

        /// <summary>
        /// Stores the point last clicked by the user.
        /// </summary>
        private Point? lastClickedPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyDrawingControl"/> class.
        /// </summary>
        public ClickyDrawingControl()
            : this(Ioc.Container.Resolve<ILineIntersectionHelper>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyDrawingControl"/> class.
        /// </summary>
        public ClickyDrawingControl(ILineIntersectionHelper lineIntersectionHelper)
        {
            Checks.AssertNotNull(lineIntersectionHelper, nameof(lineIntersectionHelper));

            this.lineIntersectionHelper = lineIntersectionHelper;

            this.dots = new List<Point>();
            this.lineSegments = new List<Line>();
            this.lineIntersections = new List<Point>();
            this.lastClickedPoint = null;

            this.InitializeComponent();

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

            // Create a new dot.
            var dot = new Point(position.X, position.Y);
            this.dots.Add(dot);

            // Create a new line segment.
            if (this.lastClickedPoint.HasValue)
            {
                var lineSegment = new Line(this.lastClickedPoint.Value, dot);
                
                // Detect new line intersections with existing line segments.
                {
                    foreach (var existingLineSegment in this.lineSegments)
                    {
                        var result = this.lineIntersectionHelper.GetLineSegmentIntersection(lineSegment, existingLineSegment);

                        if (result.HasValue)
                        {
                            this.lineIntersections.Add(result.Value);
                        }
                    }
                }

                this.lineSegments.Add(lineSegment);
            }

            this.lastClickedPoint = dot;

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            foreach (var lineSegment in this.lineSegments)
            {
                eventArgs.DrawingHandler.DrawLineSegment(lineSegment);
            }

            foreach (var intersectionPoint in this.lineIntersections)
            {
                eventArgs.DrawingHandler.DrawDot(intersectionPoint);
            }

            ////foreach (var dot in this.dots)
            ////{
            ////    eventArgs.DrawingHandler.DrawDot(dot);
            ////}
        }
    }
}

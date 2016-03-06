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
        /// Stores all the rectangles.
        /// </summary>
        private readonly IList<Rectangle> rectangles;

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
            Checks.AssertNotNullIfNotDesignMode(lineIntersectionHelper, nameof(lineIntersectionHelper), this);

            this.lineIntersectionHelper = lineIntersectionHelper;

            this.dots = new List<Point>();
            this.lineSegments = new List<Line>();
            this.lineIntersections = new List<Point>();
            this.rectangles = new List<Rectangle>();
            this.lastClickedPoint = null;

            this.InitializeComponent();

            // canvas
            this.canvas.Background = System.Windows.Media.Brushes.CornflowerBlue;
            this.canvas.MouseDown += this.Canvas_MouseDown;
            this.canvas.Rendering += this.RenderigCanvas_Rendering;
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            var position = eventArgs.GetPosition(this);

            // Create a new dot.
            var dot = new Point(position.X, position.Y);
            this.dots.Add(dot);

            if (this.lastClickedPoint.HasValue)
            {
                // Create a new line segment.
                var lineSegment = new Line(this.lastClickedPoint.Value, dot);
                
                // Detect new line intersections with existing line segments.
                foreach (var existingLineSegment in this.lineSegments)
                {
                    var result = this.lineIntersectionHelper.GetLineSegmentIntersection(lineSegment, existingLineSegment);

                    if (result.HasValue)
                    {
                        this.lineIntersections.Add(result.Value);
                    }
                }

                this.lineSegments.Add(lineSegment);

                // Create a new rectangle.
                var origin = this.lastClickedPoint.Value;
                var rectangle = new Rectangle(origin, dot.X - origin.X, dot.Y - origin.Y);
                this.rectangles.Add(rectangle);
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

            foreach (var rectangle in this.rectangles)
            {
                eventArgs.DrawingHandler.DrawPolygon(
                    new[]
                    {
                        rectangle.A,
                        rectangle.B,
                        rectangle.C,
                        rectangle.D
                    });
            }

            ////foreach (var lineSegment in this.lineSegments)
            ////{
            ////    eventArgs.DrawingHandler.DrawLineSegment(lineSegment);
            ////}

            ////foreach (var intersectionPoint in this.lineIntersections)
            ////{
            ////    eventArgs.DrawingHandler.DrawDot(intersectionPoint);
            ////}

            ////foreach (var dot in this.dots)
            ////{
            ////    eventArgs.DrawingHandler.DrawDot(dot);
            ////}
        }
    }
}

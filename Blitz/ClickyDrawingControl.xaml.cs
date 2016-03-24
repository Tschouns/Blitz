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
        private readonly ILineIntersectionHelper _lineIntersectionHelper;

        /// <summary>
        /// Stores all the "dots" created by the user.
        /// </summary>
        private readonly IList<Point> _dots;

        /// <summary>
        /// Stores all the lines created by the user.
        /// </summary>
        private readonly IList<Line> _lineSegments;

        /// <summary>
        /// Stores all the intersection points between the line segments.
        /// </summary>
        private readonly IList<Point> _lineIntersections;

        /// <summary>
        /// Stores all the rectangles.
        /// </summary>
        private readonly IList<Rectangle> _rectangles;

        /// <summary>
        /// Stores the point last clicked by the user.
        /// </summary>
        private Point? _lastClickedPoint;

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

            this._lineIntersectionHelper = lineIntersectionHelper;

            this._dots = new List<Point>();
            this._lineSegments = new List<Line>();
            this._lineIntersections = new List<Point>();
            this._rectangles = new List<Rectangle>();
            this._lastClickedPoint = null;

            this.InitializeComponent();

            // canvas
            this._canvas.Background = System.Windows.Media.Brushes.CornflowerBlue;
            this._canvas.MouseDown += this.Canvas_MouseDown;
            this._canvas.Rendering += this.RenderigCanvas_Rendering;
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
            this._dots.Add(dot);

            if (this._lastClickedPoint.HasValue)
            {
                // Create a new line segment.
                var lineSegment = new Line(this._lastClickedPoint.Value, dot);
                
                // Detect new line intersections with existing line segments.
                foreach (var existingLineSegment in this._lineSegments)
                {
                    var result = this._lineIntersectionHelper.GetLineSegmentIntersection(lineSegment, existingLineSegment);

                    if (result.HasValue)
                    {
                        this._lineIntersections.Add(result.Value);
                    }
                }

                this._lineSegments.Add(lineSegment);

                // Create a new rectangle.
                var origin = this._lastClickedPoint.Value;
                var rectangle = new Rectangle(origin, dot.X - origin.X, dot.Y - origin.Y);
                this._rectangles.Add(rectangle);
            }

            this._lastClickedPoint = dot;

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            foreach (var rectangle in this._rectangles)
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

            ////foreach (var lineSegment in this._lineSegments)
            ////{
            ////    eventArgs.DrawingHandler.DrawLineSegment(lineSegment);
            ////}

            ////foreach (var intersectionPoint in this._lineIntersections)
            ////{
            ////    eventArgs.DrawingHandler.DrawDot(intersectionPoint);
            ////}

            ////foreach (var dot in this._dots)
            ////{
            ////    eventArgs.DrawingHandler.DrawDot(dot);
            ////}
        }
    }
}

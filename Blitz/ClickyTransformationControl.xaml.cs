//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Geometry.Helpers;

    /// <summary>
    /// Interaction logic for <see cref="ClickyTransformationControl"/>.
    /// </summary>
    public partial class ClickyTransformationControl : UserControl
    {
        /// <summary>
        /// Used to translate and rotate points.
        /// </summary>
        private readonly IPointTransformationHelper pointTransformationHelper;

        /// <summary>
        /// Used determine the center of mass of a rectangle.
        /// </summary>
        private readonly ILineIntersectionHelper lineIntersectionHelper;

        /// <summary>
        /// The original rectangle, before any transformations.
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// The offset by which the rectangle is translated.
        /// </summary>
        private Vector2 rectangleOffset;

        /// <summary>
        /// The angle by which the rectangle is rotated.
        /// </summary>
        private double rectangleOrientation;

        /// <summary>
        /// A projected representation of the original rectangle, after the transformations.
        /// </summary>
        private Point[] rectangleProjection = new Point[4];

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyTransformationControl"/> class.
        /// </summary>
        public ClickyTransformationControl()
            : this(
                  Ioc.Container.Resolve<IPointTransformationHelper>(),
                  Ioc.Container.Resolve<ILineIntersectionHelper>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyTransformationControl"/> class.
        /// </summary>
        public ClickyTransformationControl(
            IPointTransformationHelper pointTransformationHelper,
            ILineIntersectionHelper lineIntersectionHelper)
        {
            Checks.AssertNotNullIfNotDesignMode(pointTransformationHelper, nameof(pointTransformationHelper), this);
            Checks.AssertNotNullIfNotDesignMode(lineIntersectionHelper, nameof(lineIntersectionHelper), this);

            this.pointTransformationHelper = pointTransformationHelper;
            this.lineIntersectionHelper = lineIntersectionHelper;

            this.rectangle = new Rectangle(300, 200);
            this.rectangleOffset = new Vector2(0, 0);
            this.rectangleOrientation = 0.0;

            this.InitializeComponent();
        }

        /// <summary>
        /// Transforms the original rectangle to get the "projection".
        /// </summary>
        private void TransformOriginalRectangle()
        {
            this.rectangleProjection = new Point[]
                {
                    this.rectangle.A,
                    this.rectangle.B,
                    this.rectangle.C,
                    this.rectangle.D
                };

            // Translate.
            this.rectangleProjection = this.pointTransformationHelper.TranslatePoints(
                this.rectangleOffset,
                this.rectangleProjection);

            // Rotate.
            var centerOfMass = this.lineIntersectionHelper.GetLineIntersection(
                new Line(this.rectangleProjection[0], this.rectangleProjection[2]),
                new Line(this.rectangleProjection[1], this.rectangleProjection[3]));

            this.rectangleProjection = this.pointTransformationHelper.RotatePoints(
                centerOfMass.Value,
                this.rectangleOrientation,
                this.rectangleProjection);
        }

        /// <summary>
        /// Is called when this control is initialized. 
        /// </summary>
        private void Grid_Initialized(object sender, System.EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.TransformOriginalRectangle();

            this.canvas.Background = System.Windows.Media.Brushes.Orange;
            this.canvas.MouseDown += this.Canvas_MouseDown;
            this.canvas.Rendering += this.RenderigCanvas_Rendering;

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.LeftButton == MouseButtonState.Pressed)
            {
                this.rectangleOffset = this.rectangleOffset.AddVector(new Vector2(10, 10));
            }

            if (eventArgs.RightButton == MouseButtonState.Pressed)
            {
                this.rectangleOrientation += 0.1;
            }

            this.TransformOriginalRectangle();

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            eventArgs.DrawingHandler.DrawPolygon(this.rectangleProjection);
        }
    }
}

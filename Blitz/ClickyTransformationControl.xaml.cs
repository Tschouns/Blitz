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
        private readonly IPointTransformationHelper _pointTransformationHelper;

        /// <summary>
        /// Used determine the center of mass of a rectangle.
        /// </summary>
        private readonly ILineIntersectionHelper _lineIntersectionHelper;

        /// <summary>
        /// The original rectangle, before any transformations.
        /// </summary>
        private readonly Rectangle _rectangle;

        /// <summary>
        /// The offset by which the rectangle is translated.
        /// </summary>
        private Vector2 _rectangleOffset;

        /// <summary>
        /// The angle by which the rectangle is rotated.
        /// </summary>
        private double _rectangleOrientation;

        /// <summary>
        /// A projected representation of the original rectangle, after the transformations.
        /// </summary>
        private Point[] _rectangleProjection = new Point[4];

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

            this._pointTransformationHelper = pointTransformationHelper;
            this._lineIntersectionHelper = lineIntersectionHelper;

            this._rectangle = new Rectangle(300, 200);
            this._rectangleOffset = new Vector2(0, 0);
            this._rectangleOrientation = 0.0;

            this.InitializeComponent();
        }

        /// <summary>
        /// Transforms the original rectangle to get the "projection".
        /// </summary>
        private void TransformOriginalRectangle()
        {
            this._rectangleProjection = new Point[]
                {
                    this._rectangle.A,
                    this._rectangle.B,
                    this._rectangle.C,
                    this._rectangle.D
                };

            // Translate.
            this._rectangleProjection = this._pointTransformationHelper.TranslatePoints(
                this._rectangleOffset,
                this._rectangleProjection);

            // Rotate.
            var centerOfMass = this._lineIntersectionHelper.GetLineIntersection(
                new Line(this._rectangleProjection[0], this._rectangleProjection[2]),
                new Line(this._rectangleProjection[1], this._rectangleProjection[3]));

            this._rectangleProjection = this._pointTransformationHelper.RotatePoints(
                centerOfMass.Value,
                this._rectangleOrientation,
                this._rectangleProjection);
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

            this._canvas.Background = System.Windows.Media.Brushes.Orange;
            this._canvas.MouseDown += this.Canvas_MouseDown;
            this._canvas.Rendering += this.RenderigCanvas_Rendering;

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.LeftButton == MouseButtonState.Pressed)
            {
                this._rectangleOffset = this._rectangleOffset.AddVector(new Vector2(10, 10));
            }

            if (eventArgs.RightButton == MouseButtonState.Pressed)
            {
                this._rectangleOrientation += 0.1;
            }

            this.TransformOriginalRectangle();

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            eventArgs.DrawingHandler.DrawPolygon(this._rectangleProjection);
        }
    }
}

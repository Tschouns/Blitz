//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using System.Collections.Generic;
    using System.Windows.Media;
    using Geometry.Extensions;
    using Geometry.Algorithms.Gjk;
    using Geometry;

    /// <summary>
    /// Interaction logic for ClickyGjkDemoControl.xaml
    /// </summary>
    public partial class ClickyGjkDemoControl : UserControl
    {
        /// <summary>
        /// Used to detect line intersections.
        /// </summary>
        private readonly IGjkAlgorithm<Polygon, Polygon> _gjk;

        /// <summary>
        /// Stores all the "dots" that make up the first polygon.
        /// </summary>
        private readonly IList<Point> _dotsForPolygon1;

        /// <summary>
        /// Stores all the "dots" that make up the second polygon.
        /// </summary>
        private readonly IList<Point> _dotsForPolygon2;

        /// <summary>
        /// Cheap little offset trick, so the area surrounding the origin can be drawn.
        /// </summary>
        private readonly Vector2 _displayOffset;

        /// <summary>
        /// Stores a value indicating whether the two polygons (if existing) intersect. <c>False</c> if either of
        /// them is no a complete polygon (yet).
        /// </summary>
        private FigureIntersectionResult _doPolygonsIntersectResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyGjkDemoControl"/> class.
        /// </summary>
        public ClickyGjkDemoControl()
            : this(Ioc.Container.Resolve<IGjkAlgorithm<Polygon, Polygon>>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyGjkDemoControl"/> class.
        /// </summary>
        public ClickyGjkDemoControl(IGjkAlgorithm<Polygon, Polygon> gjk)
        {
            Checks.AssertNotNullIfNotDesignMode(gjk, nameof(gjk), this);

            this._gjk = gjk;

            this._dotsForPolygon1 = new List<Point>();
            this._dotsForPolygon2 = new List<Point>();

            this._displayOffset = new Vector2(500, 500);

            this.InitializeComponent();

            // canvas
            this._canvas.Background = Brushes.Chocolate;
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
            var dot = new Point(position.X, position.Y).SubtactVector(this._displayOffset);

            if (eventArgs.LeftButton == MouseButtonState.Pressed)
            {
                this._dotsForPolygon1.Add(dot);
            }

            if (eventArgs.RightButton == MouseButtonState.Pressed)
            {
                this._dotsForPolygon2.Add(dot);
            }

            if (eventArgs.MiddleButton == MouseButtonState.Pressed)
            {
                this._dotsForPolygon1.Clear();
                this._dotsForPolygon2.Clear();
                this._doPolygonsIntersectResult = null;
            }

            // Check for intersection.
            if (this._dotsForPolygon1.Count < 3 || this._dotsForPolygon2.Count < 3)
            {
                this._canvas.Background = Brushes.Chocolate;
                this._canvas.InvalidateVisual();

                return;
            }

            var polygon1 = new Polygon(this._dotsForPolygon1);
            var polygon2 = new Polygon(this._dotsForPolygon2);

            this._doPolygonsIntersectResult = this._gjk.DoFiguresIntersect(polygon1, polygon2);

            // Set background color, based on result.
            this._canvas.Background = this._doPolygonsIntersectResult.DoFiguresIntersect ? Brushes.Green : Brushes.Chocolate;
            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            // Draw origin.
            eventArgs.DrawingHandler.DrawDot(GeometryConstants.Origin.AddVector(this._displayOffset));

            // Draw polygons.
            var dotsForPolygon1WithOffset = this._dotsForPolygon1.Select(aX => aX.AddVector(this._displayOffset)).ToList();
            var dotsForPolygon2WithOffset = this._dotsForPolygon2.Select(aX => aX.AddVector(this._displayOffset)).ToList();

            eventArgs.DrawingHandler.DrawPolygon(dotsForPolygon1WithOffset, Brushes.Aqua);
            eventArgs.DrawingHandler.DrawPolygon(dotsForPolygon2WithOffset, Brushes.Cornsilk);

            // Draw magic pol<gon.
            if (this._doPolygonsIntersectResult?.MagicPolygonNullable != null)
            {
                var firstMagicTriangleWithOffset = this._doPolygonsIntersectResult.MagicPolygonNullable.Corners.Take(3).Select(aX => aX.AddVector(this._displayOffset)).ToList();
                eventArgs.DrawingHandler.DrawPolygon(firstMagicTriangleWithOffset);

                if (this._doPolygonsIntersectResult.MagicPolygonNullable.Corners.Count() > 3)
                {
                    var secondMagicTriangleWithOffset = this._doPolygonsIntersectResult.MagicPolygonNullable.Corners.Skip(1).Select(aX => aX.AddVector(this._displayOffset)).ToList();
                    eventArgs.DrawingHandler.DrawPolygon(secondMagicTriangleWithOffset);
                }
            }

            // Minkowski hack.
            foreach(var dot1 in this._dotsForPolygon1)
            {
                foreach (var dot2 in this._dotsForPolygon2)
                {
                    var minkowskiDiffDot = dot1.SubtactVector(dot2.AsVector());
                    eventArgs.DrawingHandler.DrawDot(minkowskiDiffDot.AddVector(this._displayOffset));
                }
            }
        }
    }
}

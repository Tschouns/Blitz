﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Helpers;

    /// <summary>
    /// Interaction logic for <see cref="ClickyPolygonControl"/>.
    /// </summary>
    public partial class ClickyPolygonControl : UserControl
    {
        /// <summary>
        /// Used to determine the polygon's centroid and "non-simple property".
        /// </summary>
        private readonly IPolygonCalculationHelper polygonCalculationHelper;
        
        /// <summary>
        /// Stores all the polygon corners created by the user.
        /// </summary>
        private readonly IList<Point> polygonCorners;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyPolygonControl"/> class.
        /// </summary>
        public ClickyPolygonControl()
            : this(Ioc.Container.Resolve<IPolygonCalculationHelper>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyPolygonControl"/> class.
        /// </summary>
        public ClickyPolygonControl(IPolygonCalculationHelper polygonCalculationHelper)
        {
            Checks.AssertNotNullIfNotDesignMode(polygonCalculationHelper, nameof(polygonCalculationHelper), this);

            this.InitializeComponent();

            this.polygonCalculationHelper = polygonCalculationHelper;
            this.polygonCorners = new List<Point>();
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
            
            this.canvas.Background = System.Windows.Media.Brushes.IndianRed;
            this.canvas.MouseDown += this.Canvas_MouseDown;
            this.canvas.Rendering += this.RenderigCanvas_Rendering;

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            if (eventArgs.LeftButton == MouseButtonState.Pressed)
            {
                var position = eventArgs.GetPosition(this.canvas);
                var polygonCorner = new Point(
                    position.X,
                    position.Y);

                this.polygonCorners.Add(polygonCorner);
            }

            if (eventArgs.RightButton == MouseButtonState.Pressed)
            {
                if (this.polygonCorners.Any())
                {
                    this.polygonCorners.RemoveAt(this.polygonCorners.Count - 1);
                }
            }

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            if (this.polygonCorners.Count < 3)
            {
                foreach (var futurePolygonCorner in this.polygonCorners)
                {
                    eventArgs.DrawingHandler.DrawDot(futurePolygonCorner);
                }

                return;
            }

            var polygon = new Polygon(this.polygonCorners);
            var polygonIsNonSimple = this.polygonCalculationHelper.IsNonsimplePolygon(polygon);

            if (polygonIsNonSimple)
            {
                eventArgs.DrawingHandler.DrawPath(this.polygonCorners);
                return;
            }

            var centroid = this.polygonCalculationHelper.DetermineCentroid(polygon);

            eventArgs.DrawingHandler.DrawPolygon(this.polygonCorners);
            eventArgs.DrawingHandler.DrawDot(centroid);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Camera;
    using Display;
    using Geometry.Elements;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// A prototype to test the <see cref="ICamera"/> interface.
    /// </summary>
    public sealed class PrototypeCameraTransformationDemo : IDisposable
    {
        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// Holds the camera.
        /// </summary>
        private readonly ICamera _camera;

        /// <summary>
        /// Stores a set of lines, defined in world coordinates.
        /// </summary>
        private readonly IList<Line> _linesInTheWorld;

        /// <summary>
        /// Stores a set of polygons, defined in world coordinates.
        /// </summary>
        private readonly IList<Polygon> _polygonsInTheWorld;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeCameraTransformationDemo"/> class.
        /// </summary>
        public PrototypeCameraTransformationDemo()
            : this(
                  Ioc.Container.Resolve<IDisplayFactory>(),
                  Ioc.Container.Resolve<ICameraFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeCameraTransformationDemo"/> class.
        /// </summary>
        public PrototypeCameraTransformationDemo(
            IDisplayFactory displayFactory,
            ICameraFactory cameraFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));

            // Setup display.
            var displayProperties = new DisplayProperties()
            {
                Title = "Camera Transformation Demo",
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(displayProperties, this.Draw);

            // Setup camera.
            this._camera = cameraFactory.CreateCamera(
                displayProperties.Resolution.Width,
                displayProperties.Resolution.Height);

            this._camera.Scale = 2;
            //this._camera.Orientation = 0.2;
            this._camera.Position = new Point(10, 10);

            // Initialize world.
            this._linesInTheWorld = new List<Line>();
            this._polygonsInTheWorld = new List<Polygon>();
        }

        /// <summary>
        /// Runs the prototype.
        /// </summary>
        public void Run()
        {
            this._display.Show();

            // Spawn some stuff.
            this.PopulateWorld();

            // Initialize time.
            double simTime = 0;
            var realTime = DateTime.Now;

            // Loop.
            do
            {
                Application.DoEvents();

                // Calculate elapsed time (real time).
                var now = DateTime.Now;
                var elapsedRealTimeInSeconds = (now - realTime).TotalSeconds;
                realTime = now;

                // Set current simulation time (= real time).
                var elapsedSimTimeInSeconds = elapsedRealTimeInSeconds;
                simTime += elapsedRealTimeInSeconds;

                // Step the simulation...
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
        /// Populates the world, spawns some stuff.
        /// </summary>
        private void PopulateWorld()
        {
            // Add the "origin marker".
            var originMarkerLine1 = new Line(
                new Point(-100, 0),
                new Point(100, 0));
            var originMarkerLine2 = new Line(
                new Point(0, -100),
                new Point(0, 100));

            this._linesInTheWorld.Add(originMarkerLine1);
            this._linesInTheWorld.Add(originMarkerLine2);

            // Add some objects.
            var polygon = new Polygon(
                new Point(10, 10),
                new Point(20, 10),
                new Point(20, 70),
                new Point(10, 70));

            this._polygonsInTheWorld.Add(polygon);
        }

        /// <summary>
        /// Is called to actually draw the frame using the <see cref="IDrawingContext"/>.
        /// </summary>
        private void Draw(IDrawingContext drawingContext)
        {
            Checks.AssertNotNull(drawingContext, nameof(drawingContext));

            // Draw the cross.
            var width = this._display.Properties.Resolution.Width;
            var height = this._display.Properties.Resolution.Height;

            drawingContext.DrawLine(
                new Point(0, 0),
                new Point(width, height),
                Color.Green,
                1);

            drawingContext.DrawLine(
                new Point(0, height),
                new Point(width, 0),
                Color.Green,
                1);

            // Get transformation.
            var transform = this._camera.GetCameraTransformation();

            // Draw the lines.
            foreach (var line in this._linesInTheWorld)
            {
                drawingContext.DrawLine(
                    transform.WorldToViewport(line.Point1),
                    transform.WorldToViewport(line.Point2),
                    Color.Red,
                    2);
            }

            // Draw the lines.
            foreach (var polygon in this._polygonsInTheWorld)
            {
                drawingContext.DrawPolygon(
                    polygon.Corners.Select(aX => transform.WorldToViewport(aX)).ToList(),
                    Color.Red,
                    2);
            }
        }
    }
}

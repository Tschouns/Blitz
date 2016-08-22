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
    using System.Windows.Input;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Camera;
    using Display;
    using Geometry.Elements;
    using Input;
    using Input.InputAction;
    using Point = Geometry.Elements.Point;
    using Camera.CameraEffects;
    /// <summary>
    /// A prototype to test the <see cref="ICamera"/> interface.
    /// </summary>
    public sealed class PrototypeCameraTransformationDemo : IDisposable
    {
        /// <summary>
        /// Holds the <see cref="IInputActionManager"/>.
        /// </summary>
        private readonly IInputActionManager _inputActionManager;

        /// <summary>
        /// Action used to end the simulation.
        /// </summary>
        private readonly IInputAction _actionEnd;

        /// <summary>
        /// Used to move the camera, based on user input.
        /// </summary>
        private readonly ICameraEffect _positionByButtonsCameraEffect;

        /// <summary>
        /// Holds the camera.
        /// </summary>
        private readonly ICamera _camera;

        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

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
                  Ioc.Container.Resolve<IInputFactory>(),
                  Ioc.Container.Resolve<IDisplayFactory>(),
                  Ioc.Container.Resolve<ICameraFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeCameraTransformationDemo"/> class.
        /// </summary>
        public PrototypeCameraTransformationDemo(
            IInputFactory inputFactory,
            IDisplayFactory displayFactory,
            ICameraFactory cameraFactory)
        {
            Checks.AssertNotNull(inputFactory, nameof(inputFactory));
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));

            // Setup display.
            var displayProperties = new DisplayProperties()
            {
                Title = "Camera Transformation Demo",
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(displayProperties, this.Draw);

            // Setup input.
            this._inputActionManager = inputFactory.CreateInputActionManager();

            var keyboard = inputFactory.KeyboardButtonCreator;
            var actionCameraUp = this._inputActionManager.RegisterButtonHoldAction(keyboard.Create(Key.W));
            var actionCameraDown = this._inputActionManager.RegisterButtonHoldAction(keyboard.Create(Key.S));
            var actionCameraLeft = this._inputActionManager.RegisterButtonHoldAction(keyboard.Create(Key.A));
            var actionCameraRight = this._inputActionManager.RegisterButtonHoldAction(keyboard.Create(Key.D));

            this._actionEnd = this._inputActionManager.RegisterButtonHitAction(keyboard.Create(Key.Escape));

            // Setup camera.
            this._camera = cameraFactory.CreateCamera(
                displayProperties.Resolution.Width,
                displayProperties.Resolution.Height);

            this._camera.Scale = 2;
            this._camera.Position = new Point(0, 0);

            this._positionByButtonsCameraEffect = cameraFactory.CameraEffectCreator.CreatePositionByButtonsEffect(
                actionCameraUp,
                actionCameraDown,
                actionCameraLeft,
                actionCameraRight,
                10);

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
            var realTime = DateTime.Now;

            // Loop.
            do
            {
                Application.DoEvents();
                
                // Calculate elapsed time (real time).
                var now = DateTime.Now;
                var elapsedRealTimeInSeconds = (now - realTime).TotalSeconds;
                realTime = now;

                // Update input.
                this._inputActionManager.Update(elapsedRealTimeInSeconds);

                // Control camera (in a highly adventurous fashion).
                this._positionByButtonsCameraEffect.Update(elapsedRealTimeInSeconds);
                this._positionByButtonsCameraEffect.ApplyToCamera(this._camera);
            }
            while (this._display.DrawFrame() && !this._actionEnd.IsActive);
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
        }
    }
}

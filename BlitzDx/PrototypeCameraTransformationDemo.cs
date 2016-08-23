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
    using Camera.CameraEffects;
    using Display;
    using Geometry.Elements;
    using Input;
    using Input.InputAction;
    using Point = Geometry.Elements.Point;

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
        /// Used to create "blow" effects.
        /// </summary>
        private readonly ICameraEffectCreator _cameraEffectCreator;

        /// <summary>
        /// Action used to cause a blow to the left.
        /// </summary>
        private readonly IInputAction _actionCauseBlowLeft;

        /// <summary>
        /// Action used to cause a blow upwards.
        /// </summary>
        private readonly IInputAction _actionCauseBlowUp;

        /// <summary>
        /// Action used to end the simulation.
        /// </summary>
        private readonly IInputAction _actionEnd;

        /// <summary>
        /// Controls the camera.
        /// </summary>
        private readonly ICameraController _cameraController;

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
            this._actionEnd = this._inputActionManager.RegisterButtonHitAction(keyboard.Create(Key.Escape));
            this._actionCauseBlowLeft = this._inputActionManager.RegisterButtonHitAction(keyboard.Create(Key.NumPad4));
            this._actionCauseBlowUp = this._inputActionManager.RegisterButtonHitAction(keyboard.Create(Key.NumPad8));

            // Setup camera and camera controller.
            var camera = cameraFactory.CreateCamera(
                displayProperties.Resolution.Width,
                displayProperties.Resolution.Height);

            this._cameraEffectCreator = cameraFactory.CameraEffectCreator;
            var positionEffect = this._cameraEffectCreator.CreatePositionByButtonsEffect(
                this._inputActionManager,
                keyboard.Create(Key.W),
                keyboard.Create(Key.S),
                keyboard.Create(Key.A),
                keyboard.Create(Key.D),
                50);

            var scaleEffect = this._cameraEffectCreator.CreateScaleExponentialByButtonsEffect(
                this._inputActionManager,
                keyboard.Create(Key.E),
                keyboard.Create(Key.Q),
                0.5,
                5.0,
                0.5);

            this._cameraController = cameraFactory.CreateCameraController(camera);
            this._cameraController.AddEffect(positionEffect);
            this._cameraController.AddEffect(scaleEffect);

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

                // Control camera, create "blows".
                if (this._actionCauseBlowLeft.IsActive)
                {
                    var blowLeft = this._cameraEffectCreator.CreatePositionBlowOscillationEffect(
                        new Vector2(-50, 0),
                        1,
                        5);

                    this._cameraController.AddEffect(blowLeft);
                }

                if (this._actionCauseBlowUp.IsActive)
                {
                    var blowUp = this._cameraEffectCreator.CreatePositionBlowOscillationEffect(
                        new Vector2(0, 50),
                        1,
                        5);

                    this._cameraController.AddEffect(blowUp);
                }

                this._cameraController.Update(elapsedRealTimeInSeconds);
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
            var transform = this._cameraController.Camera.GetCameraTransformation();

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

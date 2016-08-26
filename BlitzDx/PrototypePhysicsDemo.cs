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
    using System.Windows.Forms;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Display;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.World;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// A prototype to test the <see cref="IDisplay"/> interface.
    /// </summary>
    public sealed class PrototypePhysicsDemo : IDisposable
    {
        /// <summary>
        /// The display properties.
        /// </summary>
        private readonly DisplayProperties _displayProperties;

        /// <summary>
        /// Holds the <see cref="IDisplay"/>.
        /// </summary>
        private readonly IDisplay _display;

        /// <summary>
        /// The physical world.
        /// </summary>
        private readonly IPhysicalWorld _physicalWorld;

        /// <summary>
        /// A list used to track the bodies spawned in the world.
        /// </summary>
        private readonly IList<IBody<Polygon>> _bodies;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypePhysicsDemo"/> class.
        /// </summary>
        public PrototypePhysicsDemo()
            : this(
                  Ioc.Container.Resolve<IDisplayFactory>(),
                  Ioc.Container.Resolve<IPhysicsFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypePhysicsDemo"/> class.
        /// </summary>
        public PrototypePhysicsDemo(
            IDisplayFactory displayFactory,
            IPhysicsFactory physicsFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(physicsFactory, nameof(physicsFactory));

            // Setup display.
            this._displayProperties = new DisplayProperties()
            {
                Resolution = new Size(1280, 720)
            };

            this._display = displayFactory.CreateDisplay(this._displayProperties, this.Draw);

            // Create world.
            this._physicalWorld = physicsFactory.CreatePhysicalWorld();
            this._bodies = new List<IBody<Polygon>>();
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

                // We apply force (towards left) at a point in space to induce torque...
                var force = new Vector2(-10, 10);
                var forceOrigin = new Point(300, 300);
                foreach (var body in this._bodies)
                {
                   body.ApplyForceAtPointInSpace(force, forceOrigin);
                }

                // Step the simulation.
                this._physicalWorld.Step(elapsedSimTimeInSeconds);
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
            // Spawn some bodies.
            var polygon = new Polygon(
                new Point(0, 0),
                new Point(100, 0),
                new Point(130, 50),
                new Point(110, 100),
                new Point(50, 100));

            for (int i = 0; i < 5; i++)
            {
                var factor = (i + 1) * 1.2;

                var body = this._physicalWorld.SpawnRigidBody(
                    30,
                    polygon,
                    new Point(factor * 150, this._displayProperties.Resolution.Height));

                this._bodies.Add(body);
            }
        }

        /// <summary>
        /// Is called to actually draw the frame using the <see cref="IDrawingContext"/>.
        /// </summary>
        private void Draw(IDrawingContext drawingContext)
        {
            Checks.AssertNotNull(drawingContext, nameof(drawingContext));

            // Draw the cross.
            var width = this._displayProperties.Resolution.Width;
            var height = this._displayProperties.Resolution.Height;

            drawingContext.DrawLine(
                new Point(0, 0),
                new Point(width, height),
                Color.Green,
                2);

            drawingContext.DrawLine(
                new Point(0, height),
                new Point(width, 0),
                Color.Brown,
                2);

            // Draw the bodies.
            foreach (var body in this._bodies)
            {
                drawingContext.DrawPolygon(
                    body.Shape.Current.Corners,
                    Color.BlueViolet,
                    3);
            }
        }
    }
}

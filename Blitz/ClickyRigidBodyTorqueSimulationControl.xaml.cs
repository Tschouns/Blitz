//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.World;

    /// <summary>
    /// Interaction logic for <see cref="ClickyRigidBodyTorqueSimulationControl"/>.
    /// </summary>
    public partial class ClickyRigidBodyTorqueSimulationControl : UserControl
    {
        /// <summary>
        /// Used to create the world.
        /// </summary>
        private readonly IPhysicsFactory _physicsFactory;

        /// <summary>
        /// Used to get a "tick" event, and step the simulation.
        /// </summary>
        private readonly DispatcherTimer _dispatcherTimer;

        /// <summary>
        /// Stores all the created rigid bodies.
        /// </summary>
        private readonly IList<IBody<Polygon>> _rigidBodies;

        /// <summary>
        /// Stores the "physical world".
        /// </summary>
        private IPhysicalWorld _world;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyRigidBodyTorqueSimulationControl"/> class.
        /// </summary>
        public ClickyRigidBodyTorqueSimulationControl()
            : this(Ioc.Container.Resolve<IPhysicsFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyRigidBodyTorqueSimulationControl"/> class.
        /// </summary>
        public ClickyRigidBodyTorqueSimulationControl(IPhysicsFactory physicsFactory)
        {
            Checks.AssertNotNullIfNotDesignMode(physicsFactory, nameof(physicsFactory), this);

            this._physicsFactory = physicsFactory;

            this._dispatcherTimer = new DispatcherTimer();
            this._rigidBodies = new List<IBody<Polygon>>();

            this.InitializeComponent();

            this._dispatcherTimer.Tick += this.DispatcherTimer_Tick;
        }

        /// <summary>
        /// Is called when this control is initialized. 
        /// </summary>
        private void Grid_Initialized(object sender, EventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            // Create the world.
            this._world = this._physicsFactory.CreatePhysicalWorld();

            // Set a colourful background.
            this._canvas.Background = System.Windows.Media.Brushes.Yellow;
            this._canvas.MouseDown += this.Canvas_MouseDown;
            this._canvas.Rendering += this.RenderigCanvas_Rendering;

            this._canvas.InvalidateVisual();

            // Initialize timer.
            this._dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this._dispatcherTimer.Start();
        }

        /// <summary>
        /// Handles the <see cref="DispatcherTimer.Tick"/> event.
        /// </summary>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // We just asume we are being called again after exactly 1 second,
            // but we "slow down" time by a factor of 10.
            this._world.Step(0.1);

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            var mousePosition = eventArgs.GetPosition(this._canvas);

            var clickPosition = new Point(mousePosition.X, mousePosition.Y);

            // Left-click spawns a new body.
            if (eventArgs.LeftButton == MouseButtonState.Pressed)
            {
                var rigidBody = this._world.SpawnRigidBody(
                    20,
                    new Polygon(
                        new Point(0, 0),
                        new Point(100, 0),
                        new Point(130, 50),
                        new Point(110, 100),
                        new Point(50, 100)),
                    clickPosition);

                this._rigidBodies.Add(rigidBody);
            }

            // Right-click "pushes" all bodies to the left.
            if (eventArgs.RightButton == MouseButtonState.Pressed)
            {
                foreach (var rigidBody in this._rigidBodies)
                {
                    // We apply force (towards left) at a point in space to induce torque...
                    var force = new Vector2(-10, 0);
                    rigidBody.AddForceAtPointInSpace(force, clickPosition);
                }
            }

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            foreach (var body in this._rigidBodies)
            {
                var polygon = body.Shape.Current;
                eventArgs.DrawingHandler.DrawPolygon(polygon.Corners);
            }
        }
    }
}

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
    using Geometry.Extensions;
    using Physics.Elements;
    using Physics.World;

    /// <summary>
    /// Interaction logic for <see cref="ClickyParticleSimulationControl"/>.
    /// </summary>
    public partial class ClickyParticleSimulationControl : UserControl
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
        /// Stores all the created particles.
        /// </summary>
        private readonly IList<IParticle> _particles;

        /// <summary>
        /// Stores the "physical world".
        /// </summary>
        private IPhysicalWorld _world;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyParticleSimulationControl"/> class.
        /// </summary>
        public ClickyParticleSimulationControl()
            : this(Ioc.Container.Resolve<IPhysicsFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickyParticleSimulationControl"/> class.
        /// </summary>
        public ClickyParticleSimulationControl(IPhysicsFactory physicsFactory)
        {
            ArgumentChecks.AssertNotNullIfNotDesignMode(physicsFactory, nameof(physicsFactory), this);

            this._physicsFactory = physicsFactory;

            this._dispatcherTimer = new DispatcherTimer();
            this._particles = new List<IParticle>();

            this.InitializeComponent();

            this._dispatcherTimer.Tick += this.DispatcherTimer_Tick;
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

            // Create the world.
            this._world = this._physicsFactory.CreatePhysicalWorld();

            // Set a colourful background.
            this._canvas.Background = System.Windows.Media.Brushes.LightSeaGreen;
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
            // We just asume we are being called again after exactly 1 second.
            this._world.Step(1);

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var mousePosition = eventArgs.GetPosition(this._canvas);
            var particlePosition = new Point(mousePosition.X, mousePosition.Y);

            // We'll create three particles of different mass next to each other. They will fall at the same speed.
            var particle1 = this._world.SpawnParticle(10, particlePosition);
            var particle2 = this._world.SpawnParticle(20, particlePosition.AddVector(new Vector2(20, 10)));
            var particle3 = this._world.SpawnParticle(40, particlePosition.AddVector(new Vector2(40, 20)));

            this._particles.Add(particle1);
            this._particles.Add(particle2);
            this._particles.Add(particle3);

            this._canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            ArgumentChecks.AssertNotNull(eventArgs, nameof(eventArgs));

            foreach (var particle in this._particles)
            {
                eventArgs.DrawingHandler.DrawDot(particle.CurrentState.Position);
            }
        }
    }
}

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
    /// Interaction logic for <see cref="ClickyParticleSimulationControl"/>.
    /// </summary>
    public partial class ClickyParticleSimulationControl : UserControl
    {
        /// <summary>
        /// Used to create the world.
        /// </summary>
        private readonly IPhysicsFactory physicsFactory;

        /// <summary>
        /// Used to get a "tick" event, and step the simulation.
        /// </summary>
        private readonly DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Stores all the created particles.
        /// </summary>
        private readonly IList<IParticle> particles;

        /// <summary>
        /// Stores the "physical world".
        /// </summary>
        private IPhysicalWorld world;

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
            Checks.AssertNotNullIfNotDesignMode(physicsFactory, nameof(physicsFactory), this);

            this.physicsFactory = physicsFactory;

            this.dispatcherTimer = new DispatcherTimer();
            this.particles = new List<IParticle>();

            this.InitializeComponent();

            this.dispatcherTimer.Tick += this.DispatcherTimer_Tick;
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
            this.world = this.physicsFactory.CreatePhysicalWorld();

            // Set a colourful background.
            this.canvas.Background = System.Windows.Media.Brushes.LightSeaGreen;
            this.canvas.MouseDown += this.Canvas_MouseDown;
            this.canvas.Rendering += this.RenderigCanvas_Rendering;

            this.canvas.InvalidateVisual();

            // Initialize timer.
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();
        }

        /// <summary>
        /// Handles the <see cref="DispatcherTimer.Tick"/> event.
        /// </summary>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // We just asume we are being called again after exactly 1 second.
            this.world.Step(1);

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.MouseDown"/> event.
        /// </summary>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var mousePosition = eventArgs.GetPosition(this.canvas);
            var particlePosition = new Point(mousePosition.X, mousePosition.Y);

            var particle = this.world.CreateParticle(1, particlePosition);

            this.particles.Add(particle);

            this.canvas.InvalidateVisual();
        }

        /// <summary>
        /// Handles the <see cref="RenderingCanvas.Rendering"/> event.
        /// </summary>
        private void RenderigCanvas_Rendering(object sender, RenderingEventArgs eventArgs)
        {
            Checks.AssertNotNull(eventArgs, nameof(eventArgs));

            foreach (var particle in this.particles)
            {
                eventArgs.DrawingHandler.DrawDot(particle.CurrentState.Position);
            }
        }
    }
}

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
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.World;
    using SharpDX.Direct3D;
    using SharpDX.DXGI;
    using SharpDX.Mathematics.Interop;
    using SharpDX.Windows;
    using DX3D11 = SharpDX.Direct3D11;

    /// <summary>
    /// Simulates rigid bodies.
    /// </summary>
    public sealed class PrototypeDxRigidBodySimulation : IDisposable
    {
        /// <summary>
        /// The form used to render the simulation.
        /// </summary>
        private readonly RenderForm _renderForm;

        /// <summary>
        /// Used to create stuff.
        /// </summary>
        private readonly IPhysicsFactory _physicsFactory;

        /// <summary>
        /// The "physical world".
        /// </summary>
        private readonly IPhysicalWorld _world;

        /// <summary>
        /// Stores all the objects simulated.
        /// </summary>
        private readonly IList<IBody<Polygon>> _bodies;

        /// <summary>
        /// Used to render stuff.
        /// </summary>
        private SwapChain _swapChain;

        /// <summary>
        /// Used to render stuff.
        /// </summary>
        private DX3D11.DeviceContext _deviceContext;

        /// <summary>
        /// Used to render stuff.
        /// </summary>
        private DX3D11.RenderTargetView _renderTargetView;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDxRigidBodySimulation"/> class.
        /// </summary>
        public PrototypeDxRigidBodySimulation()
            : this(Ioc.Container.Resolve<IPhysicsFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDxRigidBodySimulation"/> class.
        /// </summary>
        public PrototypeDxRigidBodySimulation(IPhysicsFactory physicsFactory)
        {
            Checks.AssertNotNull(physicsFactory, nameof(physicsFactory));

            this._renderForm = new RenderForm("Rigid body torque");
            this._renderForm.ClientSize = new Size(1280, 720);
            this._renderForm.AllowUserResizing = false;

            this._physicsFactory = physicsFactory;
            this._world = this._physicsFactory.CreatePhysicalWorld();
            this._bodies = new List<IBody<Polygon>>();

            var rigidBody = this._world.SpawnRigidBody(
                20,
                new Polygon(
                    new Geometry.Elements.Point(0, 0),
                    new Geometry.Elements.Point(100, 0),
                    new Geometry.Elements.Point(130, 50),
                    new Geometry.Elements.Point(110, 100),
                    new Geometry.Elements.Point(50, 100)),
                new Geometry.Elements.Point(200, 500));
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            this.InitializeDeviceResources();

            RenderLoop.Run(this._renderForm, this.RenderCallback);
        }

        /// <summary>
        /// Disposes of all disposable allocated resources.
        /// </summary>
        public void Dispose()
        {
            this._renderForm.Dispose();
            this._renderTargetView.Dispose();
        }

        /// <summary>
        /// Initializes device resources.
        /// </summary>
        private void InitializeDeviceResources()
        {
            ModeDescription backBufferDesc = new ModeDescription(
                this._renderForm.ClientSize.Width,
                this._renderForm.ClientSize.Height,
                new Rational(60, 1),
                Format.R8G8B8A8_UNorm);

            SwapChainDescription swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = this._renderForm.Handle,
                IsWindowed = true
            };

            DX3D11.Device d3dDevice;

            DX3D11.Device.CreateWithSwapChain(
                DriverType.Hardware,
                DX3D11.DeviceCreationFlags.None,
                swapChainDesc,
                out d3dDevice,
                out this._swapChain);

            this._deviceContext = d3dDevice.ImmediateContext;

            using (var backBuffer = this._swapChain.GetBackBuffer<DX3D11.Texture2D>(0))
            {
                this._renderTargetView = new DX3D11.RenderTargetView(d3dDevice, backBuffer);
            }

            this._deviceContext.OutputMerger.SetRenderTargets(this._renderTargetView);
        }

        /// <summary>
        /// Renders the simulation.
        /// </summary>
        private void RenderCallback()
        {
            this.Draw();
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        private void Draw()
        {
            this._deviceContext.ClearRenderTargetView(
                this._renderTargetView,
                new RawColor4(0.6f, 0.7f, 1.0f, 1.0f));

            this._swapChain.Present(1, PresentFlags.None);
        }
    }
}

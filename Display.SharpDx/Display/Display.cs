﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Display
{
    using System;
    using System.Windows.Forms;
    using Base.RuntimeChecks;
    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;
    using SharpDX.Windows;
    using Sprites;
    using global::Display.Sprites;

    /// <summary>
    /// Implementation of <see cref="IDisplay"/>, based on <see cref="SharpDX"/>.
    /// </summary>
    public sealed class Display : IDisplay
    {
        /// <summary>
        /// The call-back method allowing the client to draw using the <see cref="IDrawingContext"/>.
        /// </summary>
        private readonly Action<IDrawingContext> _drawCallback;

        private RenderForm _renderForm;
        private SharpDX.Direct3D11.Device _device;
        private SwapChain _swapChain;
        private Surface _backBuffer;
        private RenderTarget _renderTarget;
        private SpriteManager _spriteManager;

        /// <summary>
        /// The <see cref="IDrawingContext"/> used to "draw". Essentially a wrapper of <see cref="RenderTarget"/>.
        /// </summary>
        private RenderTargetDrawingContext _drawingContext;

        /// <summary>
        /// See <see cref="IDisplay.Properties"/>.
        /// </summary>
        public DisplayProperties Properties { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Display"/> class.
        /// </summary>
        public Display(
            IBitmapLoader bitmapLoader,
            DisplayProperties properties,
            Action<IDrawingContext> drawCallback)
        {
            ArgumentChecks.AssertNotNull(bitmapLoader, nameof(bitmapLoader));
            ArgumentChecks.AssertNotNull(drawCallback, nameof(drawCallback));

            this.Properties = properties;
            this._drawCallback = drawCallback;

            this.InitializeRenderForm();
            this.InitializeDeviceResources();
            this.SetupAltEnterHandling();
            this.InitializeRenderTarget();
            this.InitializeDrawingContext();
            this.InitializeSpriteManager(bitmapLoader);
        }

        /// <summary>
        /// See <see cref="IDisplay.SpriteManager"/>.
        /// </summary>
        public ISpriteManager SpriteManager => this._spriteManager;

        /// <summary>
        /// See <see cref="IDisplay.Show"/>.
        /// </summary>
        public void Show()
        {
            if (this._renderForm.IsDisposed)
            {
                throw new InvalidOperationException("The display has already been disposed.");
            }

            this._renderForm.Show();
        }

        /// <summary>
        /// See <see cref="IDisplay.DrawFrame"/>.
        /// </summary>
        public bool DrawFrame()
        {
            Application.DoEvents();

            this._renderTarget.BeginDraw();

            this._renderTarget.Transform = Matrix3x2.Identity;
            this._renderTarget.Clear(Color.Black);

            // Call-back
            this._drawCallback(this._drawingContext);

            this._renderTarget.EndDraw();

            this._swapChain.Present(0, PresentFlags.None);

            // returns false if the form has been closed.
            return !this._renderForm.IsDisposed;
        }

        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            this._renderForm.Dispose();
            this._device.Dispose();
            this._swapChain.Dispose();
            this._backBuffer.Dispose();
            this._renderTarget.Dispose();
            this._spriteManager.Dispose();
        }

        /// <summary>
        /// Initializes the <see cref="RenderForm"/>.
        /// </summary>
        private void InitializeRenderForm()
        {
            this._renderForm = new RenderForm(this.Properties.Title);

            // Set window size
            this._renderForm.ClientSize = this.Properties.Resolution;

            // Prevent window from being re-sized
            this._renderForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        /// <summary>
        /// Initializes device resources.
        /// </summary>
        private void InitializeDeviceResources()
        {
            // Create swap chain description
            var swapChainDesc = new SwapChainDescription()
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = this._renderForm.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            // Create swap chain and Direct3D device.
            // The BgraSupport flag is needed for Direct2D compatibility otherwise RenderTarget.FromDXGI will fail!
            SharpDX.Direct3D11.Device.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.BgraSupport,
                swapChainDesc,
                out this._device,
                out this._swapChain);

            // Get back buffer in a Direct2D-compatible format (DXGI surface)
            this._backBuffer = Surface.FromSwapChain(this._swapChain, 0);
        }

        /// <summary>
        /// Sets up a proper handling for ALT+Enter (full screen).
        /// </summary>
        private void SetupAltEnterHandling()
        {
            // Disable automatic ALT+Enter processing because it doesn't work properly with WinForms.
            using (var factory = this._swapChain.GetParent<SharpDX.DXGI.Factory1>())
            {
                factory.MakeWindowAssociation(
                    this._renderForm.Handle,
                    WindowAssociationFlags.IgnoreAltEnter);
            }

            // Add event handler for ALT+Enter.
            this._renderForm.KeyDown += (sender, eventArgs) =>
            {
                if (eventArgs.Alt && eventArgs.KeyCode == Keys.Enter)
                {
                    this._swapChain.IsFullScreen = !this._swapChain.IsFullScreen;
                }
            };
        }

        /// <summary>
        /// Initializes the <see cref="RenderTarget"/>.
        /// </summary>
        private void InitializeRenderTarget()
        {
            // Create Direct2D factory
            using (var factory = new SharpDX.Direct2D1.Factory())
            {
                // Get desktop DPI
                var dpi = factory.DesktopDpi;

                // Create bitmap render target from DXGI surface
                this._renderTarget = new RenderTarget(
                    factory,
                    this._backBuffer,
                    new RenderTargetProperties()
                    {
                        DpiX = dpi.Width,
                        DpiY = dpi.Height,
                        MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                        PixelFormat = new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Ignore),
                        Type = RenderTargetType.Default,
                        Usage = RenderTargetUsage.None
                    });
            }
        }

        /// <summary>
        /// Initializes the <see cref="RenderTargetDrawingContext"/>.
        /// </summary>
        private void InitializeDrawingContext()
        {
            this._drawingContext = new RenderTargetDrawingContext(
                this._renderTarget,
                this._renderForm.ClientSize.Height);
        }

        /// <summary>
        /// Initializes the <see cref="ISpriteManager"/>.
        /// </summary>
        private void InitializeSpriteManager(IBitmapLoader bitmapLoader)
        {
            ArgumentChecks.AssertNotNull(bitmapLoader, nameof(bitmapLoader));

            this._spriteManager = new SpriteManager(
                bitmapLoader,
                this._renderTarget,
                this._renderForm.ClientSize.Height);
        }
    }
}

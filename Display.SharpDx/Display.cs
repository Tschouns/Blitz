//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx
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

    /// <summary>
    /// Implementation of <see cref="IDisplay"/>, based on <see cref="SharpDX"/>.
    /// </summary>
    public sealed class Display : IDisplay
    {
        /// <summary>
        /// The call-back method allowing the client to draw using the <see cref="IDrawingContext"/>.
        /// </summary>
        private readonly Action<IDrawingContext> _drawCallback;

        /// <summary>
        /// The form used to display content.
        /// </summary>
        private RenderForm _renderForm;

        /// <summary>
        /// The <c>Direct3D</c> device.
        /// </summary>
        private SharpDX.Direct3D11.Device _device;

        /// <summary>
        /// The swap chain.
        /// </summary>
        private SwapChain _swapChain;

        /// <summary>
        /// The back buffer.
        /// </summary>
        private Surface _backBuffer;

        /// <summary>
        /// The render target to draw to.
        /// </summary>
        private RenderTarget _renderTarget;

        /// <summary>
        /// The <see cref="IDrawingContext"/> used to "draw". Essentially a wrapper of <see cref="RenderTarget"/>.
        /// </summary>
        private RenderTargetDrawingContext _drawingContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Display"/> class.
        /// </summary>
        public Display(DisplayProperties properties, Action<IDrawingContext> drawCallback)
        {
            Checks.AssertNotNull(drawCallback, nameof(drawCallback));

            this._drawCallback = drawCallback;

            this.InitializeRenderForm(properties.Resolution);
            this.InitializeDeviceResources();
            this.InitializeRenderTarget();
            this.InitializeDrawingContext();
        }

        /// <summary>
        /// See <see cref="IDisplay.Draw"/>.
        /// </summary>
        public void Draw()
        {
            this._renderTarget.BeginDraw();

            this._renderTarget.Transform = Matrix3x2.Identity;
            this._renderTarget.Clear(Color.YellowGreen);

            // Call-back
            this._drawCallback(this._drawingContext);

            this._renderTarget.EndDraw();

            this._swapChain.Present(0, PresentFlags.None);
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
        }

        /// <summary>
        /// Initializes the <see cref="RenderForm"/>.
        /// </summary>
        private void InitializeRenderForm(System.Drawing.Size formSize)
        {
            this._renderForm = new RenderForm("2D Shapes");

            // Disable automatic ALT+Enter processing because it doesn't work properly with WinForms.
            using (var factory = this._swapChain.GetParent<SharpDX.DXGI.Factory1>())
            {
                factory.MakeWindowAssociation(
                    this._renderForm.Handle,
                    WindowAssociationFlags.IgnoreAltEnter);
            }

            // Add event handler for ALT+Enter.
            this._renderForm.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                {
                    this._swapChain.IsFullScreen = !this._swapChain.IsFullScreen;
                }
            };

            // Set window size
            this._renderForm.Size = formSize;

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
            this._drawingContext = new RenderTargetDrawingContext(this._renderTarget);
        }
    }
}

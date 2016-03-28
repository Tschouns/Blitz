//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using System.Windows.Forms;
    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;
    using SharpDX.Windows;

    /// <summary>
    /// Prototype based on: <c>https://katyscode.wordpress.com/2013/08/24/c-directx-api-face-off-slimdx-vs-sharpdx-which-should-you-choose/</c>
    /// </summary>
    public sealed class PrototypeDx2DShapesDemo : IDisposable
    {
        /// <summary>
        /// The form used to render the simulation.
        /// </summary>
        private readonly RenderForm _renderForm;

        /// <summary>
        /// The device.
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
        /// A number used to experiment.
        /// </summary>
        private float _number;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDx2DShapesDemo"/> class.
        /// </summary>
        public PrototypeDx2DShapesDemo()
        {
            this._renderForm = new RenderForm("2D Shapes");
            this._number = 0.0f;

            this.InitializeDeviceResources();
            this.SetRenderFormSize();
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            RenderLoop.Run(this._renderForm, this.RenderCallback);
        }

        /// <summary>
        /// Disposes of all disposable allocated resources.
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
        /// Sets the window size.
        /// </summary>
        private void SetRenderFormSize()
        {
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
            this._renderForm.Size = new System.Drawing.Size(640, 480);

            // Prevent window from being re-sized
            this._renderForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
            var transformationMatrix = Matrix3x2.Rotation(this._number);
            ////this._number += 0.0001f;

            this._renderTarget.BeginDraw();
            ////this._renderTarget.Transform = Matrix3x2.Identity;
            this._renderTarget.Transform = transformationMatrix;
            this._renderTarget.Clear(Color.YellowGreen);
            
            this.DrawScene();

            this._renderTarget.EndDraw();

            this._swapChain.Present(0, PresentFlags.None);
        }

        /// <summary>
        /// Draws different stuff...
        /// </summary>
        private void DrawScene()
        {
            using (var grayBrush = new SolidColorBrush(this._renderTarget, Color.LightSlateGray))
            {
                // Draw the vertical gray grid lines.
                for (int x = 0; x < this._renderTarget.Size.Width; x += 10)
                {
                    this._renderTarget.DrawLine(
                        new Vector2(x, 0),
                        new Vector2(x, this._renderTarget.Size.Height),
                        grayBrush,
                        0.5f);
                }

                // Draw the horizontal gray grid lines.
                for (int y = 0; y < this._renderTarget.Size.Height; y += 10)
                {
                    this._renderTarget.DrawLine(
                        new Vector2(0, y),
                        new Vector2(this._renderTarget.Size.Width, y),
                        grayBrush,
                        0.5f);
                }

                // Draw the gray "filled" rectangle area.
                this._renderTarget.FillRectangle(
                    new RectangleF((this._renderTarget.Size.Width / 2) - 50, (this._renderTarget.Size.Height / 2) - 50, 100, 100),
                    grayBrush);
            }

            // Draw the blue rectangle.
            using (var blueBrush = new SolidColorBrush(this._renderTarget, Color.CornflowerBlue))
            {
                this._renderTarget.DrawRectangle(
                    new RectangleF((this._renderTarget.Size.Width / 2) - 100, (this._renderTarget.Size.Height / 2) - 100, 200, 200),
                    blueBrush);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using System.Drawing;
    using System.Linq;
    using SharpDX;
    using SharpDX.D3DCompiler;
    using SharpDX.Direct3D;
    using SharpDX.DXGI;
    using SharpDX.Mathematics.Interop;
    using SharpDX.Windows;
    using DX3D11 = SharpDX.Direct3D11;

    /// <summary>
    /// Draws a triangle in 3D space.
    /// </summary>
    public sealed class PrototypeDx3DTriangleDemo : IDisposable
    {
        /// <summary>
        /// The form used to render the simulation.
        /// </summary>
        private readonly RenderForm _renderForm;

        /// <summary>
        /// Used to render stuff.
        /// </summary>
        private DX3D11.Device _device;

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
        /// The triangle vertices.
        /// </summary>
        private Vector3[] _vertices;

        /// <summary>
        /// Stores the triangle vertices.
        /// </summary>
        private DX3D11.Buffer _triangleVertexBuffer;

        /// <summary>
        /// The vertex shader.
        /// </summary>
        private DX3D11.VertexShader _vertexShader;

        /// <summary>
        /// The pixel shader.
        /// </summary>
        private DX3D11.PixelShader _pixelShader;

        /// <summary>
        /// The shader input signature.
        /// </summary>
        private ShaderSignature _inputSignature;

        /// <summary>
        /// The shader input layout.
        /// </summary>
        private DX3D11.InputLayout _inputLayout;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeDx3DTriangleDemo"/> class.
        /// </summary>
        public PrototypeDx3DTriangleDemo()
        {
            var screenSize = new Size(1280, 720);

            this._renderForm = new RenderForm("3D Triangle");
            this._renderForm.ClientSize = screenSize;
            this._renderForm.AllowUserResizing = true;

            this.InitializeDeviceResources();
            this.InitializeShaders();
            this.SetViewPort();
            this.InitializeTriangle();
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
            this._device.Dispose();
            this._swapChain.Dispose();
            this._deviceContext.Dispose();
            this._renderTargetView.Dispose();
            this._renderForm.Dispose();

            this._triangleVertexBuffer.Dispose();
            this._vertexShader.Dispose();
            this._pixelShader.Dispose();
            this._inputSignature.Dispose();
            this._inputLayout.Dispose();
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

            // Create device and swap chain.
            DX3D11.Device.CreateWithSwapChain(
                DriverType.Hardware,
                DX3D11.DeviceCreationFlags.None,
                swapChainDesc,
                out this._device,
                out this._swapChain);

            // Get device context.
            this._deviceContext = this._device.ImmediateContext;

            // Create target view.
            using (var backBuffer = this._swapChain.GetBackBuffer<DX3D11.Texture2D>(0))
            {
                this._renderTargetView = new DX3D11.RenderTargetView(this._device, backBuffer);
            }

            this._deviceContext.OutputMerger.SetRenderTargets(this._renderTargetView);
        }

        /// <summary>
        /// Initializes the vertex and pixel shader.
        /// </summary>
        private void InitializeShaders()
        {
            var inputElements = new[]
            {
                new DX3D11.InputElement("POSITION", 0, Format.R32G32B32_Float, 0)
            };

            // Compile HLSL code, create shaders.
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile("VertexShader.hlsl", "main", "vs_4_0", ShaderFlags.Debug))
            {
                this._vertexShader = new DX3D11.VertexShader(this._device, vertexShaderByteCode);
                this._inputSignature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
                this._inputLayout = new DX3D11.InputLayout(this._device, vertexShaderByteCode, inputElements);
            }

            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile("PixelShader.hlsl", "main", "ps_4_0", ShaderFlags.Debug))
            {
                this._pixelShader = new DX3D11.PixelShader(this._device, pixelShaderByteCode);
            }

            // Set as current vertex and pixel shaders.
            this._deviceContext.VertexShader.Set(this._vertexShader);
            this._deviceContext.PixelShader.Set(this._pixelShader);

            this._deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // Set input layout.
            this._deviceContext.InputAssembler.InputLayout = this._inputLayout;
        }

        /// <summary>
        /// Sets the view port.
        /// </summary>
        private void SetViewPort()
        {
            var viewport = new Viewport(
                0,
                0,
                this._renderForm.ClientSize.Width,
                this._renderForm.ClientSize.Height);

            this._deviceContext.Rasterizer.SetViewport(viewport);
        }

        /// <summary>
        /// Initializes the triangle.
        /// </summary>
        private void InitializeTriangle()
        {
            this._vertices = new Vector3[]
            {
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(0f, -0.5f, 0.0f), 
            };

            this._triangleVertexBuffer = DX3D11.Buffer.Create(
                this._device,
                DX3D11.BindFlags.VertexBuffer,
                this._vertices);
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

            this._deviceContext.InputAssembler.SetVertexBuffers(
                0,
                new DX3D11.VertexBufferBinding(this._triangleVertexBuffer, Utilities.SizeOf<Vector3>(), 0));
            this._deviceContext.Draw(this._vertices.Count(), 0);

            this._swapChain.Present(1, PresentFlags.None);
        }
    }
}

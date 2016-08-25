//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx.PrototypeRenderLoopDemo
{
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Camera;
    using Display;
    using Geometry.Algorithms.Gjk;
    using Geometry.Elements;
    using Input;
    using RenderLoop.Loop;

    /// <summary>
    /// A prototype to test the <see cref="RenderLoop"/> namespace.
    /// </summary>
    public class DemoGame
    {
        /// <summary>
        /// Needed by the <see cref="DemoGameLogic"/>.
        /// </summary>
        private readonly IInputFactory _inputFactory;

        /// <summary>
        /// Needed by the <see cref="DemoGameLogic"/>.
        /// </summary>
        private readonly ICameraFactory _cameraFactory;

        /// <summary>
        /// Needed by the <see cref="DemoGameLogic"/>.
        /// </summary>
        private readonly IGjkAlgorithm<Circle, Polygon> _gjk;

        /// <summary>
        /// Needed by the <see cref="DemoGameDisplayRenderer"/>.
        /// </summary>
        private readonly IDisplayFactory _displayFactory;

        /// <summary>
        /// Used to create the loop.
        /// </summary>
        private readonly ILoopFactory _loopFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGame"/> class.
        /// </summary>
        public DemoGame()
            : this(
                  Ioc.Container.Resolve<IInputFactory>(),
                  Ioc.Container.Resolve<ICameraFactory>(),
                  Ioc.Container.Resolve<IGjkAlgorithm<Circle, Polygon>>(),
                  Ioc.Container.Resolve<IDisplayFactory>(),
                  Ioc.Container.Resolve<ILoopFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGame"/> class.
        /// </summary>
        public DemoGame(
            IInputFactory inputFactory,
            ICameraFactory cameraFactory,
            IGjkAlgorithm<Circle, Polygon> gjk,
            IDisplayFactory displayFactory,
            ILoopFactory loopFactory)
        {
            Checks.AssertNotNull(inputFactory, nameof(inputFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));
            Checks.AssertNotNull(gjk, nameof(gjk));
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(loopFactory, nameof(loopFactory));

            this._inputFactory = inputFactory;
            this._cameraFactory = cameraFactory;
            this._gjk = gjk;
            this._displayFactory = displayFactory;
            this._loopFactory = loopFactory;
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            // Define display properties.
            var displaySize = new System.Drawing.Size(1280, 720);
            var displayProperties = new DisplayProperties
            {
                Title = "Render Loop Demo",
                Resolution = displaySize
            };

            using (var demoGameDisplayRenderer = new DemoGameDisplayRenderer(this._displayFactory, displayProperties))
            {
                var loop = this._loopFactory.CreateLoop(
                    new DemoGameLogic(this._inputFactory, this._cameraFactory, this._gjk, displaySize),
                    demoGameDisplayRenderer);

                demoGameDisplayRenderer.ShowDisplay();
                loop.Run();
            }
        }
    }
}

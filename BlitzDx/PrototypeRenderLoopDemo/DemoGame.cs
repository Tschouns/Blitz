//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx.PrototypeRenderLoopDemo
{
    using Base.InversionOfControl;
    using Base.RuntimeChecks;
    using Display;
    using RenderLoop.Loop;

    /// <summary>
    /// A prototype to test the <see cref="RenderLoop"/> namespace.
    /// </summary>
    public class DemoGame
    {
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
                  Ioc.Container.Resolve<IDisplayFactory>(),
                  Ioc.Container.Resolve<ILoopFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGame"/> class.
        /// </summary>
        public DemoGame(
            IDisplayFactory displayFactory,
            ILoopFactory loopFactory)
        {
            Checks.AssertNotNull(displayFactory, nameof(displayFactory));
            Checks.AssertNotNull(loopFactory, nameof(loopFactory));

            this._displayFactory = displayFactory;
            this._loopFactory = loopFactory;
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void Run()
        {
            // Define display properties.
            var displayProperties = new DisplayProperties
            {
                Title = "Render Loop Demo",
                Resolution = new System.Drawing.Size(1280, 720)
            };

            using (var demoGameDisplayRenderer = new DemoGameDisplayRenderer(this._displayFactory, displayProperties))
            {
                var loop = this._loopFactory.CreateLoop(
                    new DemoGameLogic(),
                    demoGameDisplayRenderer);

                demoGameDisplayRenderer.ShowDisplay();
                loop.Run();
            }
        }
    }
}

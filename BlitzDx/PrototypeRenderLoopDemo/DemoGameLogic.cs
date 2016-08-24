//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx.PrototypeRenderLoopDemo
{
    using Base.RuntimeChecks;
    using RenderLoop;
    using RenderLoop.Callback;
    using RenderLoop.Loop;

    /// <summary>
    /// Represents the "game logic", besides drawing, for this demo. Implements <see cref="IUpdateCallback"/>.
    /// </summary>
    public class DemoGameLogic : IUpdateCallback<DemoGameState>
    {
        /// <summary>
        /// See <see cref="IUpdateCallback{TGameState}.Update(TimeInfo, TimeInfo, ILoopCommand)"/>.
        /// </summary>
        public DemoGameState Update(
            TimeInfo realTime,
            TimeInfo gameTime,
            ILoopCommand loopCommand)
        {
            Checks.AssertNotNull(realTime, nameof(realTime));
            Checks.AssertNotNull(gameTime, nameof(gameTime));
            Checks.AssertNotNull(loopCommand, nameof(loopCommand));

            return new DemoGameState();
        }
    }
}

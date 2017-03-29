//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace RenderLoop.Services.Loop
{
    using Base.RuntimeChecks;
    using Callback;
    using RenderLoop.Loop;

    /// <summary>
    /// See <see cref="ILoopFactory"/>.
    /// </summary>
    public class LoopFactory : ILoopFactory
    {
        /// <summary>
        /// See <see cref="ILoopFactory.CreateLoop{TGameState}(IUpdateCallback{TGameState}, IDrawCallback{TGameState})"/>.
        /// </summary>
        /// <typeparam name="TGameState">
        /// See <see cref="ILoopFactory.CreateLoop{TGameState}(IUpdateCallback{TGameState}, IDrawCallback{TGameState})"/>.
        /// </typeparam>
        public ILoop CreateLoop<TGameState>(
            IUpdateCallback<TGameState> updateCallback,
            IDrawCallback<TGameState> drawCallback)
            where TGameState : class
        {
            ArgumentChecks.AssertNotNull(updateCallback, nameof(updateCallback));
            ArgumentChecks.AssertNotNull(drawCallback, nameof(drawCallback));

            return new Loop.Loop<TGameState>(updateCallback, drawCallback);
        }
    }
}

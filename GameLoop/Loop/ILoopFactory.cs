//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace RenderLoop.Loop
{
    using RenderLoop.Callback;
    using Loop;

    /// <summary>
    /// Creates the loop.
    /// </summary>
    public interface ILoopFactory
    {
        /// <summary>
        /// Creates the loop around the specified update and draw callback routines.
        /// </summary>
        /// <typeparam name="TGameState">
        /// Type of the "game state"
        /// </typeparam>
        ILoop CreateLoop<TGameState>(
            IUpdateCallback<TGameState> updateCallback,
            IDrawCallback<TGameState> drawCallback)
            where TGameState : class;
    }
}

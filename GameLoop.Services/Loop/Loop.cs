//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Services.Loop
{
    using System;
    using Base.RuntimeChecks;
    using GameLoop.Callback;

    /// <summary>
    /// See <see cref="ILoop"/>.
    /// </summary>
    /// <typeparam name="TGameState">
    /// The type of the "game state"
    /// </typeparam>
    public class Loop<TGameState> : ILoop
        where TGameState : class
    {
        /// <summary>
        /// The "update" callback for this loop.
        /// </summary>
        private readonly IUpdateCallback<TGameState> _updateCallback;

        /// <summary>
        /// The "draw" callback for this loop.
        /// </summary>
        private readonly IDrawCallback<TGameState> _drawCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loop{TGameState}"/> class.
        /// </summary>
        public Loop(
            IUpdateCallback<TGameState> updateCallback,
            IDrawCallback<TGameState> drawCallback)
        {
            Checks.AssertNotNull(updateCallback, nameof(updateCallback));
            Checks.AssertNotNull(drawCallback, nameof(drawCallback));

            this._updateCallback = updateCallback;
            this._drawCallback = drawCallback;
        }

        /// <summary>
        /// See <see cref="ILoop.Run"/>.
        /// </summary>
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}

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
    using GameLoop.Loop;

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
            this.Run(1.0);
        }

        /// <summary>
        /// See <see cref="ILoop.Run(double)"/>.
        /// </summary>
        public void Run(double gameTimeFactor)
        {
            // Initialize time info.
            var time = DateTime.Now;
            double realTimeTotal = 0.0;
            double gameTimeTotal = 0.0;

            // Initialize loop command.
            var isAlive = true;
            var loopCommand = new LoopCommand(
                gameTimeFactor,
                () => isAlive = false);

            do
            {
                // Calculate time info.
                var now = DateTime.Now;
                var realTimeElapsed = (now - time).TotalSeconds;
                var gameTimeElapsed = realTimeElapsed * loopCommand.GameTimeFactor;
                realTimeTotal += realTimeElapsed;
                gameTimeTotal += gameTimeElapsed;

                // Update.
                var gameState = this._updateCallback.Update(
                    new TimeInfo(realTimeElapsed, realTimeTotal),
                    new TimeInfo(gameTimeElapsed, gameTimeTotal),
                    loopCommand);

                // Draw.
                isAlive &= this._drawCallback.Draw(gameState);
            }
            while (isAlive);
        }
    }
}

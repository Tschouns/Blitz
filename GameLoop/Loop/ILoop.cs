//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Loop
{
    /// <summary>
    /// Represents the game/sim loop.
    /// </summary>
    public interface ILoop
    {
        /// <summary>
        /// Runs the loop.
        /// </summary>
        void Run();

        /// <summary>
        /// Runs the loop. Sets the "game time" factor, which determines how fast time will pass in
        /// the game, relative to real time.
        /// </summary>
        void Run(double gameTimeFactor);
    }
}

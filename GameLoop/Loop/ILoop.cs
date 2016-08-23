//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop
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
    }
}

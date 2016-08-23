//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Callback
{
    /// <summary>
    /// Provides the "update callback" method, which creates the "game state" as an output.
    /// </summary>
    public interface IUpdateCallback<TGameState>
        where TGameState : class
    {
        /// <summary>
        /// Updates the game/sim. Produces a "game state" as output.
        /// </summary>
        TGameState Update(TimeInfo realTime, TimeInfo inGameTime, ILoopCommand loopCommand);
    }
}

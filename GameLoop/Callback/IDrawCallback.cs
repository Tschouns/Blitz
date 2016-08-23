//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Callback
{
    /// <summary>
    /// Provides the "draw callback" method, which takes the "game state" as an argument and
    /// renders it to the display.
    /// </summary>
    /// <typeparam name="TGameState">
    /// The type of the "game state"
    /// </typeparam>
    public interface IDrawCallback<TGameState>
        where TGameState : class
    {
        /// <summary>
        /// Updates the "game state".
        /// </summary>
        /// <returns>
        /// <c>true</c> as long as drawing is still possible. Returning <c>false</c> will
        /// cause the loop to end.
        /// </returns>
        bool Draw(TGameState gameState);
    }
}

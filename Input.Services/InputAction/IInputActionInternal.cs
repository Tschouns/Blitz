//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services.InputAction
{
    /// <summary>
    /// Extends <see cref="IInputAction"/>, allows to step.
    /// </summary>
    public interface IInputActionInternal : IInputAction
    {
        /// <summary>
        /// Updates the input action, potentially with regard to elapsed time. This
        /// is supposed to be called once in the game/sim update loop.
        /// </summary>
        void Update(double realTimeElapsed);
    }
}

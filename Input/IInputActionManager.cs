//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input
{
    using System.Windows.Input;

    /// <summary>
    /// Creates, holds and updates a set of input actions.
    /// </summary>
    public interface IInputActionManager
    {
        /// <summary>
        /// Registers an input action which is active while the specified button is pressed.
        /// </summary>
        IInputAction RegisterKeyboardButtonHoldAction(Key key);

        /// <summary>
        /// Registers an input action which becomes active for a single update cycle when a certain key
        /// is pressed. The user has to release and press the button anew in order for the action
        /// to become active again.
        /// </summary>
        IInputAction RegisterKeyboardButtonHitAction(Key key);

        /// <summary>
        /// Updates the registered input actions, potentially with regard to elapsed time.
        /// This is supposed to be called once in the game/sim update loop.
        /// </summary>
        void Update(double realTimeElapsed);
    }
}

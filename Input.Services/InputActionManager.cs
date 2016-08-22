//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Base.RuntimeChecks;
    using InputAction;

    /// <summary>
    /// See <see cref="IInputActionManager"/>.
    /// </summary>
    public class InputActionManager : IInputActionManager
    {
        /// <summary>
        /// Stores all the registered input actions.
        /// </summary>
        private readonly IList<IInputActionInternal> _inputActions = new List<IInputActionInternal>();

        /// <summary>
        /// See <see cref="IInputActionManager.RegisterKeyboardButtonHoldAction(Key)"/>.
        /// </summary>
        public IInputAction RegisterKeyboardButtonHoldAction(Key key)
        {
            var inputAction = new KeyboardButtonHoldInputAction(key);
            this._inputActions.Add(inputAction);

            return inputAction;
        }

        /// <summary>
        /// See <see cref="IInputActionManager.RegisterKeyboardButtonHitAction(Key)"/>.
        /// </summary>
        public IInputAction RegisterKeyboardButtonHitAction(Key key)
        {
            var inputAction = new KeyboardButtonHitInputAction(key);
            this._inputActions.Add(inputAction);

            return inputAction;
        }

        /// <summary>
        /// See <see cref="IInputActionManager.Update(double)"/>.
        /// </summary>
        public void Update(double realTimeElapsed)
        {
            Checks.AssertIsPositive(realTimeElapsed, nameof(realTimeElapsed));

            foreach(var inputAction in this._inputActions)
            {
                inputAction.Update(realTimeElapsed);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services.InputAction
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Input.Button;
    using Input.InputAction;

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
        /// See <see cref="IInputActionManager.RegisterButtonHoldAction(IButton)"/>.
        /// </summary>
        public IInputAction RegisterButtonHoldAction(IButton button)
        {
            Checks.AssertNotNull(button, nameof(button));

            var inputAction = new ButtonHoldInputAction(button);
            this._inputActions.Add(inputAction);

            return inputAction;
        }

        /// <summary>
        /// See <see cref="IInputActionManager.RegisterButtonHitAction(IButton)"/>.
        /// </summary>
        public IInputAction RegisterButtonHitAction(IButton button)
        {
            Checks.AssertNotNull(button, nameof(button));

            var inputAction = new ButtonHitInputAction(button);
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

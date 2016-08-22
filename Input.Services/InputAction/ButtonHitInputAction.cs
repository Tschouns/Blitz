//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services.InputAction
{
    using Base.RuntimeChecks;
    using Input.Button;

    /// <summary>
    /// See <see cref="IInputAction"/>. This type of action becomes active for one single update cycle (!) when
    /// a certain button is pressed. The user has to release and press the button anew in order for the action
    /// to become active again.
    /// </summary>
    public class ButtonHitInputAction : IInputActionInternal
    {
        /// <summary>
        /// Stores the button.
        /// </summary>
        private readonly IButton _button;

        /// <summary>
        /// Becomes true when the pressing of a button has been processed, i.e. when the action has been
        /// active for an update cycle. It is reset when the button is released.
        /// </summary>
        private bool _isAlreadyProcessed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonHitInputAction"/> class.
        /// </summary>
        public ButtonHitInputAction(IButton button)
        {
            Checks.AssertNotNull(button, nameof(button));

            this._button = button;
        }

        /// <summary>
        /// See <see cref="IInputAction.IsActive"/>.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// See <see cref="IInputActionInternal"/>.
        /// </summary>
        public void Update(double realTimeElapsed)
        {
            if (this._button.IsPressed)
            {
                this.IsActive = !this._isAlreadyProcessed;
                this._isAlreadyProcessed = true;
            }
            else
            {
                this.IsActive = false;
                this._isAlreadyProcessed = false;
            }
        }
    }
}

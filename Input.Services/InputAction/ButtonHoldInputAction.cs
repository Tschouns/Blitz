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
    /// See <see cref="IInputAction"/>. This type of action is active while a certain button is pressed. It stays
    /// active as long as the button remains pressed.
    /// </summary>
    public class ButtonHoldInputAction : IInputActionInternal
    {
        /// <summary>
        /// Stores the button.
        /// </summary>
        private readonly IButton _button;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonHoldInputAction"/> class.
        /// </summary>
        public ButtonHoldInputAction(IButton button)
        {
            Checks.AssertNotNull(button, nameof(button));

            this._button = button;
        }

        /// <summary>
        /// See <see cref="IInputAction"/>.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// See <see cref="IInputActionInternal"/>.
        /// </summary>
        public void Update(double realTimeElapsed)
        {
            this.IsActive = this._button.IsPressed;
        }
    }
}

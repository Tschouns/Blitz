//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services
{
    using Base.RuntimeChecks;
    using Input.Button;
    using Input.InputAction;
    using InputAction;

    /// <summary>
    /// See <see cref="IInputFactory"/>.
    /// </summary>
    public class InputFactory : IInputFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputFactory"/> class.
        /// </summary>
        public InputFactory(IKeyboardButtonCreator keyboardButtonCreator)
        {
            Checks.AssertNotNull(keyboardButtonCreator, nameof(keyboardButtonCreator));

            this.KeyboardButtonCreator = keyboardButtonCreator;
        }

        /// <summary>
        /// See <see cref="IInputFactory.KeyboardButtonCreator"/>.
        /// </summary>
        public IKeyboardButtonCreator KeyboardButtonCreator { get; }

        /// <summary>
        /// See <see cref="IInputActionManager"/>
        /// </summary>
        public IInputActionManager CreateInputActionManager()
        {
            return new InputActionManager();
        }
    }
}

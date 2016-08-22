//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input
{
    using Button;
    using InputAction;

    /// <summary>
    /// Creates <see cref="Input"/> components.
    /// </summary>
    public interface IInputFactory
    {
        /// <summary>
        /// Gets the <see cref="IKeyboardButtonCreator"/>.
        /// </summary>
        IKeyboardButtonCreator KeyboardButtonCreator { get; }

        /// <summary>
        /// Creates an <see cref="IInputActionManager"/>.
        /// </summary>
        IInputActionManager CreateInputActionManager();
    }
}

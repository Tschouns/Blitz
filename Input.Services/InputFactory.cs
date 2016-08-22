//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input.Services
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// See <see cref="IInputFactory"/>.
    /// </summary>
    public class InputFactory : IInputFactory
    {
        /// <summary>
        /// See <see cref="IInputActionManager"/>
        /// </summary>
        public IInputActionManager CreateInputActionManager(Key key)
        {
            return new InputActionManager();
        }
    }
}

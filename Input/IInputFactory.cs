//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Input
{
    using System.Windows.Input;

    /// <summary>
    /// Creates <see cref="Input"/> components.
    /// </summary>
    public interface IInputFactory
    {
        /// <summary>
        /// Creates an <see cref="IInputActionManager"/>.
        /// </summary>
        IInputActionManager CreateInputActionManager();
    }
}

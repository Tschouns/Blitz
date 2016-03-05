//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.StartUp
{
    using System;

    /// <summary>
    /// This exception is thrown when something goes wrong during start-up.
    /// </summary>
    [Serializable]
    public class StartUpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartUpException"/> class.
        /// </summary>
        public StartUpException(string message)
            : base(message)
        {
        }
    }
}

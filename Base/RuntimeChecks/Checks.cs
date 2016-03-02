//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.RuntimeChecks
{
    using System;

    /// <summary>
    /// Checks method arguments, and throws exceptions.
    /// </summary>
    public static class Checks
    {
        /// <summary>
        /// Asserts that the specified argument is not <c>null</c>, otherwise throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        public static void AssertNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}

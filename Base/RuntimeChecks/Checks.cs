//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.RuntimeChecks
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Checks method arguments, and throws exceptions.
    /// </summary>
    public static class Checks
    {
        /// <summary>
        /// Asserts that the specified floating point number is positive, including 0.
        /// </summary>
        public static void AssertIsPositive(double argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentException($"The floating point number {argument}, specified in argument {argumentName}, is not positive.");
            }
        }

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

        /// <summary>
        /// Asserts that the specified argument is not <c>null</c>, otherwise throws an <see cref="ArgumentNullException"/>.
        /// That is unless the specified UI element is run in the designer, then the argument is not checked.
        /// </summary>
        public static void AssertNotNullIfNotDesignMode(object argument, string argumentName, DependencyObject uiElement)
        {
            if (DesignerProperties.GetIsInDesignMode(uiElement))
            {
                return;
            }

            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}

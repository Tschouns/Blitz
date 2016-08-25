//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.Extensions
{

    /// <summary>
    /// Provides extension methods for <see cref="double"/>.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Returns the specified value, or <see cref="double.MinValue"/> or <see cref="double.MaxValue"/> if
        /// the value exceeds the value range negative or positive.
        /// </summary>
        public static double Safe(this double value)
        {
            if (double.IsNegativeInfinity(value))
            {
                return double.MinValue;
            }

            if (double.IsPositiveInfinity(value))
            {
                return double.MaxValue;
            }

            return value;
        }
    }
}

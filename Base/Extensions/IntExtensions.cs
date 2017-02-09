//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="int"/> and <see cref="uint"/>.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Rotates this <see cref="int"/> value left by the specified number of bits.
        /// </summary>
        public static int RotateLeft(this int value, int count)
        {
            var unsigned = (uint)value;

            return (int)unsigned.RotateLeft(count);
        }

        /// <summary>
        /// Rotates this <see cref="int"/> value right by the specified number of bits.
        /// </summary>
        public static int RotateRight(this int value, int count)
        {
            var unsigned = (uint)value;

            return (int)unsigned.RotateRight(count);
        }

        /// <summary>
        /// Rotates this <see cref="uint"/> value left by the specified number of bits.
        /// </summary>
        public static uint RotateLeft(this uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        /// <summary>
        /// Rotates this <see cref="uint"/> value right by the specified number of bits.
        /// </summary>
        public static uint RotateRight(this uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }
    }
}

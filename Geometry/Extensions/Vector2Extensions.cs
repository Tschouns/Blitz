//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Extensions
{
    using System;
    using Elements;

    /// <summary>
    /// Provides extension methods for <see cref="Vector2"/>.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Adds the specified vector to this vector.
        /// </summary>
        public static Vector2 AddVector(this Vector2 vector, Vector2 vectorToAdd)
        {
            return new Vector2(
                vector.X + vectorToAdd.X,
                vector.Y + vectorToAdd.Y);
        }

        /// <summary>
        /// Subtracts the specified vector from this vector.
        /// </summary>
        public static Vector2 SubtactVector(this Vector2 vector, Vector2 vectorToSubtract)
        {
            return new Vector2(
                vector.X - vectorToSubtract.X,
                vector.Y - vectorToSubtract.Y);
        }

        /// <summary>
        /// Multiplies this vector by a scalar factor.
        /// </summary>
        public static Vector2 Multiply(this Vector2 vector, double factor)
        {
            return new Vector2(
                vector.X * factor,
                vector.Y * factor);
        }

        /// <summary>
        /// Divides this vector by a scalar divisor.
        /// </summary>
        public static Vector2 Divide(this Vector2 vector, double divisor)
        {
            return new Vector2(
                vector.X / divisor,
                vector.Y / divisor);
        }

        /// <summary>
        /// Gets the "dot product"<c> U * V = Ux * Vx + Uy * Vy</c>. It is commutative.
        /// </summary>
        public static double Dot(this Vector2 u, Vector2 v)
        {
            return (u.X * v.X) + (u.Y * v.Y);
        }

        /// <summary>
        /// Gets the "cross product" <c>U x V = Ux * Vy - Uy * Vx</c>. It is anti-commutative.
        /// </summary>
        public static double Cross(this Vector2 u, Vector2 v)
        {
            return (u.X * v.Y) - (u.Y * v.X);
        }

        /// <summary>
        /// Gets the norm vector for this vector.
        /// </summary>
        public static Vector2 Norm(this Vector2 vector)
        {
            var magnitude = vector.Magnitude();

            return vector.Divide(magnitude);
        }

        /// <summary>
        /// Gets the magnitude, i.e. the "length", of this vector.
        /// </summary>
        public static double Magnitude(this Vector2 vector)
        {
            return Math.Sqrt(vector.SquaredMagnitude());
        }

        /// <summary>
        /// Gets the squared magnitude of this vector. This can be used to compare vector magnitudes (faster
        /// than <see cref="Magnitude"/>).
        /// </summary>
        public static double SquaredMagnitude(this Vector2 vector)
        {
            return (vector.X * vector.X) + (vector.Y * vector.Y);
        }
    }
}

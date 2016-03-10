//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Extensions
{
    using Elements;

    /// <summary>
    /// Provides extension methods for <see cref="Vector2"/>.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Adds a the specified vector to this vector.
        /// </summary>
        public static Vector2 AddVector(this Vector2 vector, Vector2 vectorToAdd)
        {
            return new Vector2(
                vector.X + vectorToAdd.X,
                vector.Y + vectorToAdd.Y);
        }

        /// <summary>
        /// Gets the "cross product" <c>U x V = Ux * Vy - Uy * Vx</c>. It is anti-commutative.
        /// </summary>
        public static double Cross(this Vector2 u, Vector2 v)
        {
            return (u.X * v.Y) - (u.Y * v.X);
        }

        /// <summary>
        /// Gets the "dot product"<c> U * V = Ux * Vy + Uy * Vx</c>. It is commutative.
        /// </summary>
        public static double Dot(this Vector2 u, Vector2 v)
        {
            return (u.X * v.Y) + (u.Y * v.X);
        }
    }
}

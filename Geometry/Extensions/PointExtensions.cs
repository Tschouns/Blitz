//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Extensions
{
    using Elements;

    /// <summary>
    /// Provides extension methods for <see cref="Point"/>.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Adds the specified vector to this point.
        /// </summary>
        public static Point AddVector(this Point point, Vector2 vector)
        {
            return new Point(
                point.X + vector.X,
                point.Y + vector.Y);
        }

        /// <summary>
        /// Subtracts the specified vector from this point.
        /// </summary>
        public static Point SubtactVector(this Point point, Vector2 vector)
        {
            return new Point(
                point.X - vector.X,
                point.Y - vector.Y);
        }

        /// <summary>
        /// Converts the this point to a vector.
        /// </summary>
        public static Vector2 AsVector(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        /// <summary>
        /// Gets the offset of this point, relative to another point.
        /// </summary>
        public static Vector2 GetOffsetFrom(this Point point, Point otherPoint)
        {
            return new Vector2(
                point.X - otherPoint.X,
                point.Y - otherPoint.Y);
        }
        
        /// <summary>
        /// Gets the "dot product" <c>A * B = Ax * Bx + Ay * By</c>. It is commutative.
        /// </summary>
        public static double Dot(this Point a, Point b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        /// <summary>
        /// Gets the "cross product" <c>A x B = Ax * By - Ay * Bx</c>. It is anti-commutative.
        /// </summary>
        public static double Cross(this Point a, Point b)
        {
            return (a.X * b.Y) - (a.Y * b.X);
        }
    }
}

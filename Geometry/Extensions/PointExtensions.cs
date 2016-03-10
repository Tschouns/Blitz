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
        /// Adds a the specified vector to this point.
        /// </summary>
        public static Point AddVector(this Point point, Vector2 vector)
        {
            return new Point(
                point.X + vector.X,
                point.Y + vector.Y);
        }

        /// <summary>
        /// Subtracts a the specified vector to this point.
        /// </summary>
        public static Point SubtactVector(this Point point, Vector2 vector)
        {
            return new Point(
                point.X - vector.X,
                point.Y - vector.Y);
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
    }
}

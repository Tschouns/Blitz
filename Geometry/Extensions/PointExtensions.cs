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
    }
}

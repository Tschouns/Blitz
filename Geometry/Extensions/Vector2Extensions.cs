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
    }
}

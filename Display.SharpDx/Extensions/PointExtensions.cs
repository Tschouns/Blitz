//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Extensions
{
    using Geometry.Elements;

    /// <summary>
    /// Provides extension methods for <see cref="Point"/>.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Converts the <see cref="Point"/> into a <see cref="SharpDX.Vector2"/>.
        /// </summary>
        public static SharpDX.Vector2 ToSharpDxVector2Flipped(this Point point, double height)
        {
            return new SharpDX.Vector2((float)point.X, (float)(height - point.Y));
        }
    }
}

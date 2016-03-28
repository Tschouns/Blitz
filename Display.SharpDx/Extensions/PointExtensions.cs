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
        public static SharpDX.Vector2 ToSharpDxVector2(this Point point)
        {
            return new SharpDX.Vector2((float)point.X, (float)point.Y);
        }
    }
}

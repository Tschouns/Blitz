//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Extensions
{
    using System.Drawing;

    /// <summary>
    /// Provides extension methods for <see cref="Color"/>.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts the <see cref="Color"/> into a <see cref="SharpDX.Color"/>.
        /// </summary>
        public static SharpDX.Color ToSharpDxColor(this Color color)
        {
            return new SharpDX.Color(color.R, color.G, color.B, color.A);
        }
    }
}

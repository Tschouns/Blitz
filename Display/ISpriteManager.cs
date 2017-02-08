//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using Geometry.Transformation;
    using System.Drawing;
    using System.Numerics;

    /// <summary>
    /// Manages all the sprites.
    /// </summary>
    public interface ISpriteManager
    {
        /// <summary>
        /// Loads a new sprite from a bitmap.
        /// </summary>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap);

        /// <summary>
        /// Unloads all sprites. Unloaded sprites can no longer be drawn.
        /// </summary>
        void Clear();
    }
}

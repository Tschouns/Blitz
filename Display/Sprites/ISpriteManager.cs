//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.Sprites
{
    using System.Drawing;
    using Geometry.Elements;
    using Geometry.Transformation;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// Manages all the sprites.
    /// </summary>
    public interface ISpriteManager
    {
        /// <summary>
        /// Loads a new sprite from a bitmap.
        /// </summary>
        /// <remarks>
        /// By default the sprite is transformed so that its origin is in the center.
        /// </remarks>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap);

        /// <summary>
        /// Loads a new sprite from a bitmap. Applies an inital scale, e.g. to scale the bitmap from
        /// its original resolution to the in-world size of the object it represents.
        /// </summary>
        /// <remarks>
        /// By default the sprite is transformed so that its origin is in the center.
        /// </remarks>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, double initialScale);

        /// <summary>
        /// Loads a new sprite from a bitmap. Specifies the origin of the sprite, an initial rotation
        /// about the specified origin, and an initial scale.
        /// </summary>
        /// <remarks>
        /// No other transformation is applied before the specified initial transformations.
        /// </remarks>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, Point origin, double initialRotation, double initialScale);

        /// <summary>
        /// Unloads all sprites. Unloaded sprites can no longer be drawn.
        /// </summary>
        void Clear();
    }
}

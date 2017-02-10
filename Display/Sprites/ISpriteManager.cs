//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.Sprites
{
    using Geometry.Elements;
    using Geometry.Transformation;
    using System.Drawing;

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
        /// Loads a new sprite from a bitmap. Applies an inital translation, rotation and scale; in that order.
        /// </summary>
        /// <remarks>
        /// No other transformation is applied before the specified initial transformations.
        /// </remarks>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, Vector2 initialTranslation, double initialRotation, double initialScale);

        /// <summary>
        /// Loads a new sprite from a bitmap. Applies an inital transformation, e.g. to transform the bitmap from
        /// its original resolution to the in-world size of the object it represents.
        /// </summary>
        /// <remarks>
        /// No other transformation is applied before the specified initial transformation.
        /// </remarks>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, Matrix3x3 initialTransformation);

        /// <summary>
        /// Unloads all sprites. Unloaded sprites can no longer be drawn.
        /// </summary>
        void Clear();
    }
}

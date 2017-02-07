//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
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
        /// Loads a new sprite from a bitmap. Specifies the position in the bitmap which marks its
        /// origin (before scaling). Also specifies an initial orientation and scale.
        /// </summary>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, Geometry.Elements.Point positionOrigin, double initialOrientation, double initialScale);

        /// <summary>
        /// Loads a new sprite from a bitmap. Applies the specified initial transformation.
        /// </summary>
        ISprite LoadFromDrawingBitmap(Bitmap bitmap, Matrix3x2 initialTransformation);

        /// <summary>
        /// Unloads all sprites. Unloaded sprites can no longer be drawn.
        /// </summary>
        void Clear();
    }
}

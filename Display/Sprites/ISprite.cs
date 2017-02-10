//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.Sprites
{
    using Geometry.Transformation;

    /// <summary>
    /// Represents a single sprite, which can be drawn to the screen via the <see cref="IDrawingContext"/>.
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// Draws the sprite.
        /// </summary>
        void Draw();

        /// <summary>
        /// Draws the sprite, applying the specified transformation.
        /// </summary>
        void Draw(Matrix3x3 transformation);
    }
}

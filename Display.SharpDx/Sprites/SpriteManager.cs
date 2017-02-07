//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Sprites
{
    using Base.RuntimeChecks;
    using SharpDX.Direct2D1;
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Geometry.Elements;
    using System.Drawing;

    /// <summary>
    /// See <see cref="ISpriteManager"/>.
    /// </summary>
    public class SpriteManager : ISpriteManager, IDisposable
    {
        private readonly IBitmapLoader _bitmapLoader;
        private readonly RenderTarget _renderTarget;
        private readonly double _renderTargetHeight;
        private readonly IList<Sprite> _sprites = new List<Sprite>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteManager"/> class.
        /// </summary>
        public SpriteManager(
            IBitmapLoader bitmapLoader,
            RenderTarget renderTarget,
            double renderTargetHeight)
        {
            Checks.AssertNotNull(bitmapLoader, nameof(bitmapLoader));
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));
            Checks.AssertIsStrictPositive(renderTargetHeight, nameof(renderTargetHeight));

            this._bitmapLoader = bitmapLoader;
            this._renderTarget = renderTarget;
            this._renderTargetHeight = renderTargetHeight;
        }

        /// <summary>
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));

            return this.LoadFromDrawingBitmap(bitmap, Matrix3x2.Identity);
        }

        /// <summary>
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap, Geometry.Elements.Point, double, double)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, global::Geometry.Elements.Point positionOrigin, double initialOrientation, double initialScale)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));
            
            var originOffsetTranslation = Matrix3x2.CreateTranslation(
                -(float)positionOrigin.X,
                (float)positionOrigin.Y);

            var initialOrientationTransformation = Matrix3x2.CreateRotation((float)initialOrientation);

            var initialScaleTransformation = Matrix3x2.CreateScale((float)initialScale);

            return this.LoadFromDrawingBitmap(
                bitmap,
                originOffsetTranslation * initialOrientationTransformation * initialScaleTransformation);
        }

        /// <summary>
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap, Matrix3x2)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, Matrix3x2 initialTransformation)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));

            var sharpDxBitmap = this._bitmapLoader.LoadFromDrawingBitmap(bitmap, this._renderTarget);
            var sprite = new Sprite(sharpDxBitmap, initialTransformation, this._renderTarget, this._renderTargetHeight);

            this._sprites.Add(sprite);

            return sprite;
        }

        /// <summary>
        /// See <see cref="ISpriteManager.Clear"/>.
        /// </summary>
        public void Clear()
        {
            foreach (var sprite in this._sprites)
            {
                sprite.Dispose();
            }

            this._sprites.Clear();
        }

        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Clear();
            }
        }
    }
}

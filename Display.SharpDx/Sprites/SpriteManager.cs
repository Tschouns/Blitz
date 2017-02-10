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
    using Geometry.Extensions;
    using Geometry.Transformation;
    using global::Display.Sprites;
    using System.Drawing;
    using Vector2 = Geometry.Elements.Vector2;

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
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap, Transformation)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));

            var initialTransformation = GetTranslationCenterToOrigin(bitmap);

            return this.LoadFromDrawingBitmap(bitmap, initialTransformation);
        }

        /// <summary>
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap, double)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, double initialScale)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));

            var scale = Matrix3x3.CreateScale(initialScale);
            var initialTransformation = scale * GetTranslationCenterToOrigin(bitmap);

            return this.LoadFromDrawingBitmap(bitmap, initialTransformation);
        }

        /// <summary>
        /// See <see cref="ISpriteManager.LoadFromDrawingBitmap(System.Drawing.Bitmap, Matrix3x3)"/>.
        /// </summary>
        public ISprite LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, Matrix3x3 initialTransformation)
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

        private static Matrix3x3 GetTranslationCenterToOrigin(System.Drawing.Bitmap bitmap)
        {
            var originOffset = new Vector2(bitmap.Size.Width / 2, bitmap.Size.Height / 2);
            var translationCenterToOrigin = Matrix3x3.CreateTranslation(originOffset.Invert());

            return translationCenterToOrigin;
        }
    }
}

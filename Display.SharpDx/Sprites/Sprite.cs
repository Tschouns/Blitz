//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Sprites
{
    using System;
    using System.Numerics;
    using SharpDX.Direct2D1;
    using Base.RuntimeChecks;
    using Extensions;
    using Geometry.Transformation;

    /// <summary>
    /// See <see cref="ISprite"/>.
    /// </summary>
    public class Sprite : ISprite, IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly RenderTarget _renderTarget;
        private readonly double _renderTargetHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        public Sprite(
             Bitmap bitmap,
             RenderTarget renderTarget,
             double renderTargetHeight)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));
            Checks.AssertIsStrictPositive(renderTargetHeight, nameof(renderTargetHeight));

            this._bitmap = bitmap;
            this._renderTarget = renderTarget;
            this._renderTargetHeight = renderTargetHeight;
        }

        /// <summary>
        /// See <see cref="ISprite.Draw"/>.
        /// </summary>
        public void Draw()
        {
            this.Draw(Matrix3x2.Identity);
        }

        /// <summary>
        /// See <see cref="ISprite.Draw(Matrix3x2)"/>.
        /// </summary>
        public void Draw(Matrix3x2 transformation)
        {
            if (this._bitmap.IsDisposed)
            {
                throw new InvalidOperationException("The sprite is already disposed!");
            }

            var backupTransformation = this._renderTarget.Transform;

            // Prepare transformation.
            // TODO implement.....

            // Draw.
            ////this._renderTarget.Transform = totalTransformationMatrix.ToSharpDxRawMatric3x2();
            this._renderTarget.DrawBitmap(this._bitmap, 1.0f, BitmapInterpolationMode.NearestNeighbor);

            // Restore old transformation.
            this._renderTarget.Transform = backupTransformation;
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
                this._bitmap.Dispose();
            }
        }
    }
}

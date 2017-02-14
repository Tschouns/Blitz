//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Sprites
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using SharpDX.Direct2D1;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Transformation;
    using Extensions;
    using SharpDX.Mathematics.Interop;
    using global::Display.Sprites;
    using Point = global::Geometry.Elements.Point;
    using Rectangle = global::Geometry.Elements.Rectangle;
    using Bitmap = global::SharpDX.Direct2D1.Bitmap;

    /// <summary>
    /// See <see cref="ISprite"/>.
    /// </summary>
    public class Sprite : ISprite, IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly Rectangle _rectangle;
        private readonly Matrix3x3 _initialTransformation;
        private readonly RenderTarget _renderTarget;
        private readonly float _renderTargetHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        public Sprite(
             Bitmap bitmap,
             Matrix3x3 initialTransformation,
             RenderTarget renderTarget,
             double renderTargetHeight)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));
            Checks.AssertIsStrictPositive(renderTargetHeight, nameof(renderTargetHeight));

            this._bitmap = bitmap;
            this._initialTransformation = initialTransformation;
            this._renderTarget = renderTarget;
            this._renderTargetHeight = (float)renderTargetHeight;

            // Flip the Y-component...
            this._rectangle = new Rectangle(
                new Point(0, -bitmap.Size.Height),
                bitmap.Size.Width,
                bitmap.Size.Height);
        }

        /// <summary>
        /// See <see cref="ISprite.Draw"/>.
        /// </summary>
        public void Draw()
        {
            this.Draw(Matrix3x3.Identity);
        }

        /// <summary>
        /// See <see cref="ISprite.Draw(Matrix3x3)"/>.
        /// </summary>
        public void Draw(Matrix3x3 transformation)
        {
            if (this._bitmap.IsDisposed)
            {
                throw new InvalidOperationException("The sprite is already disposed!");
            }

            var backupTransformation = this._renderTarget.Transform;

            // Set transformation.
            this._renderTarget.Transform = PrepareTransformationMatrix(transformation);

            // Draw.
            this._renderTarget.DrawBitmap(this._bitmap, 1.0f, BitmapInterpolationMode.NearestNeighbor);

            // Restore old transformation.
            this._renderTarget.Transform = backupTransformation;
        }

        public void DrawRectangle(Matrix3x3 transformation)
        {
            var finalTransformation = transformation * this._initialTransformation;

            var a = TransformationUtils.TransformPoint(this._rectangle.A, finalTransformation).ToSharpDxVector2Flipped(this._renderTargetHeight);
            var b = TransformationUtils.TransformPoint(this._rectangle.B, finalTransformation).ToSharpDxVector2Flipped(this._renderTargetHeight);
            var c = TransformationUtils.TransformPoint(this._rectangle.C, finalTransformation).ToSharpDxVector2Flipped(this._renderTargetHeight);
            var d = TransformationUtils.TransformPoint(this._rectangle.D, finalTransformation).ToSharpDxVector2Flipped(this._renderTargetHeight);

            using (var brush = new SolidColorBrush(this._renderTarget, Color.CornflowerBlue.ToSharpDxColor()))
            {
                var strokeWidth = 1;

                this._renderTarget.DrawLine(a, b, brush, strokeWidth);
                this._renderTarget.DrawLine(b, c, brush, strokeWidth);
                this._renderTarget.DrawLine(c, d, brush, strokeWidth);
                this._renderTarget.DrawLine(d, a, brush, strokeWidth);
            }
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

        private RawMatrix3x2 PrepareTransformationMatrix(Matrix3x3 transformation)
        {
            // Apply on top of the "initial transformation".
            var finalTransformation = transformation * this._initialTransformation;
            var finalTransformation3x2 = TransformationUtils.GetCartesianTransformationMatrix(finalTransformation);

            // Flip positions on the Y-axis.
            finalTransformation3x2.M32 = this._renderTargetHeight - finalTransformation3x2.M32;

            return finalTransformation3x2.ToSharpDxRawMatric3x2();
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System.Numerics;
    using Geometry.Elements;
    using Geometry.Transformation;

    /// <summary>
    /// See <see cref="ICameraTransformation"/>.
    /// </summary>
    public class CameraTransformation : ICameraTransformation
    {
        private readonly ITransformation _transformation;

        /// <summary>
        /// Used to do the actual transformations from world to viewport coordinates.
        /// </summary>
        private readonly Matrix3x2 _worldToViewportTransformationMatrix;

        /// <summary>
        /// Used to do the actual transformations from viewport to world coordinates.
        /// </summary>
        private readonly Matrix3x2 _viewportToWorldTransformationMatrix;

        /// <summary>
        /// Used to simply scale distances form world to viewport.
        /// </summary>
        private readonly double _worldToViewportScale;

        /// <summary>
        /// Used to simply scale distances form viewport to world.
        /// </summary>
        private readonly double _viewportToWorldScale;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraTransformation"/> class.
        /// </summary>
        public CameraTransformation(
            ITransformation transformation,
            double worldToViewportScale)
        {
            this._transformation = transformation;

            this._worldToViewportTransformationMatrix = this._transformation.ApplyToPrevious(Matrix3x2.Identity);
            this._viewportToWorldTransformationMatrix = -this._worldToViewportTransformationMatrix;

            this._worldToViewportScale = worldToViewportScale;
            this._viewportToWorldScale = worldToViewportScale == 0 ? 0 : 1 / worldToViewportScale;
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewport(Point)"/>.
        /// </summary>
        public Point WorldToViewport(Point worldPosition)
        {
            return TransformPosition(worldPosition, this._worldToViewportTransformationMatrix);
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewport(double)"/>.
        /// </summary>
        public double WorldToViewport(double worldDistance)
        {
            return worldDistance * this._worldToViewportScale;
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.ViewportToWorld(Point)"/>.
        /// </summary>
        public Point ViewportToWorld(Point viewportPosition)
        {
            return TransformPosition(viewportPosition, this._viewportToWorldTransformationMatrix);
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.ViewportToWorld(double)"/>.
        /// </summary>
        public double ViewportToWorld(double viewportDistance)
        {
            return viewportDistance * this._viewportToWorldScale;
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewportMatrix3x2"/>.
        /// </summary>
        public Matrix3x2 WorldToViewportMatrix3x2()
        {
            return this._worldToViewportTransformationMatrix;
        }

        /// <summary>
        /// Does the actual transformation of a specified position, appying the specified transformation
        /// matrix.
        /// </summary>
        private static Point TransformPosition(Point position, Matrix3x2 transformationMatrix)
        {
            var transformedPosition = new Point(
                (transformationMatrix.M11 * position.X) + (transformationMatrix.M12 * position.Y),
                (transformationMatrix.M21 * position.X) + (transformationMatrix.M22 * position.Y));

            transformedPosition.X += transformationMatrix.M31;
            transformedPosition.Y += transformationMatrix.M32;

            return transformedPosition;
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using System.Numerics;
    using Geometry.Elements;

    /// <summary>
    /// See <see cref="ICameraTransformation"/>.
    /// </summary>
    public class CameraTransformation : ICameraTransformation
    {
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
            Matrix3x2 worldToViewportTransformationMatrix,
            double worldToViewportScale)
        {
            this._worldToViewportTransformationMatrix = worldToViewportTransformationMatrix;
            this._viewportToWorldTransformationMatrix = -worldToViewportTransformationMatrix;

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
        /// Does the actual transformation of a specified position, appying the specified transformation
        /// matrix.
        /// </summary>
        private static Point TransformPosition(Point position, Matrix3x2 transformationMatrix)
        {
            var transformedPosition = new Point(
                (transformationMatrix.M11 * position.X) + (transformationMatrix.M12 * position.Y),
                (transformationMatrix.M21 * position.X) + (transformationMatrix.M22 * position.Y));

            transformedPosition = new Point(
                transformedPosition.X + transformationMatrix.M31,
                transformedPosition.Y + transformationMatrix.M32);

            return transformedPosition;
        }
    }
}

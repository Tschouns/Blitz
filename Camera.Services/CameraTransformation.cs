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
        private Matrix3x2 _worldToViewportTransformationMatrix;

        /// <summary>
        /// Used to do the actual transformations from viewport to world coordinates.
        /// </summary>
        private Matrix3x2 _viewportToWorldTransformationMatrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraTransformation"/> class.
        /// </summary>
        public CameraTransformation(Matrix3x2 worldToViewportTransformationMatrix)
        {
            this._worldToViewportTransformationMatrix = worldToViewportTransformationMatrix;
            this._viewportToWorldTransformationMatrix = -worldToViewportTransformationMatrix;
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewport"/>.
        /// </summary>
        public Point WorldToViewport(Point worldPosition)
        {
            return TransformPosition(worldPosition, this._worldToViewportTransformationMatrix);
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.ViewportToWorld"/>.
        /// </summary>
        public Point ViewportToWorld(Point viewportPosition)
        {
            return TransformPosition(viewportPosition, this._viewportToWorldTransformationMatrix);
        }

        /// <summary>
        /// Does the actual transformation of a specified position, appying the specified transformation
        /// matrix.
        /// </summary>
        private static Point TransformPosition(Point position, Matrix3x2 transformationMatrix)
        {
            return new Point(
                (transformationMatrix.M11 * position.X) + (transformationMatrix.M12 * position.Y),
                (transformationMatrix.M21 * position.X) + (transformationMatrix.M22 * position.Y));
        }
    }
}

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
        /// <summary>
        /// Used to do the actual transformations from world to viewport coordinates.
        /// </summary>
        private readonly Matrix3x3 _worldToViewportTransformationMatrix;

        /// <summary>
        /// Used to do the actual transformations from viewport to world coordinates.
        /// </summary>
        private readonly Matrix3x3 _viewportToWorldTransformationMatrix;

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
            Matrix3x3 worldToViewportTransformationMatrix,
            double worldToViewportScale)
        {
            this._worldToViewportTransformationMatrix = worldToViewportTransformationMatrix;

            //TODO: Add unary negation!!!!
            this._viewportToWorldTransformationMatrix = this._worldToViewportTransformationMatrix;

            this._worldToViewportScale = worldToViewportScale;
            this._viewportToWorldScale = worldToViewportScale == 0 ? 0 : 1 / worldToViewportScale;
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewportMatrix3x3"/>.
        /// </summary>
        public Matrix3x3 WorldToViewportMatrix3x3 => this._worldToViewportTransformationMatrix;

        /// <summary>
        /// See <see cref="ICameraTransformation.WorldToViewport(Point)"/>.
        /// </summary>
        public Point WorldToViewport(Point worldPosition)
        {
            return TransformationUtils.TransformPoint(worldPosition, this._worldToViewportTransformationMatrix);
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
            return TransformationUtils.TransformPoint(viewportPosition, this._viewportToWorldTransformationMatrix);
        }

        /// <summary>
        /// See <see cref="ICameraTransformation.ViewportToWorld(double)"/>.
        /// </summary>
        public double ViewportToWorld(double viewportDistance)
        {
            return viewportDistance * this._viewportToWorldScale;
        }
    }
}

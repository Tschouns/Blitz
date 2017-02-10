//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using Base.RuntimeChecks;
    using Geometry;
    using Geometry.Elements;
    using Geometry.Extensions;
    using Geometry.Transformation;

    /// <summary>
    /// See <see cref="ICamera"/>.
    /// </summary>
    public class Camera : ICamera
    {
        private readonly ITransformationFactory _transformationFactory;

        /// <summary>
        /// The width of the viewport.
        /// </summary>
        private readonly double _viewportWidth;

        /// <summary>
        /// The height of the viewport.
        /// </summary>
        private readonly double _viewportHeight;

        /// <summary>
        /// Represents the viewport center, in viewport coordinates.
        /// </summary>
        private readonly Point _viewportCenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera(
            ITransformationFactory transformationFactory,
            int viewportWidth,
            int viewportHeight)
        {
            Checks.AssertNotNull(transformationFactory, nameof(transformationFactory));
            Checks.AssertIsStrictPositive(viewportWidth, nameof(viewportWidth));
            Checks.AssertIsStrictPositive(viewportHeight, nameof(viewportHeight));

            this._transformationFactory = transformationFactory;

            this._viewportWidth = viewportWidth;
            this._viewportHeight = viewportHeight;

            this._viewportCenter = new Point(
                this._viewportWidth / 2,
                this._viewportHeight / 2);

            this.State = new CameraState()
            {
                Position = GeometryConstants.Origin,
                Orientation = 0.0,
                Scale = 1.0
            };
        }

        /// <summary>
        /// See <see cref="ICamera.State"/>.
        /// </summary>
        public CameraState State { get; set; }

        /// <summary>
        /// See <see cref="ICamera.GetCameraTransformation"/>.
        /// </summary>
        public ICameraTransformation GetCameraTransformation()
        {
            // Translate to in-world position, then scale, then translate to align with screen center, then rotate about the screen center.
            var transformationMatrix =
                Matrix3x3.CreateRotation(-this.State.Orientation, this._viewportCenter) *
                Matrix3x3.CreateTranslation(this._viewportCenter.AsVector()) *
                Matrix3x3.CreateTranslation(this.State.Position.AsVector().Invert()) *
                Matrix3x3.CreateScale(this.State.Scale, this.State.Position);

            return new CameraTransformation(
                transformationMatrix,
                this.State.Scale);
        }

        /// <summary>
        /// See <see cref="ICamera.IsInView"/>.
        /// </summary>
        public bool IsInView(Point point)
        {
            throw new NotImplementedException();
        }
    }
}

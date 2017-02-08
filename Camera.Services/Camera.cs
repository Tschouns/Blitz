﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using System;
    using System.Numerics;
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
        public Camera(int viewportWidth, int viewportHeight)
        {
            Checks.AssertIsStrictPositive(viewportWidth, nameof(viewportWidth));
            Checks.AssertIsStrictPositive(viewportHeight, nameof(viewportHeight));

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
            // TODO: Get rid of these f*****g casts.
            var origin = new System.Numerics.Vector2((float)this._viewportCenter.X, (float)this._viewportCenter.Y) / (float)this.State.Scale;

            var worldToViewportTransformationMatrix =
                Matrix3x2.Identity *
                Matrix3x2.CreateTranslation(new System.Numerics.Vector2((float)-this.State.Position.X, (float)-this.State.Position.Y)) *
                Matrix3x2.CreateRotation((float)this.State.Orientation) *
                Matrix3x2.CreateTranslation(origin) *
                Matrix3x2.CreateScale((float)this.State.Scale, (float)this.State.Scale);

            return new CameraTransformation(
                worldToViewportTransformationMatrix,
                ////new Transformation(this.State.Orientation, this.State.Scale, this.State.Position.AsVector().Invert().Multiply(this.State.Scale)),
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

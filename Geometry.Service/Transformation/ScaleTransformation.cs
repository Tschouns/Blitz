//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Transformation
{
    using System.Numerics;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Transformation;

    /// <summary>
    /// See <see cref="ITransformation"/>. Implements a scale transformation.
    /// </summary>
    public class ScaleTransformation : ITransformation
    {
        private readonly ITransformation _previousTransformation;
        private readonly Matrix3x2 _scaleMatrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTransformation"/> class.
        /// </summary>
        public ScaleTransformation(
            ITransformation previousTransformation,
            double scale,
            Point scaleOrigin)
        {
            ArgumentChecks.AssertNotNull(previousTransformation, nameof(previousTransformation));

            this._previousTransformation = previousTransformation;
            this._scaleMatrix = Matrix3x2.CreateScale(
                (float)scale,
                new System.Numerics.Vector2((float)scaleOrigin.X, (float)scaleOrigin.Y));
        }

        /// <summary>
        /// See <see cref="ITransformation.ApplyToPrevious"/>.
        /// </summary>
        public Matrix3x2 ApplyToPrevious(Matrix3x2 previousTransformationMatrix)
        {
            var newTransformationMatrix =
                this._scaleMatrix *
                this._previousTransformation.ApplyToPrevious(previousTransformationMatrix);

            return newTransformationMatrix;
        }
    }
}

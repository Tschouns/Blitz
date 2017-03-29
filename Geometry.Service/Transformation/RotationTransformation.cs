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
    /// See <see cref="ITransformation"/>. Implements a rotation transformation.
    /// </summary>
    public class RotationTransformation : ITransformation
    {
        private readonly ITransformation _previousTransformation;
        private readonly Matrix3x2 _rotationMatrix;
        ////private readonly Point _rotationOrigin;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotationTransformation"/> class.
        /// </summary>
        public RotationTransformation(
            ITransformation previousTransformation,
            double rotation,
            Point rotationOrigin)
        {
            ArgumentChecks.AssertNotNull(previousTransformation, nameof(previousTransformation));

            this._previousTransformation = previousTransformation;

            this._rotationMatrix = Matrix3x2.CreateRotation((float)rotation);
            ////this._rotationOrigin = rotationOrigin;

            ////this._rotationMatrix = Matrix3x2.CreateRotation(
            ////    (float)rotation,
            ////    new System.Numerics.Vector2(-(float)rotationOrigin.X, -(float)rotationOrigin.Y));

            ////// Desperate experiment...
            ////this._rotationMatrix.M31 = 0.0f;
            ////this._rotationMatrix.M32 = 0.0f;
        }

        /// <summary>
        /// See <see cref="ITransformation.ApplyToPrevious"/>.
        /// </summary>
        public Matrix3x2 ApplyToPrevious(Matrix3x2 previousTransformationMatrix)
        {
            // TODO: check whether this is really correct...
            var newTransformationMatrix =
                this._rotationMatrix *
                this._previousTransformation.ApplyToPrevious(previousTransformationMatrix);

            ////var newTransformationMatrix = this._previousTransformation.ApplyToPrevious(previousTransformationMatrix);

            ////// Move to align with the origin, rotate, and move back.
            ////newTransformationMatrix.M31 += -(float)this._rotationOrigin.X;
            ////newTransformationMatrix.M32 += -(float)this._rotationOrigin.Y;

            ////newTransformationMatrix = this._rotationMatrix * newTransformationMatrix;

            ////newTransformationMatrix.M31 += (float)this._rotationOrigin.X;
            ////newTransformationMatrix.M32 += (float)this._rotationOrigin.Y;

            return newTransformationMatrix;
        }
    }
}

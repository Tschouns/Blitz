//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Transformation
{
    using System.Numerics;
    using Base.RuntimeChecks;
    using Geometry.Transformation;
    using Vector2 = Geometry.Elements.Vector2;

    /// <summary>
    /// See <see cref="ITransformation"/>. Implements a translation transformation.
    /// </summary>
    public class TranslationTransformation : ITransformation
    {
        private readonly ITransformation _previousTransformation;
        private readonly Vector2 _translationVector;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationTransformation"/> class.
        /// </summary>
        public TranslationTransformation(
            ITransformation previousTransformation,
            Vector2 translationVector)
        {
            Checks.AssertNotNull(previousTransformation, nameof(previousTransformation));

            this._previousTransformation = previousTransformation;
            this._translationVector = translationVector;
        }

        /// <summary>
        /// See <see cref="ITransformation.ApplyToPrevious"/>.
        /// </summary>
        public Matrix3x2 ApplyToPrevious(Matrix3x2 previousTransformationMatrix)
        {
            var newTransformationMatrix = this._previousTransformation.ApplyToPrevious(previousTransformationMatrix);

            newTransformationMatrix.M31 += (float)this._translationVector.X;
            newTransformationMatrix.M32 += (float)this._translationVector.Y;

            return newTransformationMatrix;
        }
    }
}

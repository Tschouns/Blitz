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
    /// See <see cref="ITransformation"/>. Does nothing to the previous transformation matrix.
    /// </summary>
    public class DummyTransformation : ITransformation
    {
        /// <summary>
        /// See <see cref="ITransformation.ApplyToPrevious"/>.
        /// </summary>
        public Matrix3x2 ApplyToPrevious(Matrix3x2 previousTransformationMatrix)
        {
            // We do not change the matrix.
            return previousTransformationMatrix;
        }
    }
}

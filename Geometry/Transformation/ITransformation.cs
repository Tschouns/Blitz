//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using System.Numerics;

    /// <summary>
    /// Represents an a 2D transformation - either a scaling, rotation, translation or a combination
    /// of those.
    /// It can be applied to a matrix representing the previous transformation(s).
    /// </summary>
    public interface ITransformation
    {
        /// <summary>
        /// Applies the transformation(s) to the "previous tansformation" represented by the specified
        /// matrix..
        /// </summary>
        Matrix3x2 ApplyToPrevious(Matrix3x2 previousTransformationMatrix);
    }
}

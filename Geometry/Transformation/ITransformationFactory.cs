//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using System.Numerics;
    using Geometry.Elements;
    using Vector2 = Geometry.Elements.Vector2;

    /// <summary>
    /// Provides helper methods to create 2D transformations.
    /// </summary>
    public interface ITransformationFactory
    {
        /// <summary>
        /// Creates a scale transformation.
        /// </summary>
        ITransformation CreateScale(double scale, Point scaleOrigin);

        /// <summary>
        /// Creates a scale transformation on top of a previous transformation.
        /// </summary>
        ITransformation CreateScaleOnTopOf(double scale, Point scaleOrigin, ITransformation previousTransformation);

        /// <summary>
        /// Creates a rotation transformation.
        /// </summary>
        ITransformation CreateRotation(double rotation, Point rotationOrigin);

        /// <summary>
        /// Creates a rotation transformation on top of a previous transformation.
        /// </summary>
        ITransformation CreateRotationOnTopOf(double rotation, Point rotationOrigin, ITransformation previousTransformation);

        /// <summary>
        /// Creates a translation transformation.
        /// </summary>
        ITransformation CreateTranslation(Vector2 translationVector);

        /// <summary>
        /// Creates a translation transformation on top of a previous transformation.
        /// </summary>
        ITransformation CreateTranslationOnTopOf(Vector2 translationVector, ITransformation previousTransformation);
    }
}

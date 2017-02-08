//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services.Transformation
{
    using Geometry.Elements;
    using Geometry.Transformation;
    using Vector2 = Geometry.Elements.Vector2;

    /// <summary>
    /// See <see cref="ITransformationFactory"/>.
    /// </summary>
    public class TransformationFactory : ITransformationFactory
    {
        /// <summary>
        /// See <see cref="ITransformationFactory.CreateScale"/>.
        /// </summary>
        public ITransformation CreateScale(double scale, Point scaleOrigin)
        {
            return new ScaleTransformation(new DummyTransformation(), scale, scaleOrigin);
        }

        /// <summary>
        /// See <see cref="ITransformationFactory.CreateScaleOnTopOf"/>.
        /// </summary>
        public ITransformation CreateScaleOnTopOf(double scale, Point scaleOrigin, ITransformation previousTransformation)
        {
            return new ScaleTransformation(previousTransformation, scale, scaleOrigin);
        }

        /// <summary>
        /// See <see cref="ITransformationFactory.CreateRotation"/>.
        /// </summary>
        public ITransformation CreateRotation(double rotation, Point rotationOrigin)
        {
            return new RotationTransformation(new DummyTransformation(), rotation, rotationOrigin);
        }

        /// <summary>
        /// See <see cref="ITransformationFactory.CreateRotationOnTopOf"/>.
        /// </summary>
        public ITransformation CreateRotationOnTopOf(double rotation, Point rotationOrigin, ITransformation previousTransformation)
        {
            return new RotationTransformation(previousTransformation, rotation, rotationOrigin);
        }

        /// <summary>
        /// See <see cref="ITransformationFactory.CreateTranslation"/>.
        /// </summary>
        public ITransformation CreateTranslation(Vector2 translationVector)
        {
            return new TranslationTransformation(new DummyTransformation(), translationVector);
        }

        /// <summary>
        /// See <see cref="ITransformationFactory.CreateTranslation"/>.
        /// </summary>
        public ITransformation CreateTranslationOnTopOf(Vector2 translationVector, ITransformation previousTransformation)
        {
            return new TranslationTransformation(previousTransformation, translationVector);
        }
    }
}

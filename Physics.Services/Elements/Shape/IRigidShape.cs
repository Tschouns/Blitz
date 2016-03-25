//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Provides a method to update the "current" state of the shape, based
    /// on position and orientation of the body.
    /// </summary>
    /// <typeparam name="TGeometricFigure">
    /// See <see cref="IShape{TGeometricFigure}"/>.
    /// </typeparam>
    public interface IRigidShape<TGeometricFigure> : IShape<TGeometricFigure>
    {
        /// <summary>
        /// Calculates the inertia for this shape, given a specified mass.
        /// </summary>
        double CalculateInertia(double mass);

        /// <summary>
        /// Updates the "current" state of the shape, based on position and
        /// orientation of the body.
        /// </summary>
        void Update(Point position, double orientation);
    }
}

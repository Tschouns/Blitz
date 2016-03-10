//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Helpers
{
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to calculate properties of physical bodies.
    /// </summary>
    public interface IBodyCalculationHelper
    {
        /// <summary>
        /// Calculates the moment of inertia for a specified polygon of the specified
        /// mass, rotating about the specified axis.
        /// </summary>
        double CalculateMomentOfInertia(Polygon polygon, Point axis, double mass);

        /// <summary>
        /// Calculates the moment of inertia for a specified polygon of the specified
        /// mass, rotating about the origin.
        /// </summary>
        double CalculateMomentOfInertiaAboutOrigin(Polygon polygon, double mass);
    }
}

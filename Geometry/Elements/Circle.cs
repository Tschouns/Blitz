//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents circle, defined by center (point) and a radius.
    /// </summary>
    public class Circle : IFigure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle(Point center, double radius)
        {
            Checks.AssertNotNull(radius, nameof(radius));

            this.Center = center;
            this.Radius = radius;
        }

        /// <summary>
        /// Gets the center (position) of the circle.
        /// </summary>
        public Point Center { get; }

        /// <summary>
        /// Gets the radius of the circle.
        /// </summary>
        public double Radius { get; }
    }
}

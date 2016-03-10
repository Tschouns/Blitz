//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements.Shape;

    /// <summary>
    /// Implementation of <see cref="IPolygonShape"/>
    /// </summary>
    public class PolygonShape : IPolygonShape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonShape"/> class.
        /// </summary>
        public PolygonShape(
            double volume,
            Point centerOfMass,
            Polygon polygon)
        {
            Checks.AssertIsStrictPositive(volume, nameof(volume));
            Checks.AssertNotNull(polygon, nameof(polygon));

            this.Volume = volume;
            this.CenterOfMass = centerOfMass;
            this.Polygon = polygon;
        }

        /// <summary>
        /// Gets... <see cref="IShape.Volume"/>.
        /// </summary>
        public double Volume { get; }

        /// <summary>
        /// Gets... <see cref="IShape.CenterOfMass"/>.
        /// </summary>
        public Point CenterOfMass { get; }

        /// <summary>
        /// Gets... <see cref="IPolygonShape.Polygon"/>.
        /// </summary>
        public Polygon Polygon { get; }
    }
}

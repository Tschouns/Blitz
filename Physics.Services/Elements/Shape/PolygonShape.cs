//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements.Shape
{
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
        public PolygonShape(Polygon polygon)
        {
            Checks.AssertNotNull(polygon, nameof(polygon));

            this.Polygon = polygon;
        }

        /// <summary>
        /// Gets... <see cref="IPolygonShape.Polygon"/>.
        /// </summary>
        public Polygon Polygon { get; }
    }
}

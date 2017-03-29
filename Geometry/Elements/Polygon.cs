//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents polygon, defined by a set of points (corners). Each pair of consecutive additionalCorners
    /// define a segment. The last corner connect to the first.
    /// </summary>
    public class Polygon : IFigure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon(
            Point corner1,
            Point corner2,
            Point corner3,
            params Point[] additionalCorners)
            : this(new[] { corner1, corner2, corner3 }.Union(additionalCorners))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon(IEnumerable<Point> corners)
        {
            ArgumentChecks.AssertNotNull(corners, nameof(corners));

            if (corners.Count() < 3)
            {
                throw new ArgumentException("The polygon must have at least 3 corners.");
            }

            // The enumeration is copied, so the state of the polygon is fully encapsulated.
            this.Corners = corners.ToList();
        }

        /// <summary>
        /// Gets the corners of the polygon. Each pair of consecutive additionalCorners define a segment.
        /// The last corner connect to the first.
        /// </summary>
        public IEnumerable<Point> Corners { get; }
    }
}

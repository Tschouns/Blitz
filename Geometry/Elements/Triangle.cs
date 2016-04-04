//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    /// <summary>
    /// Represents polygon, defined by a set of three points (corners).
    /// </summary>
    public class Triangle : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        public Triangle(Point a, Point b, Point c)
            : base(a, b, c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        /// <summary>
        /// Gets corner A.
        /// </summary>
        public Point A { get; }

        /// <summary>
        /// Gets corner B.
        /// </summary>
        public Point B { get; }

        /// <summary>
        /// Gets corner C.
        /// </summary>
        public Point C { get; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    using System.Linq;
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents an axis-aligned rectangle.
    /// </summary>
    public class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(double width, double height)
            : this(new Point(0, 0), width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Point a, double width, double height)
            : base(
                  a,
                  new Point(a.X + width, a.Y),
                  new Point(a.X + width, a.Y + height),
                  new Point(a.X, a.Y + height))
        {
            Checks.AssertIsStrictPositive(width, nameof(width));
            Checks.AssertIsStrictPositive(height, nameof(height));

            this.Width = width;
            this.Height = height;

            this.A = this.Corners.ElementAt(0);
            this.B = this.Corners.ElementAt(1);
            this.C = this.Corners.ElementAt(2);
            this.D = this.Corners.ElementAt(3);
        }

        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Gets the height of the rectangle.
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Gets the first (lower-left) corner point going around the rectangle counter-clockwise.
        /// </summary>
        public Point A { get; }

        /// <summary>
        /// Gets the second (lower-right) corner point going around the rectangle counter-clockwise.
        /// </summary>
        public Point B { get; }

        /// <summary>
        /// Gets the third (upper-right) corner point going around the rectangle counter-clockwise.
        /// </summary>
        public Point C { get; }

        /// <summary>
        /// Gets the fourth (upper-left) corner point going around the rectangle counter-clockwise.
        /// </summary>
        public Point D { get; }
    }
}

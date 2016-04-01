//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents an axis-aligned rectangle.
    /// </summary>
    public class Rectangle : IFigure
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
        {
            Checks.AssertIsStrictPositive(width, nameof(width));
            Checks.AssertIsStrictPositive(height, nameof(height));

            this.Width = width;
            this.Height = height;

            this.A = a;
            
            this.B = new Point(
                a.X + width,
                a.Y);

            this.C = new Point(
                a.X + width,
                a.Y + height);

            this.D = new Point(
                a.X,
                a.Y + height);

            this.Corners = new Point[4]
                {
                    this.A,
                    this.B,
                    this.C,
                    this.D
                };
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

        /// <summary>
        /// Gets all four corner points going around rectangle.
        /// </summary>
        public Point[] Corners { get; }
    }
}

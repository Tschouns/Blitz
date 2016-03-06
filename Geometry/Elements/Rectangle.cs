//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    /// <summary>
    /// Represents a rectangle on a plane.
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Point origin, double width, double height)
        {
            this.A = origin;

            this.B = new Point(
                origin.X + width,
                origin.Y);

            this.C = new Point(
                origin.X + width,
                origin.Y + height);

            this.D = new Point(
                origin.X,
                origin.Y + height);
        }

        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public double Heigh { get; }

        /// <summary>
        /// Gets the first corner point going around the rectangle (origin).
        /// </summary>
        public Point A { get; }

        /// <summary>
        /// Gets the second corner point going around the rectangle.
        /// </summary>
        public Point B { get; }

        /// <summary>
        /// Gets the third corner point going around the rectangle.
        /// </summary>
        public Point C { get; }

        /// <summary>
        /// Gets the fourth corner point going around the rectangle.
        /// </summary>
        public Point D { get; }
    }
}

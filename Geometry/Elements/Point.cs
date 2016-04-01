//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    /// <summary>
    /// Represents a point on a plane.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// See <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Elements
{
    /// <summary>
    /// Represents a two-component vector.
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X component.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y component.
        /// </summary>
        public double Y { get; set; }
    }
}

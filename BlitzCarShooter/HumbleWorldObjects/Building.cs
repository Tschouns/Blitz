//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter.HumbleWorldObjects
{
    using System.Drawing;
    using Geometry.Elements;
    using Point = Geometry.Elements.Point;
    using Rectangle = Geometry.Elements.Rectangle;

    /// <summary>
    /// A humble little building.
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        public Building(Point position, double width, double height, Color color)
        {
            this.Polygon = new Rectangle(
                position,
                width,
                height);

            this.Color = color;
        }

        /// <summary>
        /// Gets the polygon representing the building.
        /// </summary>
        public Polygon Polygon { get; }

        /// <summary>
        /// Gets the color of the building.
        /// </summary>
        public Color Color { get; }
    }
}

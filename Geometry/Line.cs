﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry
{
    using System;

    /// <summary>
    /// Represents a line on a plane, defined by two points.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        public Line(Point point1, Point point2)
        {
            if (object.Equals(point1, point2))
            {
                throw new ArgumentException("The two specified points are identical; the line is not defined.");
            }

            this.Point1 = point1;
            this.Point2 = point2;
        }

        /// <summary>
        /// Gets the first point defining the line.
        /// </summary>
        public Point Point1 { get; }

        /// <summary>
        /// Gets the second point defining the line.
        /// </summary>
        public Point Point2 { get; }
    }
}

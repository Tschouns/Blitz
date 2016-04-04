//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers.Result
{
    /// <summary>
    /// Represents the barycentric coordinates (u, v, w) of a point with respect to a
    /// triangle, made up of points a, b and c.
    /// </summary>
    public class BarycentricCoordinates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarycentricCoordinates"/> class.
        /// </summary>
        public BarycentricCoordinates(double u, double v, double w)
        {
            this.U = u;
            this.V = v;
            this.W = w;
        }

        /// <summary>
        /// Gets the u component (i.e. the "weight" of point a).
        /// </summary>
        public double U { get; }

        /// <summary>
        /// Gets the v component (i.e. the "weight" of point b).
        /// </summary>
        public double V { get; }

        /// <summary>
        /// Gets the w component (i.e. the "weight" of point c).
        /// </summary>
        public double W { get; }
    }
}

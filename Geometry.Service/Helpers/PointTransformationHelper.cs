//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Elements;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="IPointTransformationHelper"/>.
    /// </summary>
    public class PointTransformationHelper : IPointTransformationHelper
    {
        /// <summary>
        /// See <see cref="IPointTransformationHelper.RotatePoint"/>
        /// </summary>
        public Point RotatePoint(Point origin, double angle, Point point)
        {
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);

            return this.RotatePointInternal(origin, Math.Sin(angle), Math.Cos(angle), point);
        }

        /// <summary>
        /// See <see cref="IPointTransformationHelper.RotatePoints"/>
        /// </summary>
        public IEnumerable<Point> RotatePoints(Point origin, double angle, Point[] points)
        {
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);

            var newPoints = new Point[points.Length];

            for (int i = 0; i < points.Count(); i++)
            {
                newPoints[i] = this.RotatePointInternal(origin, sinAngle, cosAngle, points[i]);
            }

            return newPoints;
        }

        /// <summary>
        /// Internal helper method: does the actual rotation, based on the specified sine and cosine of the rotation angle theta.
        /// </summary>
        private Point RotatePointInternal(Point origin, double sinTheta, double cosTheta, Point point)
        {
            double newPointX =
                (cosTheta * (point.X - origin.X)) -
                (sinTheta * (point.Y - origin.Y)) +
                origin.X;

            double newPointY =
                (sinTheta * (point.X - origin.X)) +
                (cosTheta * (point.Y - origin.Y)) +
                origin.X;

            return new Point(newPointX, newPointY);
        }
    }
}

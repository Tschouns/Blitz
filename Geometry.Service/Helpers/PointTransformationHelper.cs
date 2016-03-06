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
    using Extensions;
    using Geometry.Helpers;

    /// <summary>
    /// See <see cref="IPointTransformationHelper"/>.
    /// </summary>
    public class PointTransformationHelper : IPointTransformationHelper
    {
        /// <summary>
        /// See <see cref="IPointTransformationHelper.TranslatePoint"/>.
        /// </summary>
        public Point TranslatePoint(Vector2 offset, Point point)
        {
            return point.AddVector(offset);
        }

        /// <summary>
        /// See <see cref="IPointTransformationHelper.TranslatePoints"/>.
        /// </summary>
        public Point[] TranslatePoints(Vector2 offset, Point[] points)
        {
            var translatedPoints = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                translatedPoints[i] = points[i].AddVector(offset);
            }

            return translatedPoints;
        }

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
        public Point[] RotatePoints(Point origin, double angle, Point[] points)
        {
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);

            var rotatedPoints = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                rotatedPoints[i] = this.RotatePointInternal(origin, sinAngle, cosAngle, points[i]);
            }

            return rotatedPoints;
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
                origin.Y;

            return new Point(newPointX, newPointY);
        }
    }
}

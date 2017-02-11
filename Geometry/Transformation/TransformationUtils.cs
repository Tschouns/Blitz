//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using System.Numerics;
    using Geometry.Elements;

    /// <summary>
    /// Provides methods to transform points.
    /// </summary>
    public static class TransformationUtils
    {
        /// <summary>
        /// Transforms the specified point using the specified transformation matrix.
        /// </summary>
        public static Point TransformPoint(Point point, Matrix3x3 transform)
        {
            // We use the first column of a matrix 3x3 to store our point, in homogenous coordinates.
            var pointMatrix = Matrix3x3.Identity;

            pointMatrix.M11 = point.X;
            pointMatrix.M21 = point.Y;
            pointMatrix.M31 = 1;

            // Apply the transformation.
            var transformedPointMatrix = transform * pointMatrix;

            var x = transformedPointMatrix.M11;
            var y = transformedPointMatrix.M21;
            var w = transformedPointMatrix.M31;

            if (w == 0)
            {
                return GeometryConstants.Origin;
            }

            // Get back cartesian coordinates, by dividing by w.
            var transformedPoint = new Point(
                x / w,
                y / w);

            return transformedPoint;
        }

        /// <summary>
        /// Gets a matrix 3x2, which can be used for transformation of cartesian, from a specified
        /// 3x3 matrix representing a homogenous transformation.
        /// </summary>
        public static Matrix3x2 GetCartesianTransformationMatrix(Matrix3x3 homogenousTransformation)
        {
            var h = homogenousTransformation;
            var cartesianTransformationMatrix = new Matrix3x2(
                (float)h.M11, (float)h.M12,
                (float)h.M21, (float)h.M22,
                (float)(h.M13), (float)(h.M23));

            if (h.M33 != 1)
            {
                throw new System.Exception("Hoppla");
            }

            return cartesianTransformationMatrix;
        }
    }
}

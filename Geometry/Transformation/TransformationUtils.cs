//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using System.Numerics;
    using Geometry.Elements;
    using Extensions;
    using Vector2 = Geometry.Elements.Vector2;

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
            var transformedVector = TransformVector(point.AsVector(), transform);

            return new Point(transformedVector.X, transformedVector.Y);
        }

        /// <summary>
        /// Transforms the specified vector using the specified transformation matrix.
        /// </summary>
        public static Vector2 TransformVector(Vector2 point, Matrix3x3 transform)
        {
            // We use the first column of a matrix 3x3 to store our vector, in homogenous coordinates.
            var vectorMatrix = Matrix3x3.Identity;

            vectorMatrix.M11 = point.X;
            vectorMatrix.M21 = point.Y;
            vectorMatrix.M31 = 1;

            // Apply the transformation.
            var transformedVectorMatrix = transform * vectorMatrix;

            var x = transformedVectorMatrix.M11;
            var y = transformedVectorMatrix.M21;
            var w = transformedVectorMatrix.M31;

            if (w == 0)
            {
                return GeometryConstants.Origin.AsVector();
            }

            // Get back cartesian coordinates, by dividing by w.
            var transformedVector = new Vector2(
                x / w,
                y / w);

            return transformedVector;
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

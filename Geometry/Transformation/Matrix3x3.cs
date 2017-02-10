//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using System;
    using System.Text;
    using Base.Extensions;
    using Elements;
    using Extensions;

    /// <summary>
    /// Represents a 3x3 matrix.
    /// </summary>
    public struct Matrix3x3
    {
        /// <summary>
        /// Creates a 3x3 matrix from the specified components.
        /// </summary>
        public Matrix3x3(
            double m11,
            double m12,
            double m13,
            double m21,
            double m22,
            double m23,
            double m31,
            double m32,
            double m33)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
        }

        /// <summary>
        /// Gets the multiplicative identity matrix.
        /// </summary>
        public static Matrix3x3 Identity => new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

        /// <summary>
        /// Gets or sets the first element of the first row.
        /// </summary>
        public double M11 { get; set; }

        /// <summary>
        /// Gets or sets the second element of the first row.
        /// </summary>
        public double M12 { get; set; }

        /// <summary>
        /// Gets or sets the third element of the first row.
        /// </summary>
        public double M13 { get; set; }

        /// <summary>
        /// Gets or sets the first element of the second row.
        /// </summary>
        public double M21 { get; set; }

        /// <summary>
        /// Gets or sets the second element of the second row.
        /// </summary>
        public double M22 { get; set; }

        /// <summary>
        /// Gets or sets the third element of the second row.
        /// </summary>
        public double M23 { get; set; }

        /// <summary>
        /// Gets or sets the first element of the third row.
        /// </summary>
        public double M31 { get; set; }

        /// <summary>
        /// Gets or sets the second element of the third row.
        /// </summary>
        public double M32 { get; set; }

        /// <summary>
        /// Gets or sets the third element of the third row.
        /// </summary>
        public double M33 { get; set; }

        /// <summary>
        /// Creates a rotation matrix using the specified rotation in radians.
        /// </summary>
        public static Matrix3x3 CreateRotation(double radians)
        {
            var rotationMatrix = Identity;

            var sin = Math.Sin(radians);
            var cos = Math.Cos(radians);

            rotationMatrix.M11 = cos;
            rotationMatrix.M12 = -sin;

            rotationMatrix.M21 = sin;
            rotationMatrix.M22 = cos;

            return rotationMatrix;
        }

        /// <summary>
        /// Creates a rotation matrix using the specified rotation in radians and a center point.
        /// </summary>
        public static Matrix3x3 CreateRotation(double radians, Point centerPoint)
        {
            // Translate to align with the origin, rotate, and translate back.
            var rotationMatrix =
                CreateTranslation(centerPoint.AsVector()) *
                CreateRotation(radians) *
                CreateTranslation(centerPoint.AsVector().Invert());

            return rotationMatrix;
        }

        /// <summary>
        /// Creates a scaling matrix that scales uniformly with the specified scale.
        /// </summary>
        public static Matrix3x3 CreateScale(double scale)
        {
            return CreateScale(scale, scale);
        }

        /// <summary>
        /// Creates a scaling matrix from the specified X and Y components.
        /// </summary>
        public static Matrix3x3 CreateScale(double xScale, double yScale)
        {
            var scaleMatrix = Identity;

            scaleMatrix.M11 = xScale;
            scaleMatrix.M22 = yScale;

            return scaleMatrix;
        }

        /// <summary>
        /// Creates a scaling matrix that scales uniformly with the specified scale and the specified center point.
        /// </summary>
        public static Matrix3x3 CreateScale(double scale, Point centerPoint)
        {
            return CreateScale(scale, scale, centerPoint);
        }

        /// <summary>
        /// Creates a scaling matrix from the specified X and Y components that scales with the specified center point.
        /// </summary>
        public static Matrix3x3 CreateScale(double xScale, double yScale, Point centerPoint)
        {
            // Translate to align with the origin, scale, and translate back.
            var scaleMatrix =
                CreateTranslation(centerPoint.AsVector()) *
                CreateScale(xScale, yScale) *
                CreateTranslation(centerPoint.AsVector().Invert());

            return scaleMatrix;
        }

        /// <summary>
        /// Creates a translation matrix from the specified 2-dimensional vector.
        /// </summary>
        public static Matrix3x3 CreateTranslation(Vector2 vector)
        {
            var translationMatrix = Identity;

            translationMatrix.M13 = vector.X;
            translationMatrix.M23 = vector.Y;

            return translationMatrix;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified matrices are equal.
        /// </summary>
        public static bool operator ==(Matrix3x3 a, Matrix3x3 b)
        {
            return
                a.M11 == b.M11 &&
                a.M12 == b.M12 &&
                a.M13 == b.M13 &&
                a.M21 == b.M21 &&
                a.M22 == b.M22 &&
                a.M23 == b.M23 &&
                a.M31 == b.M31 &&
                a.M32 == b.M32 &&
                a.M33 == b.M33;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified matrices are not equal.
        /// </summary>
        public static bool operator !=(Matrix3x3 a, Matrix3x3 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Adds each element in one matrix with its corresponding element in a second matrix.
        /// </summary>
        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3(
                a.M11 + b.M11,
                a.M12 + b.M12,
                a.M13 + b.M13,
                a.M21 + b.M21,
                a.M22 + b.M22,
                a.M23 + b.M23,
                a.M31 + b.M31,
                a.M32 + b.M32,
                a.M33 + b.M33);
        }

        /// <summary>
        /// Returns the matrix that results from multiplying two matrices together.
        /// </summary>
        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            var m11 = (a.M11 * b.M11) + (a.M12 * b.M21) + (a.M13 * b.M31);
            var m12 = (a.M11 * b.M12) + (a.M12 * b.M22) + (a.M13 * b.M32);
            var m13 = (a.M11 * b.M13) + (a.M12 * b.M23) + (a.M13 * b.M33);

            var m21 = (a.M21 * b.M11) + (a.M22 * b.M21) + (a.M23 * b.M31);
            var m22 = (a.M21 * b.M12) + (a.M22 * b.M22) + (a.M23 * b.M32);
            var m23 = (a.M21 * b.M13) + (a.M22 * b.M23) + (a.M23 * b.M33);

            var m31 = (a.M31 * b.M11) + (a.M32 * b.M21) + (a.M33 * b.M31);
            var m32 = (a.M31 * b.M12) + (a.M32 * b.M22) + (a.M33 * b.M32);
            var m33 = (a.M31 * b.M13) + (a.M32 * b.M23) + (a.M33 * b.M33);

            return new Matrix3x3(
                m11, m12, m13,
                m21, m22, m23,
                m31, m32, m33);
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another 3x3 matrix are equal.
        /// </summary>
        public bool Equals(Matrix3x3 matrix3x3)
        {
            return this == matrix3x3;
        }

        /// <summary>
        /// See <see cref="object.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Matrix3x3))
            {
                return false;
            }

            return this.Equals((Matrix3x3)obj);
        }

        /// <summary>
        /// See <see cref="object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            var hashM11 = this.M11.GetHashCode().RotateLeft(1);
            var hashM12 = this.M12.GetHashCode().RotateLeft(2);
            var hashM13 = this.M13.GetHashCode().RotateLeft(3);

            var hashM21 = this.M21.GetHashCode().RotateLeft(4);
            var hashM22 = this.M22.GetHashCode().RotateLeft(5);
            var hashM23 = this.M23.GetHashCode().RotateLeft(6);

            var hashM31 = this.M31.GetHashCode().RotateLeft(7);
            var hashM32 = this.M32.GetHashCode().RotateLeft(8);
            var hashM33 = this.M33.GetHashCode().RotateLeft(9);

            return
                hashM11 ^ hashM12 ^ hashM13 ^
                hashM21 ^ hashM22 ^ hashM23 ^
                hashM31 ^ hashM32 ^ hashM33;
        }

        /// <summary>
        /// See <see cref="object.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{this.M11}, {this.M12}, {this.M13} | {this.M21}, {this.M22}, {this.M23} | {this.M31}, {this.M32}, {this.M33}";
        }
    }
}

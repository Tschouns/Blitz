//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest.Transformation
{
    using System;
    using Geometry.Elements;
    using Geometry.Transformation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for <see cref="Matrix3x3Tests"/>.
    /// </summary>
    [TestClass]
    public class Matrix3x3Tests
    {
        #region CreateRotation

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_90DegreeCounterClockwiseAboutOrigin_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(Math.PI/2, GeometryConstants.Origin);

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            Assert.AreEqual(
                new Point(-5, 5),
                transformedPosition);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_90DegreeClockwiseAboutOrigin_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(-Math.PI / 2, GeometryConstants.Origin);

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            Assert.AreEqual(
                new Point(5, -5),
                transformedPosition);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_180DegreeCounterClockwiseAboutOrigin_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(Math.PI, GeometryConstants.Origin);

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            AssertEqualWithinTolerance(
                new Point(-5, -5),
                transformedPosition);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_180DegreeClockwiseAboutOrigin_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(Math.PI, GeometryConstants.Origin);

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            AssertEqualWithinTolerance(
                new Point(-5, -5),
                transformedPosition);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_90DegreeCounterClockwiseAboutOffset_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(Math.PI / 2, new Point(4, 4));

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            Assert.AreEqual(
                new Point(3, 5),
                transformedPosition);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void Rotate_90DegreeClockwiseAboutOffset_ReturnsCorrectPosition()
        {
            // Arrange
            var rotation = Matrix3x3.CreateRotation(-Math.PI / 2, new Point(4, 4));

            // Act
            var transformedPosition = TransformationUtils.TransformPoint(new Point(5, 5), rotation);

            // Assert
            Assert.AreEqual(
                new Point(5, 3),
                transformedPosition);
        }

        #endregion

        #region CreateScale

        #endregion

        #region Helpers

        private static void AssertEqualWithinTolerance(Point expected, Point actual)
        {
            var deviationX = actual.X - expected.X;
            var deviationY = actual.Y - expected.Y;

            var tolerance = 0.0000000001;

            if (deviationX > tolerance || deviationY > tolerance)
            {
                throw new ArgumentException($"The points are not equal withing tolerance - expected: {expected}, actual: {actual}");
            }
        }

        #endregion
    }
}

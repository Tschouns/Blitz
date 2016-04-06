//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest.Helpers
{
    using Elements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Helpers;

    /// <summary>
    /// Contains unit tests for <see cref="TriangleCalculationHelper"/>.
    /// </summary>
    [TestClass]
    public class TriangleCalculationHelperTests
    {
        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private TriangleCalculationHelper _testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._testCandidate = new TriangleCalculationHelper();
        }

        #region IsPointWithinTriangle

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_SomewhereInTheMiddle_ReturnsTrue()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(4, 1),
                new Point(1, 4));

            Point point = new Point(2, 2);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_Outside_ReturnsFalse()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(4, 1),
                new Point(1, 4));

            Point point = new Point(3, 3);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_OnTheEdge_ReturnsTrue()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(4, 1),
                new Point(1, 4));

            Point point = new Point(2.5, 2.5);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_JustOutside_ReturnsFalse()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(4, 1),
                new Point(1, 4));

            Point point = new Point(2.55, 2.5);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_PointEqualesCorner_ReturnsTrue()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(7, 2.5),
                new Point(1.8, 4.3));

            Point point = new Point(7, 2.5);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_JustOutsideCorner_ReturnsFalse()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(7, 2.5),
                new Point(1.8, 4.3));

            Point point = new Point(7.1, 2.5);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_TriangleClockwisePointInside_ReturnsTrue()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(1.8, 4.3),
                new Point(7, 2.5));

            Point point = new Point(6, 2.6);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsPointWithinTriangle_TriangleClockwisePointOutside_ReturnsFalse()
        {
            // Arrange
            var trianlge = new Triangle(
                new Point(1, 1),
                new Point(1.8, 4.3),
                new Point(7, 2.5));

            Point point = new Point(6.9, 2.3);

            // Act
            var result = this._testCandidate.IsPointWithinTriangle(trianlge, point);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion  
    }
}

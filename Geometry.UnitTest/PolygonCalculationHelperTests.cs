//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest
{
    using Geometry.Elements;
    using Geometry.Service.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for <see cref="PolygonCalculationHelperTests"/>.
    /// </summary>
    [TestClass]
    public class PolygonCalculationHelperTests
    {
        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private PolygonCalculationHelper testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.testCandidate = new PolygonCalculationHelper(new LineIntersectionHelper());
        }

        #region IsNonsimplePolygon

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsNonsimplePolygon_ConvexWithNoSegmentIntersecions_ReturnsFalse()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(0, 2),
                new Point(1, 3),
                new Point(2, 3)
            };

            // Act
            var result = this.testCandidate.IsNonsimplePolygon(new Polygon(corners));

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsNonsimplePolygon_ConcaveWithNoSegmentIntersecions_ReturnsFalse()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(1, 4),
                new Point(2, 2),
                new Point(5, 5),
                new Point(3, 1),
            };

            // Act
            var result = this.testCandidate.IsNonsimplePolygon(new Polygon(corners));

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsNonsimplePolygon_HasOneSegmentIntersecion_ReturnsTrue()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(4, 1),
                new Point(1, 4),
                new Point(4, 4)
            };

            // Act
            var result = this.testCandidate.IsNonsimplePolygon(new Polygon(corners));

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void IsNonsimplePolygon_HasTwoSegmentIntersecions_ReturnsTrue()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(7, 1),
                new Point(6, 2),
                new Point(5, 0),
                new Point(4, 2)
            };

            // Act
            var result = this.testCandidate.IsNonsimplePolygon(new Polygon(corners));

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region CalculateArea

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CalculateArea_TriangleCounterClockwise_ReturnsCorrectArea()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(7, 1),
                new Point(3, 5)
            };

            // Act
            var result = this.testCandidate.CalculateArea(new Polygon(corners));

            // Assert
            Assert.AreEqual(12.0, result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CalculateArea_RectangleCounterClockwise_ReturnsCorrectArea()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(7, 1),
                new Point(7, 5),
                new Point(1, 5)
            };

            // Act
            var result = this.testCandidate.CalculateArea(new Polygon(corners));

            // Assert
            Assert.AreEqual(24.0, result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CalculateArea_ParallelogramCounterClockwise_ReturnsCorrectArea()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(7, 1),
                new Point(12, 5),
                new Point(6, 5)
            };

            // Act
            var result = this.testCandidate.CalculateArea(new Polygon(corners));

            // Assert
            Assert.AreEqual(24.0, result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CalculateArea_HouseShapedPentagonClockwise_ReturnsCorrectArea()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(1, 5),
                new Point(4, 7),
                new Point(7, 5),
                new Point(7, 1)
            };

            // Act
            var result = this.testCandidate.CalculateArea(new Polygon(corners));

            // Assert
            Assert.AreEqual(30.0, result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CalculateArea_ConvextOctagonClockwise_ReturnsCorrectArea()
        {
            // Arrange
            Point[] corners =
            {
                new Point(1, 1),
                new Point(0, 7),
                new Point(2, 7),
                new Point(4, 4),
                new Point(6, 7),
                new Point(7, 7),
                new Point(8, 1),
                new Point(2, 3)
            };

            // Act
            var result = this.testCandidate.CalculateArea(new Polygon(corners));

            // Assert
            Assert.AreEqual(29.0, result);
        }

        #endregion

        #region DetermineCentroid

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void DetermineCentroid_SquareCounterClockwise_ReturnsCorrectCentroid()
        {
            // Arrange
            Point[] corners =
            {
                new Point(0, 0),
                new Point(4, 0),
                new Point(4, 4),
                new Point(0, 4),
            };

            // Act
            var result = this.testCandidate.DetermineCentroid(new Polygon(corners));

            // Assert
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(2, result.Y);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void DetermineCentroid_SquareClockwise_ReturnsCorrectCentroid()
        {
            // Arrange
            Point[] corners =
            {
                new Point(0, 0),
                new Point(0, 4),
                new Point(4, 4),
                new Point(4, 0),
            };

            // Act
            var result = this.testCandidate.DetermineCentroid(new Polygon(corners));

            // Assert
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(2, result.Y);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void DetermineCentroid_SquareHalfInTheBeyondTheOriginCounterClockwise_ReturnsCorrectCentroid()
        {
            // Arrange
            Point[] corners =
            {
                new Point(-5, 0),
                new Point(1, 0),
                new Point(1, 6),
                new Point(-5, 6),
            };

            // Act
            var result = this.testCandidate.DetermineCentroid(new Polygon(corners));

            // Assert
            Assert.AreEqual(-2, result.X);
            Assert.AreEqual(3, result.Y);
        }

        #endregion
    }
}

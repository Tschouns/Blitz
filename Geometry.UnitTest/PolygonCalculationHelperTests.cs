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
    }
}

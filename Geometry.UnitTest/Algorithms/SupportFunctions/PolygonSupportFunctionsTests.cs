//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest.Algorithms.SupportFunctions
{
    using Geometry.Elements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Algorithms.SupportFunctions;
    using Services.Helpers;

    /// <summary>
    /// Contains unit tests for <see cref="PolygonSupportFunctions"/>.
    /// </summary>
    [TestClass]
    public class PolygonSupportFunctionsTests
    {
        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private PolygonSupportFunctions testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var lineCalculationHelper = new LineCalculationHelper();
            this.testCandidate = new PolygonSupportFunctions(lineCalculationHelper);
        }

        #region GetSupportPoint

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPointForFourVertices_DirectionXIsPositiveYIsPositive_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(0, 2),
                new Point(1, 3),
                new Point(2, 3)
            });

            var direction = new Vector2(1, 1);

            // Act
            var resultSupportPoint = this.testCandidate.GetSupportPoint(polygon, direction);

            // Assert
            var expectedSupportPoint = new Point(2, 3);

            Assert.AreEqual(
                expectedSupportPoint,
                resultSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPointForFiveVertices_DirectionXIsPositiveYIsPositive_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(0, 2),
                new Point(1, 3),
                new Point(2, 3),
                new Point(4, 2)
            });

            var direction = new Vector2(1, 1);

            // Act
            var resultSupportPoint = this.testCandidate.GetSupportPoint(polygon, direction);

            // Assert
            var expectedSupportPoint = new Point(4, 2);

            Assert.AreEqual(
                expectedSupportPoint,
                resultSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPointForFiveVertices_DirectionXIsPositiveYIsNegative_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(0, 2),
                new Point(1, 3),
                new Point(2, 3),
                new Point(4, 2)
            });

            var direction = new Vector2(1, -1);

            // Act
            var resultSupportPoint = this.testCandidate.GetSupportPoint(polygon, direction);

            // Assert
            var expectedSupportPoint = new Point(4, 2);

            Assert.AreEqual(
                expectedSupportPoint,
                resultSupportPoint);
        }

        #endregion

        #region GetFigureOutlinePointClosestToPosition

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetFigureOutlinePointClosestToPosition_CornerIsClosest_ReturnsClosestCorner()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 2),
                new Point(2, 1)
            });

            var position = new Point(0, 0);

            // Act
            var closestPoint = this.testCandidate.GetFigureOutlinePointClosestToPosition(polygon, position);

            // Assert
            var expectedClosestPoint = new Point(1, 1);

            Assert.AreEqual(
                expectedClosestPoint,
                closestPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetFigureOutlinePointClosestToPosition_CornerIsJustClosest_ReturnsClosestCorner()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 2),
                new Point(2, 1)
            });

            var position = new Point(0, 1);

            // Act
            var closestPoint = this.testCandidate.GetFigureOutlinePointClosestToPosition(polygon, position);

            // Assert
            var expectedClosestPoint = new Point(1, 1);

            Assert.AreEqual(
                expectedClosestPoint,
                closestPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetFigureOutlinePointClosestToPosition_PositionIsBetweenTwoClosestCorners_ReturnsClosestPointOnLineSegment()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(1, 3),
                new Point(2, 3),
                new Point(2, 1)
            });

            var position = new Point(0, 2);

            // Act
            var closestPoint = this.testCandidate.GetFigureOutlinePointClosestToPosition(polygon, position);

            // Assert
            var expectedClosestPoint = new Point(1, 2);

            Assert.AreEqual(
                expectedClosestPoint,
                closestPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetFigureOutlinePointClosestToPosition_PositionIsJustBetweenTwoClosestCorners_ReturnsClosestPointOnLineSegment()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(1, 3),
                new Point(2, 3),
                new Point(2, 1)
            });

            var position = new Point(0, 1.5);

            // Act
            var closestPoint = this.testCandidate.GetFigureOutlinePointClosestToPosition(polygon, position);

            // Assert
            var expectedClosestPoint = new Point(1, 1.5);

            Assert.AreEqual(
                expectedClosestPoint,
                closestPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetFigureOutlinePointClosestToPosition_PositionIsWithinPolygon_ReturnsPosition()
        {
            // Arrange
            var polygon = new Polygon(new[]
            {
                new Point(1, 1),
                new Point(1, 3),
                new Point(2, 3),
                new Point(2, 1)
            });

            var position = new Point(1.2, 2);

            // Act
            var closestPoint = this.testCandidate.GetFigureOutlinePointClosestToPosition(polygon, position);

            // Assert
            var expectedClosestPoint = new Point(1, 2);

            Assert.AreEqual(
                expectedClosestPoint,
                closestPoint);
        }

        #endregion
    }
}

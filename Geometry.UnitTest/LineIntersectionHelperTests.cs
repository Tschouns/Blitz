//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest
{
    using Elements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Helpers;

    /// <summary>
    /// Contains unit tests for <see cref="LineIntersectionHelper"/>.
    /// </summary>
    [TestClass]
    public class LineIntersectionHelperTests
    {
        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private LineIntersectionHelper testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.testCandidate = new LineIntersectionHelper();
        }

        #region AreLinesParallel

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void AreLinesParallel_LinesAreParallel_ReturnsTrue()
        {
            // Arrange
            var pointA1 = new Point(2, 2);
            var pointA2 = new Point(5, 6);
            var pointB1 = new Point(2, 3);
            var pointB2 = new Point(5, 7);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var result = this.testCandidate.AreLinesParallel(lineA, lineB);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void AreLinesParallel_LinesAreBothHorizontal_ReturnsTrue()
        {
            // Arrange
            var pointA1 = new Point(1, 2);
            var pointA2 = new Point(2, 2);
            var pointB1 = new Point(3, 1);
            var pointB2 = new Point(4, 1);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var result = this.testCandidate.AreLinesParallel(lineA, lineB);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void AreLinesParallel_LinesAreIdentical_ReturnsTrue()
        {
            // Arrange
            var pointA1 = new Point(0, 0);
            var pointA2 = new Point(1, 1);
            var pointB1 = new Point(2, 2);
            var pointB2 = new Point(3, 3);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var result = this.testCandidate.AreLinesParallel(lineA, lineB);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void AreLinesParallel_LineSegmentsIntersect_ReturnsFalse()
        {
            // Arrange
            var pointA1 = new Point(0, 0);
            var pointA2 = new Point(1, 1);
            var pointB1 = new Point(0, 1);
            var pointB2 = new Point(1, 0);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var result = this.testCandidate.AreLinesParallel(lineA, lineB);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region GetLineIntersection

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineIntersection_LinesAreParallel_ReturnsNull()
        {
            // Arrange
            var pointA1 = new Point(2, 2);
            var pointA2 = new Point(5, 6);
            var pointB1 = new Point(2, 3);
            var pointB2 = new Point(5, 7);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineIntersection(lineA, lineB);

            // Assert
            Assert.IsNull(intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineIntersection_LineSegmentsIntersect_ReturnsIntersectionPoint()
        {
            // Arrange
            var pointA1 = new Point(0, 0);
            var pointA2 = new Point(1, 1);
            var pointB1 = new Point(0, 1);
            var pointB2 = new Point(1, 0);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineIntersection(lineA, lineB);

            // Assert
            Assert.IsNotNull(intersectionPoint);
            Assert.AreEqual(new Point(0.5, 0.5), intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineIntersection_LinesIntersectOutsideSegments_ReturnsIntersectionPoint()
        {
            // Arrange
            var pointA1 = new Point(1, 1);
            var pointA2 = new Point(3, 3);
            var pointB1 = new Point(5, 3);
            var pointB2 = new Point(7, 1);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineIntersection(lineA, lineB);

            // Assert
            Assert.IsNotNull(intersectionPoint);
            Assert.AreEqual(new Point(4, 4), intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineIntersection_LinesIntersectWithinOneSegment_ReturnsInterseciontPoint()
        {
            // Arrange
            var pointA1 = new Point(1, 1);
            var pointA2 = new Point(3, 3);
            var pointB1 = new Point(3, 1);
            var pointB2 = new Point(4, 0);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineIntersection(lineA, lineB);

            // Assert
            Assert.IsNotNull(intersectionPoint);
            Assert.AreEqual(new Point(2, 2), intersectionPoint);
        }

        #endregion

        #region GetLineSegmentIntersection

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineSegmentIntersection_LinesAreParallel_ReturnsNull()
        {
            // Arrange
            var pointA1 = new Point(2, 2);
            var pointA2 = new Point(5, 6);
            var pointB1 = new Point(2, 3);
            var pointB2 = new Point(5, 7);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineSegmentIntersection(lineA, lineB);

            // Assert
            Assert.IsNull(intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineSegmentIntersection_LineSegmentsIntersect_ReturnsIntersectionPoint()
        {
            // Arrange
            var pointA1 = new Point(0, 0);
            var pointA2 = new Point(1, 1);
            var pointB1 = new Point(0, 1);
            var pointB2 = new Point(1, 0);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineSegmentIntersection(lineA, lineB);

            // Assert
            Assert.IsNotNull(intersectionPoint);
            Assert.AreEqual(new Point(0.5, 0.5), intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineSegmentIntersection_LinesIntersectOutsideSegments_ReturnsNull()
        {
            // Arrange
            var pointA1 = new Point(1, 1);
            var pointA2 = new Point(3, 3);
            var pointB1 = new Point(5, 3);
            var pointB2 = new Point(7, 1);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineSegmentIntersection(lineA, lineB);

            // Assert
            Assert.IsNull(intersectionPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetLineSegmentIntersection_LinesIntersectWithinOneSegment_ReturnsNull()
        {
            // Arrange
            var pointA1 = new Point(1, 1);
            var pointA2 = new Point(3, 3);
            var pointB1 = new Point(3, 1);
            var pointB2 = new Point(4, 0);

            var lineA = new Line(pointA1, pointA2);
            var lineB = new Line(pointB1, pointB2);

            // Act
            var intersectionPoint = this.testCandidate.GetLineSegmentIntersection(lineA, lineB);

            // Assert
            Assert.IsNull(intersectionPoint);
        }

        #endregion
    }
}

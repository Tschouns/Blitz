//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest.Algorithms.Gjk
{
    using System;
    using Geometry.Elements;
    using Geometry.Services.Algorithms.Gjk;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for <see cref="CircleSupportFunctions"/>.
    /// </summary>
    [TestClass]
    public class CircleSupportFunctionsTests
    {
        /// <summary>
        /// Stores the tolerance for the X and Y deviation from the expected support point.
        /// </summary>
        private const double Tolerance = 0.00001f;

        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private CircleSupportFunctions _testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._testCandidate = new CircleSupportFunctions();
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPoint_CircleCenteredOnOriginDirectionPositive_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var center = new Point(0, 0);
            var radius = 7.0f;
            var direction = new Vector2(1, 1);

            var expectedSupportPoint = new Point(4.94974f, 4.94974f);

            // Act / Assert
            this.TestGetSupportPoint(center, radius, direction, expectedSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPoint_CircleInPositiveQuadrantDirectionPositive_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var center = new Point(8, 8);
            var radius = 7.0f;
            var direction = new Vector2(6.789, 6.789);

            var expectedSupportPoint = new Point(12.94974f, 12.94974f);

            // Act / Assert
            this.TestGetSupportPoint(center, radius, direction, expectedSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPoint_CircleInPositiveQuadrantDirectionNegative_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var center = new Point(8, 8);
            var radius = 7.0f;
            var direction = new Vector2(-4, -4);

            var expectedSupportPoint = new Point(3.05026f, 3.05026f);

            // Act / Assert
            this.TestGetSupportPoint(center, radius, direction, expectedSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPoint_CircleInPositiveQuadrantDirectionLeft_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var center = new Point(8, 8);
            var radius = 7.0f;
            var direction = new Vector2(-12.345, 0);

            var expectedSupportPoint = new Point(1, 8);

            // Act / Assert
            this.TestGetSupportPoint(center, radius, direction, expectedSupportPoint);
        }

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void GetSupportPoint_CircleBelowXAxisDirectionDescendingRight_ReturnsCorrectSupportPoint()
        {
            // Arrange
            var center = new Point(2, -8);
            var radius = 7.0f;
            var direction = new Vector2(12.345, -12.345);

            var expectedSupportPoint = new Point(6.94974f, -12.94974f);

            // Act / Assert
            this.TestGetSupportPoint(center, radius, direction, expectedSupportPoint);
        }

        /// <summary>
        /// Does the actual test of <see cref="CircleSupportFunctions.GetSupportPoint"/>.
        /// </summary>
        public void TestGetSupportPoint(Point center, double radius, Vector2 direction, Point expectedSupportPoint)
        {
            // Arrange
            var circle = new Circle(center, radius);

            // Act
            var resultSupportPoint = this._testCandidate.GetSupportPoint(circle, direction);

            Console.WriteLine($"Expected support point: {expectedSupportPoint}");
            Console.WriteLine($"Result support point: {resultSupportPoint}");

            // Assert
            var deviationX = Math.Abs(expectedSupportPoint.X - resultSupportPoint.X);
            var deviationY = Math.Abs(expectedSupportPoint.Y - resultSupportPoint.Y);

            Console.WriteLine($"Derivation (x, y): {deviationX}, {deviationY}");

            Assert.IsTrue(deviationX <= Tolerance);
            Assert.IsTrue(deviationY <= Tolerance);
        }
    }
}

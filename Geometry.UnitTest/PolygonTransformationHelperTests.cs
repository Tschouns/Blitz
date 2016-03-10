//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest
{
    using System;
    using Elements;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Helpers;
    
    /// <summary>
    /// Contains unit tests for <see cref="PolygonTransformationHelper"/>.
    /// </summary>
    [TestClass]
    public class PolygonTransformationHelperTests
    {
        /// <summary>
        /// Used to verify certain results.
        /// </summary>
        private IPolygonCalculationHelper polygonCalculationHelper;

        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private PolygonTransformationHelper testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.polygonCalculationHelper = new PolygonCalculationHelper(new LineIntersectionHelper());

            this.testCandidate = new PolygonTransformationHelper(
                new PointTransformationHelper(),
                this.polygonCalculationHelper);
        }

        #region AreLinesParallel

        /// <summary>
        /// Does a test.
        /// </summary>
        [TestMethod]
        public void CenterOnOrigin_PolygonHasPositiveOffset_CentroidEqualsOrigin()
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
            var original = new Polygon(corners);
            var result = this.testCandidate.CenterOnOrigin(original);

            // Assert - The area must remain the same.
            Assert.AreEqual(
                this.polygonCalculationHelper.CalculateArea(original),
                this.polygonCalculationHelper.CalculateArea(result));

            // Assert - The centroid must be {0,0} (or almost: the floating point calculations may leave tiny inaccuracies).
            var resultCentroid = this.polygonCalculationHelper.DetermineCentroid(result);

            var roundedResultCentroidX = Math.Round(resultCentroid.X, 15);
            var roundedResultCentroidY = Math.Round(resultCentroid.Y, 15);

            Assert.AreEqual(
                GeometryGlobalConstants.Origin.X,
                roundedResultCentroidX);

            Assert.AreEqual(
                GeometryGlobalConstants.Origin.Y,
                roundedResultCentroidY);
        }

        #endregion
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.UnitTest.Helpers
{
    using System;
    using Geometry.Elements;
    using Geometry.Helpers;
    using Geometry.Service.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for <see cref="PolygonTransformationHelper"/>.
    /// </summary>
    [TestClass]
    public class PolygonTransformationHelperTests
    {
        /// <summary>
        /// Used to verify certain results.
        /// </summary>
        private IPolygonCalculationHelper _polygonCalculationHelper;

        /// <summary>
        /// Stores the test candidate.
        /// </summary>
        private PolygonTransformationHelper _testCandidate;

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._polygonCalculationHelper = new PolygonCalculationHelper(new LineIntersectionHelper());

            this._testCandidate = new PolygonTransformationHelper(
                new PointTransformationHelper(),
                this._polygonCalculationHelper);
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
            var result = this._testCandidate.CenterOnOrigin(original);

            // Assert - The area must remain the same.
            Assert.AreEqual(
                this._polygonCalculationHelper.CalculateArea(original),
                this._polygonCalculationHelper.CalculateArea(result));

            // Assert - The centroid must be {0,0} (or almost: the floating point calculations may leave tiny inaccuracies).
            var resultCentroid = this._polygonCalculationHelper.DetermineCentroid(result);

            var roundedResultCentroidX = Math.Round(resultCentroid.X, 15);
            var roundedResultCentroidY = Math.Round(resultCentroid.Y, 15);

            Assert.AreEqual(
                GeometryConstants.Origin.X,
                roundedResultCentroidX);

            Assert.AreEqual(
                GeometryConstants.Origin.Y,
                roundedResultCentroidY);
        }

        #endregion
    }
}

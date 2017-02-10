//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using System.Numerics;
    using Geometry.Elements;
    using Geometry.Transformation;

    /// <summary>
    /// Represents the transformation for a specific camera state. Transforms positions forth
    /// and back between world and viewport coordinates.
    /// </summary>
    public interface ICameraTransformation
    {
        /// <summary>
        /// Transforms the specified position from world coordinates into viewport coordinates.
        /// </summary>
        Point WorldToViewport(Point worldPosition);

        /// <summary>
        /// Transforms (or scales) the specified distance from world viewport.
        /// </summary>
        double WorldToViewport(double worldDistance);

        /// <summary>
        /// Transforms the specified position from viewport coordinates into world coordinates.
        /// </summary>
        Point ViewportToWorld(Point viewportPosition);

        /// <summary>
        ///  Transforms (or scales) the specified distance from world viewport.
        /// </summary>
        double ViewportToWorld(double viewportDistance);

        /// <summary>
        /// Gets a 3x3 matrix which represents the transformation from world to viewport coordinates.
        /// </summary>
        Matrix3x3 WorldToViewportMatrix3x3();
    }
}

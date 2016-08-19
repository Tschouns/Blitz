//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera
{
    using Geometry.Elements;

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
        /// Transforms the specified position from viewport coordinates into world coordinates.
        /// </summary>
        Point ViewportToWorld(Point viewportPosition);
    }
}

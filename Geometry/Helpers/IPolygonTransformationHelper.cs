//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Helpers
{
    using Elements;

    /// <summary>
    /// Provides methods to perform different polygon transformations.
    /// </summary>
    public interface IPolygonTransformationHelper
    {
        /// <summary>
        /// Translates the specified polygon by a specified offset.
        /// </summary>
        Polygon TranslatePolygon(Vector2 offset, Polygon polygon);

        /// <summary>
        /// Rotates the specified polygon, around the specified origin, by the specified angle (in radians).
        /// </summary>
        Polygon RotatePolygon(Point origin, double angle, Polygon polygon);

        /// <summary>
        /// Translates the specified polygon in such a way, that its centroid is aligned with the origin.
        /// </summary>
        Polygon CenterOnOrigin(Polygon polygon);
    }
}

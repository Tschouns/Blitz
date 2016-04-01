//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Algorithms.Gjk
{
    using Geometry.Elements;

    /// <summary>
    /// Represents a "support function" used in the <c>Gilbert-Johnson-Keerthi</c> algorithm to determine
    /// the point of a specified figure which is furthest along a specified direction vector.
    /// </summary>
    /// <typeparam name="TFigure">
    /// Type of the geometric figure the support function is for
    /// </typeparam>
    public interface ISupportFunctions<TFigure>
        where TFigure : class, IFigure
    {
        /// <summary>
        /// Gets the point of the specified figure which is farthest along the specified direction vector.
        /// </summary>
        Point GetSupportPoint(TFigure figure, Vector2 direction);
    }
}

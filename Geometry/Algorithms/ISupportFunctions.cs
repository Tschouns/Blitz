//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Algorithms
{
    using Elements;

    /// <summary>
    /// Represents a "support function" used in the <c>Gilbert-Johnson-Keerthi</c> and other algorithms to
    /// determine certain properties of geometric figures.
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

        /// <summary>
        /// Gets the point in a specified figure outline which is closest to the specified position.
        /// </summary>
        Point GetFigureOutlinePointClosestToPosition(TFigure figure, Point position);
    }
}

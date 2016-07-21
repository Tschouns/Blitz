//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Algorithms.Gjk
{
    using Elements;

    /// <summary>
    /// Provides methods to determine information about distance and intersection between
    /// two geometric figures, based on the <c>Gilbert-Johnson-Keerthi</c> algorithm.
    /// </summary>
    /// <typeparam name="TFigure1">
    /// Type of figure 1
    /// </typeparam>
    /// <typeparam name="TFigure2">
    /// Type of figure 2
    /// </typeparam>
    public interface IGjkAlgorithm<TFigure1, TFigure2>
        where TFigure1 : class, IFigure
        where TFigure2 : class, IFigure
    {
        /// <summary>
        /// Determines whether two figures intersect.
        /// </summary>
        FigureIntersectionResult DoFiguresIntersect(TFigure1 figure1, TFigure2 figure2);
    }
}

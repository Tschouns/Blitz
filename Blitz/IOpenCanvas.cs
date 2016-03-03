//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Windows.Media;

    /// <summary>
    /// Represents a canvas to which drawings can be added.
    /// </summary>
    public interface IOpenCanvas
    {
        /// <summary>
        /// Adds a drawing to the canvas.
        /// </summary>
        void AddDrawing(DrawingVisual drawing);

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        void ClearDrawings();
    }
}

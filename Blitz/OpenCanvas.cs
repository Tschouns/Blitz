//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Blitz
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using Base.RuntimeChecks;

    /// <summary>
    /// Represents a canvas to which drawings can be added.
    /// </summary>
    public class OpenCanvas : Canvas, IOpenCanvas
    {
        /// <summary>
        /// See <see cref="IOpenCanvas.AddDrawing"/>.
        /// </summary>
        public void AddDrawing(DrawingVisual drawing)
        {
            Checks.AssertNotNull(drawing, nameof(drawing));

            this.AddVisualChild(drawing);
        }

        /// <summary>
        /// See <see cref="IOpenCanvas.ClearDrawings"/>.
        /// </summary>
        public void ClearDrawings()
        {
            this.Children.Clear();
        }
    }
}

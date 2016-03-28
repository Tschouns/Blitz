//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display
{
    using Geometry.Elements;

    /// <summary>
    /// Provides an interface for drawing a frame.
    /// </summary>
    public interface IDrawingContext
    {
        /// <summary>
        /// Draws a line.
        /// </summary>
        void DrawLine(Point point1, Point point2, System.Drawing.Color color, float strokeWidth);
    }
}

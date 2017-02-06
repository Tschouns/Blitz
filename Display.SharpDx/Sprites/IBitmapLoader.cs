//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Sprites
{
    using SharpDX.Direct2D1;

    /// <summary>
    /// Provides methods to load bitmaps for a render target.
    /// </summary>
    public interface IBitmapLoader
    {
        Bitmap LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, RenderTarget renderTarget);
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Sprites
{
    using SharpDX.Direct2D1;
    using Base.RuntimeChecks;
    using SharpDX.DXGI;
    using SharpDX;
    using System.Runtime.InteropServices;
    using System.Drawing.Imaging;

    /// <summary>
    /// See <see cref="IBitmapLoader"/>.
    /// </summary>
    public class BitmapLoader : IBitmapLoader
    {
        /// <summary>
        /// See <see cref="IBitmapLoader.LoadFromDrawingBitmap(System.Drawing.Bitmap, RenderTarget)"/>.
        /// From: http://stackoverflow.com/questions/5160510/loading-creating-an-image-into-sharpdx-in-a-net-program
        /// </summary>
        public Bitmap LoadFromDrawingBitmap(System.Drawing.Bitmap bitmap, RenderTarget renderTarget)
        {
            Checks.AssertNotNull(bitmap, nameof(bitmap));
            Checks.AssertNotNull(renderTarget, nameof(renderTarget));

            var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
            var size = new Size2(bitmap.Width, bitmap.Height);

            // Transform pixels from BGRA to RGBA.
            int stride = bitmap.Width * sizeof(int);
            using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
            {
                // Lock System.Drawing.Bitmap.
                var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                // Convert all pixels.
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int offset = bitmapData.Stride * y;
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Not optimized...
                        byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        int rgba = R | (G << 8) | (B << 16) | (A << 24);
                        tempStream.Write(rgba);
                    }

                }

                bitmap.UnlockBits(bitmapData);
                tempStream.Position = 0;

                var sharpDxBitmap = new Bitmap(renderTarget, size, tempStream, stride, bitmapProperties);

                return sharpDxBitmap;
            }
        }
    }
}

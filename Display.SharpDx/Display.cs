//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx
{
    using System;

    /// <summary>
    /// Implementation of <see cref="IDisplay"/>, based on <see cref="SharpDX"/>.
    /// </summary>
    public sealed class Display : IDisplay
    {
        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// See <see cref="IDisplay.Draw"/>.
        /// </summary>
        public void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}

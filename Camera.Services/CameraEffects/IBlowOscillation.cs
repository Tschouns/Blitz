//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Geometry.Elements;

namespace Camera.Services.CameraEffects
{
    /// <summary>
    /// Simulates an oscillation as caused by a "blow".
    /// </summary>
    public interface IBlowOscillation
    {
        /// <summary>
        /// Gets a value indicating whether the oscillation has depleted.
        /// </summary>
        bool HasDepleted { get; }

        /// <summary>
        /// Gets the current oscillation.
        /// </summary>
        Vector2 CurrentOscillation { get; }

        /// <summary>
        /// Updates the blow oscillation.
        /// </summary>
        void Update(double timeElapsed);
    }
}

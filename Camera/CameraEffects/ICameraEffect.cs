//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.CameraEffects
{
    /// <summary>
    /// Represents a camera effect. When applied to a <see cref="ICamera"/>, it can manipulate
    /// its properties, e.g. based on user input, an in-game character's position or in any pre-defined
    /// way...
    /// </summary>
    public interface ICameraEffect
    {
        /// <summary>
        /// Updates the camera effect, potentially with regard to elapsed time.
        /// This is supposed to be called once in the game/sim update loop.
        /// </summary>
        void Update(double timeElapsed);

        /// <summary>
        /// Applies the effect
        /// </summary>
        void ApplyToCamera(ICamera camera);
    }
}

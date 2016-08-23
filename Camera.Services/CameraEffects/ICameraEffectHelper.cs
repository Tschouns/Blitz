//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    /// <summary>
    /// Provides helper methods, which are typically used by camera effects.
    /// </summary>
    public interface ICameraEffectHelper
    {
        /// <summary>
        /// Limits the specified value by the specified lower and upper limit.
        /// </summary>
        double LimitValue(double value, double lowerLimit, double upperLimit);
    }
}

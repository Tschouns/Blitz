//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Camera.Services.CameraEffects
{
    /// <summary>
    /// See <see cref="ICameraEffectHelper"/>.
    /// </summary>
    public class CameraEffectHelper : ICameraEffectHelper
    {
        /// <summary>
        /// See <see cref="ICameraEffectHelper.LimitValue(double, double, double)"/>.
        /// </summary>
        public double LimitValue(double value, double lowerLimit, double upperLimit)
        {
            if (value < lowerLimit)
            {
                return lowerLimit;
            }

            if (value > upperLimit)
            {
                return upperLimit;
            }

            return value;
        }
    }
}

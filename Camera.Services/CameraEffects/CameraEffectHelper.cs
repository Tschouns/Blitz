//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;

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

        /// <summary>
        /// See <see cref="ICameraEffectHelper.OutOfBounds(double, double, double)"/>.
        /// </summary>
        public double OutOfBounds(double value, double lowerLimit, double uppderLimit)
        {
            if (value < lowerLimit)
            {
                return value - lowerLimit;
            }

            if (value > uppderLimit)
            {
                return value - uppderLimit;
            }

            return 0;
        }
    }
}

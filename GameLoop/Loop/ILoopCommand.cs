//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Loop
{
    /// <summary>
    /// Provides an interface to control the loop.
    /// </summary>
    public interface ILoopCommand
    {
        /// <summary>
        /// Gets or sets the factor by which in-game time will pass, relative to real time.
        /// Default is 1.0. Increasing or decreasing this factor will cause in-game time to go
        /// faster or slower, respectively.
        /// </summary>
        double GameTimeFactor { get; set; }

        /// <summary>
        /// Stops the loop. The current update/draw cycle will finish regularly, but the
        /// callbacks will not be called again after that.
        /// </summary>
        void Stop();
    }
}

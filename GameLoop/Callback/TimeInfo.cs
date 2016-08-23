//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop
{
    /// <summary>
    /// Provides time information.
    /// </summary>
    public class TimeInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeInfo"/> class.
        /// </summary>
        public TimeInfo(double elapsed, double total)
        {
            this.Elapsed = elapsed;
            this.Total = total;
        }

        /// <summary>
        /// Gets the amount of time gone by since the last update.
        /// </summary>
        public double Elapsed { get; }

        /// <summary>
        /// Gets the total amount of time gone by since the start.
        /// </summary>
        public double Total { get; }
    }
}

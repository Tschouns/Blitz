//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GameLoop.Services.Loop
{
    using System;
    using Base.RuntimeChecks;
    using GameLoop;
    using GameLoop.Loop;

    /// <summary>
    /// 
    /// </summary>
    public class LoopCommand : ILoopCommand
    {
        /// <summary>
        /// The delegate to call when the loop shall be stopped.
        /// </summary>
        private readonly Action _stopLoopAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoopCommand"/> class.
        /// </summary>
        public LoopCommand(
            double initialGameTimeFactor,
            Action stopLoopAction)
        {
            Checks.AssertNotNull(stopLoopAction, nameof(stopLoopAction));

            this.GameTimeFactor = initialGameTimeFactor;
            this._stopLoopAction = stopLoopAction;
        }

        /// <summary>
        /// See <see cref="ILoopCommand.GameTimeFactor"/>.
        /// </summary>
        public double GameTimeFactor { get; set; }

        /// <summary>
        /// See <see cref="ILoopCommand.Stop"/>.
        /// </summary>
        public void Stop()
        {
            this._stopLoopAction();
        }
    }
}

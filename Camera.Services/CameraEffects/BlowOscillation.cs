//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;

    /// <summary>
    /// See <see cref="IBlowOscillation"/>.
    /// </summary>
    public class BlowOscillation : IBlowOscillation
    {
        /// <summary>
        /// Stores the start oscillation, which is also the peak.
        /// </summary>
        private readonly Vector2 _startOscillation;

        /// <summary>
        /// Stores the total duration of the blow oscillation.
        /// </summary>
        private readonly double _totalDuration;

        /// <summary>
        /// Stores the oscillation frequency * 2Pi, as the Sin function expects the x in radians.
        /// </summary>
        private readonly double _frequencyTimes2Pi;

        /// <summary>
        /// Stores the time component. Progresses from 0 towards the total duration.
        /// </summary>
        private double _time;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlowOscillation"/> class.
        /// </summary>
        public BlowOscillation(
            Vector2 startOscillation,
            double duration,
            double frequency)
        {
            Checks.AssertIsPositive(frequency, nameof(frequency));

            this._startOscillation = startOscillation;
            this._totalDuration = duration;
            this._frequencyTimes2Pi = frequency * 2 * Math.PI;

            this._time = 0;
            this.CurrentOscillation = new Vector2(0, 0);
        }

        /// <summary>
        /// See <see cref="IBlowOscillation.CurrentOscillation"/>.
        /// </summary>
        public Vector2 CurrentOscillation { get; private set; }

        /// <summary>
        /// See <see cref="IBlowOscillation.HasDepleted"/>.
        /// </summary>
        public bool HasDepleted => this._time >= this._totalDuration;

        /// <summary>
        /// See <see cref="IBlowOscillation.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            this._time += timeElapsed;

            var sinFactor = Math.Sin(this._time * this._frequencyTimes2Pi);
            var remainingStrengthFactor = (this._totalDuration - this._time) / this._totalDuration;

            Console.Write(sinFactor);
            Console.Write(" * ");
            Console.Write(remainingStrengthFactor);
            Console.Write(" = ");
            Console.WriteLine(sinFactor * remainingStrengthFactor);

            this.CurrentOscillation = this._startOscillation.Multiply(sinFactor * remainingStrengthFactor);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using System;
    using Base.RuntimeChecks;
    using Geometry.Algorithms;
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// Simulates a blast, which pushes surrounding objects away.
    /// </summary>
    public class Blast<TShapeFigure> : IGlobalForce<IBody<TShapeFigure>>
        where TShapeFigure : class, IFigure
    {
        /// <summary>
        /// Used to determine the point in a body closest to the blast.
        /// </summary>
        private readonly ISupportFunctions<TShapeFigure> _supportFunctions;

        /// <summary>
        /// The position the blast originates from.
        /// </summary>
        private readonly Point _position;

        /// <summary>
        /// The maximum force induced by this blast. Decreases over time.
        /// </summary>
        private readonly double _maxForce;

        /// <summary>
        /// The maximum blast radius. Increases over time.
        /// </summary>
        private readonly double _maxBlastRadius;

        /// <summary>
        /// The speed by which the blast expands, i.e. the blast radius is increased.
        /// </summary>
        private readonly double _expansionSpeed;

        /// <summary>
        /// The speed by which the force is depleted. Is determined so that the force is
        /// depleted around the same time the blast reaches its expansion limit.
        /// </summary>
        private readonly double _forceDepletionSpeed;

        /// <summary>
        /// The current force this blast induces when applied to an object.
        /// </summary>
        private double _currentForce;

        /// <summary>
        /// The current blast radius.
        /// </summary>
        private double _currentBlastRadius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Blast"/> class.
        /// </summary>
        public Blast(
            Point position,
            double force,
            double blastRadius,
            double expansionSpeed)
        {
            Checks.AssertIsPositive(blastRadius, nameof(blastRadius));
            Checks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));

            this._position = position;
            this._maxForce = force;
            this._maxBlastRadius = blastRadius;
            this._expansionSpeed = expansionSpeed;
            this._forceDepletionSpeed = (this._maxForce / this._maxBlastRadius) * this._expansionSpeed;

            this._currentForce = force;
            this._currentBlastRadius = 0.0;
        }

        /// <summary>
        /// See <see cref="IGlobalForce.Step(double)"/>.
        /// </summary>
        public void Step(double time)
        {
            this._currentForce -= time * this._forceDepletionSpeed;
            this._currentBlastRadius += time * this._expansionSpeed;
        }

        /// <summary>
        /// See <see cref="IGlobalForce.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(IBody<TShapeFigure> physicalObject)
        {
            throw new NotImplementedException();
        }
    }
}

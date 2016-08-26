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
    using Geometry.Extensions;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// Simulates a blast, which pushes surrounding objects away. It can be applied
    /// to any <see cref="IBody{TShapeFigure}"/>.
    /// </summary>
    public class BodyBlast<TShapeFigure> : IForce<IBody<TShapeFigure>>
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
        public BodyBlast(
            ISupportFunctions<TShapeFigure> supportFunctions,
            Point position,
            double force,
            double blastRadius,
            double expansionSpeed)
        {
            Checks.AssertNotNull(supportFunctions, nameof(supportFunctions));
            Checks.AssertIsPositive(blastRadius, nameof(blastRadius));
            Checks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));

            this._supportFunctions = supportFunctions;

            this._position = position;
            this._maxForce = force;
            this._maxBlastRadius = blastRadius;
            this._expansionSpeed = expansionSpeed;
            this._forceDepletionSpeed = (this._maxForce / this._maxBlastRadius) * this._expansionSpeed;

            this._currentForce = force;
            this._currentBlastRadius = 0.0;
        }

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.IsDepleted"/>.
        /// </summary>
        public bool IsDepleted => this._currentForce <= 0;

        /// <summary>
        /// See <see cref="IForce.Step(double)"/>.
        /// </summary>
        public void Step(double time)
        {
            if (this.IsDepleted)
            {
                return;
            }

            this._currentForce -= time * this._forceDepletionSpeed;
            this._currentBlastRadius += time * this._expansionSpeed;
        }

        /// <summary>
        /// See <see cref="IForce.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(IBody<TShapeFigure> physicalObject)
        {
            Checks.AssertNotNull(physicalObject, nameof(physicalObject));

            var pointClosestToBlastCenter = this._supportFunctions.GetFigureOutlinePointClosestToPosition(
                physicalObject.Shape.Current,
                this._position);

            var forceVector = pointClosestToBlastCenter.GetOffsetFrom(this._position).Norm().Multiply(this._currentForce);
            var forceApplicationOffset = pointClosestToBlastCenter.GetOffsetFrom(physicalObject.CurrentState.Position);

            physicalObject.ApplyForceAtOffset(forceVector, forceApplicationOffset);
        }
    }
}

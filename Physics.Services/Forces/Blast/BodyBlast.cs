//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces.Blast
{
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
            ArgumentChecks.AssertNotNull(supportFunctions, nameof(supportFunctions));
            ArgumentChecks.AssertIsPositive(blastRadius, nameof(blastRadius));
            ArgumentChecks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));

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
            ArgumentChecks.AssertNotNull(physicalObject, nameof(physicalObject));

            // TODO: make this class "more generic", so it could be used for particles as well. Add a strategy to determine the "point to apply the force to".
            var pointClosestToBlastCenter = this._supportFunctions.GetFigureOutlinePointClosestToPosition(
                physicalObject.Shape.Current,
                this._position);

            // Is the object within reach of the blast?
            var offset = pointClosestToBlastCenter.GetOffsetFrom(this._position);
            if (offset.Magnitude() > this._currentBlastRadius)
            {
                return;
            }

            var forceVector = offset.Norm().Multiply(this._currentForce);

            physicalObject.ApplyForceAtPointInSpace(forceVector, pointClosestToBlastCenter);
        }
    }
}

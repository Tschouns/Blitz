//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces.FlowResistance
{
    using Geometry.Elements;
    using Physics.Elements;
    using Physics.Forces;
    using Base.RuntimeChecks;
    using Geometry.Algorithms;
    using Geometry.Extensions;
    using Geometry.Helpers;
    
    /// <summary>
    /// Simulates flow resistance to linear motion. It slows down bodies based on their velocity and exposed area.
    /// </summary>
    public class BodyLinearFlowResistance<TShapeFigure> : IForce<IBody<TShapeFigure>>
        where TShapeFigure : class, IFigure
    {
        /// <summary>
        /// Used to find perpendiculars through specific point.
        /// </summary>
        private readonly ILineCalculationHelper _lineCalculationHelper;

        /// <summary>
        /// Used to determine the exposed "area" (actually a length... or width... because 2D, see).
        /// </summary>
        private readonly ISupportFunctions<TShapeFigure> _supportFunctions;

        /// <summary>
        /// The density of the fluid or gas.
        /// </summary>
        private readonly double _density;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyFlowResistance"/> class.
        /// </summary>
        public BodyLinearFlowResistance(
            ILineCalculationHelper lineCalculationHelper,
            ISupportFunctions<TShapeFigure> supportFunctions,
            double density)
        {
            ArgumentChecks.AssertNotNull(lineCalculationHelper, nameof(lineCalculationHelper));
            ArgumentChecks.AssertNotNull(supportFunctions, nameof(supportFunctions));
            ArgumentChecks.AssertIsPositive(density, nameof(density));

            this._lineCalculationHelper = lineCalculationHelper;
            this._supportFunctions = supportFunctions;
            this._density = density;
        }

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.IsDepleted"/>. This force is never depleted.
        /// </summary>
        public bool IsDepleted => false;

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.Step(double)"/>.
        /// </summary>
        public void Step(double time)
        {
            // Flow resistance does not change over time.
        }

        /// <summary>
        /// See <see cref="IForce{TPhysicalObject}.ApplyToObject(TPhysicalObject)"/>.
        /// </summary>
        public void ApplyToObject(IBody<TShapeFigure> physicalObject)
        {
            ArgumentChecks.AssertNotNull(physicalObject, nameof(physicalObject));

            var velocity = physicalObject.CurrentState.Velocity;
            if (velocity.SquaredMagnitude() == 0)
            {
                return;
            }

            var forceDirection = velocity.Invert();
            var supportPointLeft = this._supportFunctions.GetSupportPoint(physicalObject.Shape.Current, forceDirection.Get90DegreesLeft());
            var supportPointRight = this._supportFunctions.GetSupportPoint(physicalObject.Shape.Current, forceDirection.Get90DegreesRight());

            var intersectionWithPerpendicular = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughPoint(
                new Line(physicalObject.CurrentState.Position, physicalObject.CurrentState.Position.AddVector(forceDirection)),
                supportPointLeft);

            var pointOppositeLeftSupportPoint = this._lineCalculationHelper.GetIntersectionWithPerpendicularThroughPoint(
                new Line(supportPointLeft, intersectionWithPerpendicular),
                supportPointRight);

            var offset = pointOppositeLeftSupportPoint.GetOffsetFrom(supportPointLeft);

            // Determine the exposed "area".
            var exposedArea = offset.SquaredMagnitude();

            // Determine force.
            var forceMagnitude = exposedArea * this._density * velocity.SquaredMagnitude() / 2;
            var forceVector = forceDirection.Norm().Multiply(forceMagnitude);

            // Determine the point to apply force to.
            var pointToApplyForceTo = supportPointLeft.AddVector(offset.Divide(2));

            // Apply force.
            physicalObject.ApplyForceAtPointInSpace(forceVector, pointToApplyForceTo);
        }

    }
}

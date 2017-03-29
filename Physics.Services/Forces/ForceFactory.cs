//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Forces
{
    using Base.RuntimeChecks;
    using Geometry.Algorithms;
    using Geometry.Elements;
    using Physics.Elements;
    using Geometry.Helpers;
    using Physics.Forces;
    using Blast;
    using FlowResistance;
    using Gravity;

    /// <summary>
    /// /// See <see cref="IForceFactory"/>.
    /// /// </summary>
    public class ForceFactory : IForceFactory
    {
        /// <summary>
        /// Used by forces.
        /// </summary>
        private readonly ILineCalculationHelper _lineCalculationHelper;

        /// <summary>
        /// Used by forces.
        /// </summary>
        private readonly ISupportFunctions<Polygon> _polygonSupportFunctions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForceFactory"/> class.
        /// </summary>
        public ForceFactory(
            ILineCalculationHelper lineCalculationHelper,
            ISupportFunctions<Polygon> polygonSupportFunctions)
        {
            ArgumentChecks.AssertNotNull(lineCalculationHelper, nameof(lineCalculationHelper));
            ArgumentChecks.AssertNotNull(polygonSupportFunctions, nameof(polygonSupportFunctions));

            this._lineCalculationHelper = lineCalculationHelper;
            this._polygonSupportFunctions = polygonSupportFunctions;
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateGravity(double)"/>.
        /// </summary>
        public ForceSet CreateGravity(double acceleration)
        {
            ArgumentChecks.AssertIsPositive(acceleration, nameof(acceleration));

            var gravityForParticles = new GenericGravity<IParticle>(acceleration);
            var gravityForBodies = new GenericGravity<IBody<Polygon>>(acceleration);

            return new ForceSet(gravityForParticles, gravityForBodies);
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateLinearFlowRestistance(double)"/>.
        /// </summary>
        public ForceSet CreateLinearFlowRestistance(double density)
        {
            ArgumentChecks.AssertIsPositive(density, nameof(density));

            var flowResistanceForParticles = new GenericDummyForce<IParticle>();
            var flowResistanceForBodies = new BodyLinearFlowResistance<Polygon>(
                this._lineCalculationHelper,
                this._polygonSupportFunctions,
                density);

            return new ForceSet(flowResistanceForParticles, flowResistanceForBodies);
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateRotaionalFlowRestistance(double)"/>.
        /// </summary>
        public ForceSet CreateRotaionalFlowRestistance(double density)
        {
            ArgumentChecks.AssertIsPositive(density, nameof(density));

            var flowResistanceForParticles = new GenericDummyForce<IParticle>();
            var flowResistanceForBodies = new BodyRotationalFlowResistance<Polygon>( density);

            return new ForceSet(flowResistanceForParticles, flowResistanceForBodies);
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateBlast(Point, double, double, double)"/>.
        /// </summary>
        public ForceSet CreateBlast(
            Point position,
            double force,
            double blastRadius,
            double expansionSpeed)
        {
            ArgumentChecks.AssertIsPositive(blastRadius, nameof(blastRadius));
            ArgumentChecks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));

            // TODO: make blast generic, add "apply strategies" for each type.
            var blastForParticles = new GenericDummyForce<IParticle>();
            var blastForBodies = new BodyBlast<Polygon>(
                this._polygonSupportFunctions,
                position,
                force,
                blastRadius,
                expansionSpeed);

            return new ForceSet(blastForParticles, blastForBodies);
        }
    }
}

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
    using Physics.Forces;

    /// <summary>
    /// See <see cref="IForceFactory"/>.
    /// </summary>
    public class ForceFactory : IForceFactory
    {
        /// <summary>
        /// Used by certain forces.
        /// </summary>
        private readonly ISupportFunctions<Polygon> _polygonSupportFunctions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForceFactory"/> class.
        /// </summary>
        public ForceFactory(ISupportFunctions<Polygon> polygonSupportFunctions)
        {
            Checks.AssertNotNull(polygonSupportFunctions, nameof(polygonSupportFunctions));

            this._polygonSupportFunctions = polygonSupportFunctions;
        }

        /// <summary>
        /// See <see cref="IForceFactory.CreateGravity(double)"/>.
        /// </summary>
        public ForceSet CreateGravity(double acceleration)
        {
            Checks.AssertIsPositive(acceleration, nameof(acceleration));

            var gravityForParticles = new GenericGravity<IParticle>(acceleration);
            var gravityForBodies = new GenericGravity<IBody<Polygon>>(acceleration);

            return new ForceSet(gravityForParticles, gravityForBodies);
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
            Checks.AssertIsPositive(blastRadius, nameof(blastRadius));
            Checks.AssertIsStrictPositive(expansionSpeed, nameof(expansionSpeed));

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

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Elements
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Physics.Elements;
    using Physics.Forces;

    /// <summary>
    /// See <see cref="IPhysicalSpace"/>.
    /// </summary>
    public class PhysicalSpace : IPhysicalSpace
    {
        /// <summary>
        /// Holds the global forces in the space.
        /// </summary>
        private readonly IList<IGlobalForce> globalForces;

        /// <summary>
        /// Holds all "physical" objects in the space.
        /// </summary>
        private readonly IList<IPhysicalObject> physicalObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalSpace"/> class.
        /// </summary>
        public PhysicalSpace()
        {
            this.globalForces = new List<IGlobalForce>();
            this.physicalObjects = new List<IPhysicalObject>();
        }

        /// <summary>
        /// See <see cref="IPhysicalSpace.AddGlobalForce"/>.
        /// </summary>
        public void AddGlobalForce(IGlobalForce globalForce)
        {
            Checks.AssertNotNull(globalForce, nameof(globalForce));

            this.globalForces.Add(globalForce);
        }

        /// <summary>
        /// See <see cref="IPhysicalSpace.AddParticle"/>.
        /// </summary>
        public void AddParticle(IParticle particle)
        {
            Checks.AssertNotNull(particle, nameof(particle));

            this.physicalObjects.Add(particle);
        }

        /// <summary>
        /// See <see cref="IPhysicalSpace.Step"/>.
        /// </summary>
        public void Step(double time)
        {
            Checks.AssertIsPositive(time, nameof(time));

            foreach (var physicalObject in this.physicalObjects)
            {
                foreach (var globalForce in this.globalForces)
                {
                    globalForce.ApplyToObject(physicalObject);
                }

                physicalObject.Step(time);
                physicalObject.ResetAppliedForce();
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;
    using Camera;
    using HumbleWorldObjects;

    /// <summary>
    /// Represents a "game state" for this demo.
    /// </summary>
    public class DemoGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGameState"/> class.
        /// </summary>
        public DemoGameState(
            ICameraTransformation cameraTransformation,
            IEnumerable<Building> buildings,
            IEnumerable<Car> cars,
            IEnumerable<Explosion> explosions)
        {
            Checks.AssertNotNull(cameraTransformation, nameof(cameraTransformation));
            Checks.AssertNotNull(buildings, nameof(buildings));
            Checks.AssertNotNull(cars, nameof(cars));
            Checks.AssertNotNull(explosions, nameof(explosions));

            this.CameraTransformation = cameraTransformation;
            this.Buildings = buildings;
            this.Cars = cars;
            this.Explosions = explosions;
        }

        /// <summary>
        /// Gets the camera transformation.
        /// </summary>
        public ICameraTransformation CameraTransformation { get; }

        /// <summary>
        /// Gets the buidlings in the world.
        /// </summary>
        public IEnumerable<Building> Buildings { get; }

        /// <summary>
        /// Gets the cars in the world.
        /// </summary>
        public IEnumerable<Car> Cars { get; }

        /// <summary>
        /// Gets the explosions in the world.
        /// </summary>
        public IEnumerable<Explosion> Explosions { get; }
    }
}

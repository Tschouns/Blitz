//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter.HumbleWorld
{
    using Base.RuntimeChecks;
    using Physics.World;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// The humble little world.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Used to create stuff.
        /// </summary>
        private readonly IPhysicsFactory _physicsFactory;

        /// <summary>
        /// Used to simulate physics.
        /// </summary>
        private readonly IPhysicalWorld _physicalWorld;

        /// <summary>
        /// Stores all the buildings of this fairly humble game world.
        /// </summary>
        private readonly IList<Building> _humbleBuildings = new List<Building>();

        /// <summary>
        /// Stores all the cars of this fairly humble game world.
        /// </summary>
        private readonly IList<Car> _humbleCars = new List<Car>();

        /// <summary>
        /// Stores all the explosions/craters in the world.
        /// </summary>
        private readonly IList<Explosion> _explosions = new List<Explosion>();

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World(IPhysicsFactory physicsFactory)
        {
            Checks.AssertNotNull(physicsFactory, nameof(physicsFactory));

            this._physicsFactory = physicsFactory;
            this._physicalWorld = this._physicsFactory.CreatePhysicalWorld();

            // Add air resistance.
            var airResistance = this._physicsFactory.Forces.CreateFlowRestistance(4);
            this._physicalWorld.AddForce(airResistance);

            this.PopulateWorld();
        }

        /// <summary>
        /// Gets the buildings in the world.
        /// </summary>
        public IEnumerable<Building> Buildings => this._humbleBuildings;

        /// <summary>
        /// Gets the cars in the world.
        /// </summary>
        public IEnumerable<Car> Cars => this._humbleCars;

        /// <summary>
        /// Gets the explosions going on in the world.
        /// </summary>
        public IEnumerable<Explosion> Expolosions => this._explosions;

        /// <summary>
        /// Spawns a car on the left, driving to the right.
        /// </summary>
        public void SpawnCarLeft()
        {
            var car = new Car(
                this._physicalWorld,
                Color.GreenYellow,
                new Point(-200, -5),
                false);

            this._humbleCars.Add(car);
        }

        /// <summary>
        /// Spawns a car on the right, driving to the left.
        /// </summary>
        public void SpawnCarRight()
        {
            var car = new Car(
                this._physicalWorld,
                Color.GreenYellow,
                new Point(200, 5),
                true);

            this._humbleCars.Add(car);
        }

        /// <summary>
        /// Spawns an explosion.
        /// </summary>
        public void SpawnExplosion(Point position, double explosionRadius, double explosionForce)
        {
            var explosion = new Explosion(position, explosionRadius);

            this._explosions.Add(explosion);

            var blast = this._physicsFactory.Forces.CreateBlast(position, explosionForce, explosionRadius, 500);

            this._physicalWorld.AddForce(blast);
        }

        /// <summary>
        /// Updates the world.
        /// </summary>
        public void Update(double elapsedTime)
        {
            // Remove cars.
            var carsOutOfRange = this._humbleCars.Where(car => Math.Abs(car.Position.X) > 200).ToList();
            foreach (var carToRemove in carsOutOfRange)
            {
                this._humbleCars.Remove(carToRemove);
            }

            // Update cars.
            foreach (var car in this._humbleCars)
            {
                car.Update();
            }

            // Update explosions.
            var currentExplosions = this._explosions.ToList();
            foreach (var explosion in currentExplosions)
            {
                explosion.Update(elapsedTime);
            }

            // Do physics.
            this._physicalWorld.Step(elapsedTime);
        }

        /// <summary>
        /// Adds some stuff to this "world".
        /// </summary>
        private void PopulateWorld()
        {
            // Add buildings north of the road.
            this._humbleBuildings.Add(new Building(
                new Point(0, 10),
                10,
                50,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(-20, 10),
                10,
                50,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(-40, 10),
                10,
                40,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(80, 10),
                20,
                30,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(110, 10),
                20,
                30,
                Color.Gray));

            // Add buildings south of the road.
            this._humbleBuildings.Add(new Building(
                new Point(-70, -30),
                60,
                20,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(0, -30),
                20,
                20,
                Color.Gray));

            this._humbleBuildings.Add(new Building(
                new Point(25, -30),
                20,
                20,
                Color.Gray));
        }
    }
}

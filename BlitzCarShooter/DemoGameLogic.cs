//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Input;
    using Base.RuntimeChecks;
    using Camera;
    using Camera.CameraEffects;
    using Geometry.Algorithms.Gjk;
    using Geometry.Elements;
    using HumbleWorld;
    using Input;
    using Input.InputAction;
    using RenderLoop;
    using RenderLoop.Callback;
    using RenderLoop.Loop;
    using Point = Geometry.Elements.Point;

    /// <summary>
    /// Represents the "game logic", besides drawing, for this demo. Implements <see cref="IUpdateCallback"/>.
    /// </summary>
    public class DemoGameLogic : IUpdateCallback<DemoGameState>
    {
        /// <summary>
        /// Used to manage user input.
        /// </summary>
        private readonly IInputActionManager _inputActionManager;

        /// <summary>
        /// The input action which ends the game.
        /// </summary>
        private readonly IInputAction _actionEndGame;

        /// <summary>
        /// The input action which spawns a car on the left, driving to the right.
        /// </summary>
        private readonly IInputAction _actionSpawnCarLeft;

        /// <summary>
        /// The input action which spawns a car on the right, driving to the left.
        /// </summary>
        private readonly IInputAction _actionSpawnCarRight;

        /// <summary>
        /// The input action which starts the "follow cam".
        /// </summary>
        private readonly IInputAction _actionFollowCam;

        /// <summary>
        /// The input action which shoots! Hurra!
        /// </summary>
        private readonly IInputAction _actionShoot;

        /// <summary>
        /// Used to create camera effects.
        /// </summary>
        private readonly ICameraEffectCreator _cameraEffectCreator;

        /// <summary>
        /// The camera controller, used control the camera by keyboard.
        /// </summary>
        private readonly ICameraController _cameraController;

        /// <summary>
        /// Helper used to detect collisions between explosions and cars.
        /// </summary>
        private readonly IGjkAlgorithm<Circle, Polygon> _gjk;

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
        /// Initializes a new instance of the <see cref="DemoGameLogic"/> class.
        /// </summary>
        public DemoGameLogic(
            IInputFactory inputFactory,
            ICameraFactory cameraFactory,
            IGjkAlgorithm<Circle, Polygon> gjk,
            Size viewportSize)
        {
            Checks.AssertNotNull(inputFactory, nameof(inputFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));
            Checks.AssertNotNull(gjk, nameof(gjk));

            this._gjk = gjk;

            // Setup input.
            var button = inputFactory.KeyboardButtonCreator;

            this._inputActionManager = inputFactory.CreateInputActionManager();
            this._actionSpawnCarLeft = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.NumPad4));
            this._actionSpawnCarRight = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.NumPad6));
            this._actionFollowCam = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.F));
            this._actionShoot = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.Space));
            this._actionEndGame = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.Escape));

            this._cameraEffectCreator = cameraFactory.CameraEffectCreator;

            var positionCameraEffect = this._cameraEffectCreator.CreatePositionAbsoluteByButtonsEffect(
                this._inputActionManager,
                button.Create(Key.W),
                button.Create(Key.S),
                button.Create(Key.A),
                button.Create(Key.D),
                50);

            var scaleCameraEffect = this._cameraEffectCreator.CreateScaleExponentialByButtonsEffect(
                this._inputActionManager,
                button.Create(Key.E),
                button.Create(Key.Q),
                1,
                5,
                1);

            // Setup camera.
            var camera = cameraFactory.CreateCamera(viewportSize.Width, viewportSize.Height);
            this._cameraController = cameraFactory.CreateCameraController(camera);
            this._cameraController.AddEffect(positionCameraEffect);
            this._cameraController.AddEffect(scaleCameraEffect);

            // Initialize world.
            this.PopulateWorld();
        }

        /// <summary>
        /// See <see cref="IUpdateCallback{TGameState}.Update(TimeInfo, TimeInfo, ILoopCommand)"/>.
        /// </summary>
        public DemoGameState Update(
            TimeInfo realTime,
            TimeInfo gameTime,
            ILoopCommand loopCommand)
        {
            Checks.AssertNotNull(realTime, nameof(realTime));
            Checks.AssertNotNull(gameTime, nameof(gameTime));
            Checks.AssertNotNull(loopCommand, nameof(loopCommand));

            this._inputActionManager.Update(realTime.Elapsed);
            this._cameraController.Update(realTime.Elapsed);

            // Follow cam.
            if (this._actionFollowCam.IsActive)
            {
                this.ActivateFollowCam();
            }

            // Spawn cars.
            if (this._actionSpawnCarLeft.IsActive)
            {
                this.SpawnCarLeft();
            }

            if (this._actionSpawnCarRight.IsActive)
            {
                this.SpawnCarRight();
            }

            // Shoot.
            if (this._actionShoot.IsActive)
            {
                var position = this._cameraController.Camera.State.Position;
                this.SpawnExplosion(position, 50);
            }

            // Remove cars.
            var carsOutOfRange = this._humbleCars.Where(car => Math.Abs(car.Position.X) > 200).ToList();
            foreach (var carToRemove in carsOutOfRange)
            {
                this._humbleCars.Remove(carToRemove);
            }

            // Update cars.
            foreach (var car in this._humbleCars)
            {
                car.Update(gameTime.Elapsed);
            }

            // Update explosions.
            var currentExplosions = this._explosions.ToList();
            foreach (var explosion in currentExplosions)
            {
                explosion.Update(gameTime.Elapsed);

                // Test against each (undestroyed) car, and destroy them if hit.
                if (explosion.IsFinished)
                {
                    continue;
                }

                var undestroyedCars = this._humbleCars.Where(car => !car.IsDestroyed).ToList();
                foreach (var car in undestroyedCars)
                {
                    var result = this._gjk.DoFiguresIntersect(explosion.Circle, car.Polygon);
                    if (result.DoFiguresIntersect)
                    {
                        car.Destroy();
                        SpawnExplosion(car.Position, 30);
                    }
                }
            }

            // Stop the game.
            if (this._actionEndGame.IsActive)
            {
                loopCommand.Stop();
            }

            return new DemoGameState(
                this._cameraController.Camera.GetCameraTransformation(),
                this._humbleBuildings,
                this._humbleCars,
                this._explosions);
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

        /// <summary>
        /// Spawns a car on the left, driving to the right.
        /// </summary>
        private void SpawnCarLeft()
        {
            var car = new Car(
                Color.GreenYellow,
                new Point(-200, -5),
                false);

            this._humbleCars.Add(car);
        }

        /// <summary>
        /// Spawns a car on the right, driving to the left.
        /// </summary>
        private void SpawnCarRight()
        {
            var car = new Car(
                Color.GreenYellow,
                new Point(200, 5),
                true);

            this._humbleCars.Add(car);
        }

        /// <summary>
        /// Activates the "follow cam". Makes the camera follow the last car which was spawned.
        /// </summary>
        private void ActivateFollowCam()
        {
            if (!this._humbleCars.Any())
            {
                return;
            }

            var car = this._humbleCars.Last();
            var followEffect = this._cameraEffectCreator.CreatePositionFollowEffect(
                car,
                x => x.Position,
                () => !this._humbleCars.Contains(car));

            this._cameraController.AddEffect(followEffect);
        }

        /// <summary>
        /// Spawns an explosion.
        /// </summary>
        private void SpawnExplosion(Point position, double explosionSize)
        {
            var explosion = new Explosion(position, explosionSize);

            // Make camera rattle.
            var cameraRattleEffect = this._cameraEffectCreator.CreatePositionBlowOscillationEffect(
                new Vector2(explosionSize / 3, explosionSize / 4),
                1,
                8);

            this._cameraController.AddEffect(cameraRattleEffect);

            this._explosions.Add(explosion);
        }
    }
}

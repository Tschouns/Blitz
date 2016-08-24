//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx.PrototypeRenderLoopDemo
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Input;
    using Base.RuntimeChecks;
    using Camera;
    using Geometry.Elements;
    using HumbleWorldObjects;
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
        /// The camera controller, used control the camera by keyboard.
        /// </summary>
        private readonly ICameraController _cameraController;

        /// <summary>
        /// Stores all the buildings of this fairly humble game world.
        /// </summary>
        private readonly IList<Building> _humbleBuildings = new List<Building>();

        /// <summary>
        /// Stores all the cars of this fairly humble game world.
        /// </summary>
        private readonly IList<Car> _humbleCars = new List<Car>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGameLogic"/> class.
        /// </summary>
        public DemoGameLogic(
            IInputFactory inputFactory,
            ICameraFactory cameraFactory,
            Size viewportSize)
        {
            Checks.AssertNotNull(inputFactory, nameof(inputFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));

            // Setup input.
            var button = inputFactory.KeyboardButtonCreator;

            this._inputActionManager = inputFactory.CreateInputActionManager();
            this._actionSpawnCarLeft = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.NumPad4));
            this._actionSpawnCarRight = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.NumPad6));
            this._actionEndGame = this._inputActionManager.RegisterButtonHitAction(button.Create(Key.Escape));

            var positionCameraEffect = cameraFactory.CameraEffectCreator.CreatePositionByButtonsEffect(
                this._inputActionManager,
                button.Create(Key.W),
                button.Create(Key.S),
                button.Create(Key.A),
                button.Create(Key.D),
                50);

            var scaleCameraEffect = cameraFactory.CameraEffectCreator.CreateScaleExponentialByButtonsEffect(
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

            // Spawn cars.
            if (this._actionSpawnCarLeft.IsActive)
            {
                this.SpawnCarLeft();
            }

            if (this._actionSpawnCarRight.IsActive)
            {
                this.SpawnCarRight();
            }

            // Remove cars.
            var carsOutOfRange = this._humbleCars.Where(aX => Math.Abs(aX.Position.X) > 200).ToList();
            foreach (var carToRemove in carsOutOfRange)
            {
                this._humbleCars.Remove(carToRemove);
            }

            // Update cars.
            foreach (var car in this._humbleCars)
            {
                car.Update(gameTime.Elapsed);
            }

            // Stop the game.
            if (this._actionEndGame.IsActive)
            {
                loopCommand.Stop();
            }

            return new DemoGameState(
                this._cameraController.Camera.GetCameraTransformation(),
                this._humbleBuildings,
                this._humbleCars);
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
    }
}

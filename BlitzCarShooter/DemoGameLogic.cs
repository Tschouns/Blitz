//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter
{
    using System;
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
    using Physics.World;
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
        /// The humble game world.
        /// </summary>
        private readonly World _humbleWorld;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGameLogic"/> class.
        /// </summary>
        public DemoGameLogic(
            IInputFactory inputFactory,
            ICameraFactory cameraFactory,
            IPhysicsFactory physicsFactory,
            IGjkAlgorithm<Circle, Polygon> gjk,
            Size viewportSize)
        {
            Checks.AssertNotNull(inputFactory, nameof(inputFactory));
            Checks.AssertNotNull(cameraFactory, nameof(cameraFactory));
            Checks.AssertNotNull(physicsFactory, nameof(physicsFactory));
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

            var rotateCameraEffect = this._cameraEffectCreator.CreateRotationByButtonsEffect(
                this._inputActionManager,
                button.Create(Key.Q),
                button.Create(Key.E),
                Math.PI/5);

            var scaleCameraEffect = this._cameraEffectCreator.CreateScaleExponentialByButtonsEffect(
                this._inputActionManager,
                button.Create(Key.X),
                button.Create(Key.Y),
                1,
                10,
                1);

            // Setup camera.
            var camera = cameraFactory.CreateCamera(viewportSize.Width, viewportSize.Height);
            this._cameraController = cameraFactory.CreateCameraController(camera);
            this._cameraController.AddEffect(positionCameraEffect);
            this._cameraController.AddEffect(rotateCameraEffect);
            this._cameraController.AddEffect(scaleCameraEffect);

            // Create world.
            this._humbleWorld = new World(physicsFactory);
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
                this._humbleWorld.SpawnCarLeft();
            }

            if (this._actionSpawnCarRight.IsActive)
            {
                this._humbleWorld.SpawnCarRight();
            }

            // Shoot.
            if (this._actionShoot.IsActive)
            {
                var position = this._cameraController.Camera.State.Position;
                this.SpawnExplosion(position, 50, 100000);
            }

            // Update world.
            this._humbleWorld.Update(gameTime.Elapsed);

            // Destroy cars by explosions.
            var activeExplosions = this._humbleWorld.Expolosions.Where(x => !x.IsFinished).ToList();
            var aliveCars = this._humbleWorld.Cars.Where(x => !x.IsDestroyed).ToList();

            foreach (var explosion in activeExplosions)
            {
                foreach (var car in aliveCars)
                {
                    var result = this._gjk.DoFiguresIntersect(explosion.Circle, car.Polygon);
                    if (result.DoFiguresIntersect)
                    {
                        car.Destroy();
                        ////SpawnExplosion(car.Position, 30, 10000);
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
                this._humbleWorld.Buildings,
                this._humbleWorld.Cars,
                this._humbleWorld.Expolosions);
        }

        /// <summary>
        /// Activates the "follow cam". Makes the camera follow the last car which was spawned.
        /// </summary>
        private void ActivateFollowCam()
        {
            if (!this._humbleWorld.Cars.Any())
            {
                return;
            }

            var car = this._humbleWorld.Cars.Last();
            var followEffect = this._cameraEffectCreator.CreatePositionFollowEffect(
                car,
                x => x.Position,
                () => !this._humbleWorld.Cars.Contains(car) || car.IsDestroyed);

            this._cameraController.AddEffect(followEffect);
        }

        /// <summary>
        /// Spawns an explosion.
        /// </summary>
        private void SpawnExplosion(Point position, double explosionRadius, double explosionForce)
        {
            this._humbleWorld.SpawnExplosion(position, explosionRadius, explosionForce);

            // Make camera rattle.
            var cameraRattleEffect = this._cameraEffectCreator.CreatePositionBlowOscillationEffect(
                new Vector2(explosionRadius / 3, explosionRadius / 4),
                1,
                8);

            this._cameraController.AddEffect(cameraRattleEffect);
        }
    }
}

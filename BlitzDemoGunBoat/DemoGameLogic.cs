//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDemoGunBoat
{
    using System.Drawing;
    using Base.RuntimeChecks;
    using Camera;
    using Camera.CameraEffects;
    using Geometry.Algorithms.Gjk;
    using Geometry.Elements;
    using Input;
    using Input.InputAction;
    using Physics.World;
    using RenderLoop;
    using RenderLoop.Callback;
    using RenderLoop.Loop;

    /// <summary>
    /// Represents the "game logic", besides drawing, for this demo. Implements <see cref="IUpdateCallback{TGameState}"/>.
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
        /// Initializes a new instance of the <see cref="DemoGameLogic"/> class.
        /// </summary>
        public DemoGameLogic(
            IInputFactory inputFactory,
            ICameraFactory cameraFactory,
            IPhysicsFactory physicsFactory,
            IGjkAlgorithm<Circle, Polygon> gjk,
            Size viewportSize)
        {
            ArgumentChecks.AssertNotNull(inputFactory, nameof(inputFactory));
            ArgumentChecks.AssertNotNull(cameraFactory, nameof(cameraFactory));
            ArgumentChecks.AssertNotNull(physicsFactory, nameof(physicsFactory));
            ArgumentChecks.AssertNotNull(gjk, nameof(gjk));

            var camera = cameraFactory.CreateCamera(viewportSize.Width, viewportSize.Height);
            this._cameraController = cameraFactory.CreateCameraController(camera);
        }

        /// <summary>
        /// See <see cref="IUpdateCallback{TGameState}.Update(TimeInfo, TimeInfo, ILoopCommand)"/>.
        /// </summary>
        public DemoGameState Update(
            TimeInfo realTime,
            TimeInfo gameTime,
            ILoopCommand loopCommand)
        {
            var cameraTransformation = this._cameraController.Camera.GetCameraTransformation();
            return new DemoGameState(cameraTransformation);
        }
    }
}

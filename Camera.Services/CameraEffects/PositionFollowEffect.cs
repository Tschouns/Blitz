//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services.CameraEffects
{
    using System;
    using Base.RuntimeChecks;
    using CameraEffects;
    using Geometry.Elements;
    using global::Camera.CameraEffects;

    /// <summary>
    /// A camera effect which makes the camera follow an object or character in the world.
    /// </summary>
    /// <typeparam name="TFollowed">
    /// The type of the object the camera is supposed to follow
    /// </typeparam>
    public class PositionFollowEffect<TFollowed> : ICameraEffect
        where TFollowed : class
    {
        /// <summary>
        /// The object that the camera should follow.
        /// </summary>
        private readonly TFollowed _followedObject;

        /// <summary>
        /// The function used to retrieve the current position followed object.
        /// </summary>
        private readonly Func<TFollowed, Point> _retrieveCurrentFollowedObjectPositionFunc;

        /// <summary>
        /// Determines whether the effect has expired. TODO: rethink this design...
        /// </summary>
        private readonly Func<bool> _determineIsExpiredFunc;

        /// <summary>
        /// The position of the followed object.
        /// </summary>
        private Point _followedPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionFollowEffect{TFollowed}"/> class.
        /// </summary>
        public PositionFollowEffect(
            TFollowed followedObject,
            Func<TFollowed, Point> retrieveCurrentFollowedObjectPositionFunc,
            Func<bool> determineIsExpiredFunc)
        {
            Checks.AssertNotNull(followedObject, nameof(followedObject));
            Checks.AssertNotNull(retrieveCurrentFollowedObjectPositionFunc, nameof(retrieveCurrentFollowedObjectPositionFunc));
            Checks.AssertNotNull(determineIsExpiredFunc, nameof(determineIsExpiredFunc));

            this._followedObject = followedObject;
            this._retrieveCurrentFollowedObjectPositionFunc = retrieveCurrentFollowedObjectPositionFunc;
            this._determineIsExpiredFunc = determineIsExpiredFunc;
        }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>. This effect does not expire.
        /// </summary>
        public bool HasExpired => this._determineIsExpiredFunc();

        /// <summary>
        /// See <see cref="ICameraEffect.Update(double)"/>.
        /// </summary>
        public void Update(double timeElapsed)
        {
            this._followedPosition = this._retrieveCurrentFollowedObjectPositionFunc(this._followedObject);
        }

        /// <summary>
        /// See <see cref="ICameraEffect.ApplyToCamera(double)"/>.
        /// </summary>
        public void ApplyToCamera(ICamera camera)
        {
            Checks.AssertNotNull(camera, nameof(camera));

            // Cheap first draft - TODO: finish...
            // Temporary hack - TODO: redesign, so an effect does not know the camera, but produces only an "offset".
            var state = new CameraState()
            {
                Position = this._followedPosition,
                Orientation = camera.State.Orientation,
                Scale = camera.State.Scale
            };

            camera.State = state;
        }
    }
}

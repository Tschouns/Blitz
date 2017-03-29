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
    using Geometry.Extensions;
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
        /// Initializes a new instance of the <see cref="PositionFollowEffect{TFollowed}"/> class.
        /// </summary>
        public PositionFollowEffect(
            TFollowed followedObject,
            Func<TFollowed, Point> retrieveCurrentFollowedObjectPositionFunc,
            Func<bool> determineIsExpiredFunc)
        {
            ArgumentChecks.AssertNotNull(followedObject, nameof(followedObject));
            ArgumentChecks.AssertNotNull(retrieveCurrentFollowedObjectPositionFunc, nameof(retrieveCurrentFollowedObjectPositionFunc));
            ArgumentChecks.AssertNotNull(determineIsExpiredFunc, nameof(determineIsExpiredFunc));

            this._followedObject = followedObject;
            this._retrieveCurrentFollowedObjectPositionFunc = retrieveCurrentFollowedObjectPositionFunc;
            this._determineIsExpiredFunc = determineIsExpiredFunc;
        }

        /// <summary>
        /// See <see cref="ICameraEffect.HasExpired"/>. This effect does not expire.
        /// </summary>
        public bool HasExpired => this._determineIsExpiredFunc();

        /// <summary>
        /// See <see cref="ICameraEffect.GetCameraOffset(CameraState, double)"/>.
        /// </summary>
        public CameraOffset GetCameraOffset(CameraState cameraState, double timeElapsed)
        {
            var followedPosition = this._retrieveCurrentFollowedObjectPositionFunc(this._followedObject);
            var positionOffset = followedPosition.GetOffsetFrom(cameraState.Position);

            return new CameraOffset()
            {
                PositionOffset = positionOffset
            };
        }
    }
}

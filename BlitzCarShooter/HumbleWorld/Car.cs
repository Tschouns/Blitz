﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter.HumbleWorld
{
    using System.Drawing;
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;
    using Point = Geometry.Elements.Point;
    using Rectangle = Geometry.Elements.Rectangle;
    using Physics.World;
    using System;

    /// <summary>
    /// A humble little car, which can only drive straight from left to right, or from right to left.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// The moving speed of the car.
        /// </summary>
        private readonly double _movingSpeed = 15.0;

        /// <summary>
        /// A value indicating whether the car is moving from right to left. If false, it is moving from left to right.
        /// </summary>
        private readonly bool _isMovingFromRightToLeft;

        /// <summary>
        /// The "physical" car body.
        /// </summary>
        private readonly IBody<Polygon> _carBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car(
            IPhysicalWorld physicalWorld,
            Color color,
            Point startingPosition,
            bool isMovingFromRightToLeft)
        {
            ArgumentChecks.AssertNotNull(physicalWorld, nameof(physicalWorld));

            double length = 7.0f;
            double width = 4.0;
            var carShape = new Rectangle(length, width);

            this._carBody = physicalWorld.SpawnRigidBody(
                1000,
                carShape,
                startingPosition);

            if (isMovingFromRightToLeft)
            {
                // Turn the car 180 degree.
                this._carBody.SetOrientation(Math.PI);
            }

            this.Color = color;
            this.TimeSinceDestroyed = 0.0;
            this._isMovingFromRightToLeft = isMovingFromRightToLeft;
        }

        /// <summary>
        /// Gets the current position of the car.
        /// </summary>
        public Point Position => this._carBody.CurrentState.Position;

        /// <summary>
        /// Gets the current orientation of the car.
        /// </summary>
        public double Orientation => this._carBody.CurrentState.Orientation;

        /// <summary>
        /// Gets the polygon representing the car in its current position.
        /// </summary>
        public Polygon Polygon => this._carBody.Shape.Current;

        /// <summary>
        /// Gets the color of the building.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets a value Indicating whether the car is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// Gets the time since the car was destroyed.
        /// </summary>
        public double TimeSinceDestroyed { get; private set; }

        /// <summary>
        /// Updates the car.
        /// </summary>
        public void Update(double elapsedTime)
        {
            if (this.IsDestroyed)
            {
                this.TimeSinceDestroyed += elapsedTime;
                return;
            }

            // Move the car.
            if (Math.Abs(this._carBody.CurrentState.Velocity.X) < this._movingSpeed)
            {
                var acceleration = 3.0;
                if (this._isMovingFromRightToLeft)
                {
                    acceleration = -acceleration;
                }

                this._carBody.ApplyAcceleration(new Vector2(acceleration, 0));
            }

            ////var speed = this._movingSpeed;
            ////if (this._isMovingFromRightToLeft)
            ////{
            ////    speed = -speed;
            ////}

            ////this._carBody.SetVelocity(new Vector2(speed, 0));
        }

        /// <summary>
        /// Destroys the car. It will no longer move.
        /// </summary>
        public void Destroy()
        {
            this.IsDestroyed = true;
            this.Color = Color.DarkRed;
        }
    }
}

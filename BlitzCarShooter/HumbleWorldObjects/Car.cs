//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter.HumbleWorldObjects
{
    using System.Drawing;
    using Geometry.Elements;
    using Point = Geometry.Elements.Point;
    using Rectangle = Geometry.Elements.Rectangle;

    /// <summary>
    /// A humble little car, which can only drive straight from left to right, or from right to left.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// The lenght of the car.
        /// </summary>
        private readonly double _length = 7.0;

        /// <summary>
        /// The width of the car (not the rectangle representing it).
        /// </summary>
        private readonly double _width = 4.0;

        /// <summary>
        /// The moving speed of the car.
        /// </summary>
        private readonly double _movingSpeed = 15.0;

        /// <summary>
        /// A value indicating whether the car is moving from right to left. If false, it is moving from left to right.
        /// </summary>
        private readonly bool _isMovingFromRightToLeft;

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car(
            Color color,
            Point startingPosition,
            bool isMovingFromRightToLeft)
        {
            this.Color = color;
            this.Position = startingPosition;
            this._isMovingFromRightToLeft = isMovingFromRightToLeft;

            this.SetRectangle();
        }

        /// <summary>
        /// Gets the current position of the car.
        /// </summary>
        public Point Position { get; private set; }

        /// <summary>
        /// Gets the polygon representing the car in its current position.
        /// </summary>
        public Polygon Polygon { get; private set; }

        /// <summary>
        /// Gets the color of the building.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        ///Gets a value Indicating whether the car is destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// Updates the car.
        /// </summary>
        public void Update(double elapsedTime)
        {
            if (this.IsDestroyed)
            {
                return;
            }

            // Move the car.
            var movingDistance = this._movingSpeed * elapsedTime;

            if (this._isMovingFromRightToLeft)
            {
                movingDistance = -movingDistance;
            }

            this.Position = new Point(
                this.Position.X + movingDistance,
                this.Position.Y);

            // Set the rectangle.
            this.SetRectangle();
        }

        /// <summary>
        /// Destroys the car. It will no longer move.
        /// </summary>
        public void Destroy()
        {
            this.IsDestroyed = true;
            this.Color = Color.DarkRed;
        }

        /// <summary>
        /// Sets the rectangle, as the car's representation.
        /// </summary>
        private void SetRectangle()
        {
            var rectanglePosition = new Point(
                this.Position.X - (this._length / 2),
                this.Position.Y - (this._width / 2));

            this.Polygon = new Rectangle(
                rectanglePosition,
                this._length,
                this._width);
        }
    }
}

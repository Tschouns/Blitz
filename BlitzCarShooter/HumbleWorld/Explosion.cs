//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzCarShooter.HumbleWorld
{
    using System.Drawing;
    using Geometry.Elements;
    using Point = Geometry.Elements.Point;
    using Rectangle = Geometry.Elements.Rectangle;

    /// <summary>
    /// Represents an explosion as caused by shooting stuff.
    /// </summary>
    public class Explosion
    {
        /// <summary>
        /// The speed by which the expansion radius is increased.
        /// </summary>
        private readonly double _expansionSpeed = 150.0;

        /// <summary>
        /// The maximum expansion radius.
        /// </summary>
        private readonly double _maxExpansionRadius;

        /// <summary>
        /// The current expansion radius of the explosion.
        /// </summary>
        private double _currentExpansionRadius = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Explosion"/> class.
        /// </summary>
        public Explosion(Point position, double expansionRadius)
        {
            this.Position = position;
            this._maxExpansionRadius = expansionRadius;

            this.Color = Color.OrangeRed;

            this.Circle = new Circle(this.Position, this._currentExpansionRadius);
        }

        /// <summary>
        /// Gets the position of the explosion.
        /// </summary>
        public Point Position { get; }

        /// <summary>
        /// Gets the circle representing the car the explosion in its current position and state.
        /// </summary>
        public Circle Circle { get; private set; }

        /// <summary>
        /// Gets the color of the explosion.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the explosion is already finished, in which case, for simplicity sake, it becomes a crater;)
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        /// Updates the explosion.
        /// </summary>
        public void Update(double elapsedTime)
        {
            if (this.IsFinished)
            {
                return;
            }

            var expansionIncrement = this._expansionSpeed * elapsedTime;
            this._currentExpansionRadius += expansionIncrement;

            // "Finish" the explosion, and turn it into a crater.
            if (this._currentExpansionRadius > this._maxExpansionRadius)
            {
                this.IsFinished = true;
                this._currentExpansionRadius = 5.0;
                this.Color = Color.DarkSlateGray;
            }
            
            // Set the circle.
            this.Circle = new Circle(this.Position, this._currentExpansionRadius);
        }
    }
}

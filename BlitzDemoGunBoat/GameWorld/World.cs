//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDemoGunBoat.GameWorld
{
    using Base.RuntimeChecks;
    using BlitzDemoGunBoat.Level;
    using Geometry.Elements;

    public class World
    {
        /// <summary>
        /// 
        /// </summary>
        public World(LevelData level)
        {
            ArgumentChecks.AssertNotNull(level, nameof(level));
        }

        public Rectangle WorldBounds { get; }

        public Polygon Lake { get; }
    }
}

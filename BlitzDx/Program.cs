//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlitzDx
{
    using System;
    using Base.StartUp;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The famous "main".
        /// </summary>
        [STAThread]
        public static void Main()
        {
            StartUp.InitializeComponents();

            using (var sim = new PrototypeDx2DShapesDemo())
            {
                sim.Run();
            }
        }
    }
}

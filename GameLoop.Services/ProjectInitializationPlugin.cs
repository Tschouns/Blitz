﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace RenderLoop.Services
{
    using Base.InversionOfControl;
    using Base.StartUp;
    using RenderLoop.Loop;
    using Loop;

    /// <summary>
    /// Initializes this project, see <see cref="IProjectInitializationPlugin"/>.
    /// </summary>
    [ProjectInitializationPlugin]
    public class ProjectInitializationPlugin : IProjectInitializationPlugin
    {
        /// <summary>
        /// See <see cref="IProjectInitializationPlugin.PerformIocContainerRegistrations"/>.
        /// </summary>
        public void PerformIocContainerRegistrations()
        {
            Ioc.Container.RegisterSingleton<ILoopFactory, LoopFactory>();
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx
{
    using Base.InversionOfControl;
    using Base.StartUp;
    using Display;
    using Sprites;

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
            Ioc.Container.RegisterSingleton<IBitmapLoader, BitmapLoader>();
            Ioc.Container.RegisterSingleton<IDisplayFactory, DisplayFactory>();
        }
    }
}

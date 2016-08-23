//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Camera.Services
{
    using Base.InversionOfControl;
    using Base.StartUp;
    using CameraEffects;
    using global::Camera.CameraEffects;

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
            Ioc.Container.RegisterSingleton<ICameraEffectHelper, CameraEffectHelper>();
            Ioc.Container.RegisterSingleton<ICameraEffectCreator, CameraEffectCreator>();
            Ioc.Container.RegisterSingleton<ICameraFactory, CameraFactory>();
        }
    }
}

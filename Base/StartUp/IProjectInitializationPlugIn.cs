//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.StartUp
{
    /// <summary>
    /// Provides an plug-in interface for initializing a project when starting up the app.
    /// </summary>
    public interface IProjectInitializationPlugin
    {
        /// <summary>
        /// Performs all the necessary IOC container registrations for the project.
        /// </summary>
        void PerformIocContainerRegistrations();
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.StartUp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Base.Extensions;

    /// <summary>
    /// Starts up the application and initializes all the projects.
    /// </summary>
    public static class StartUp
    {
        /// <summary>
        /// Initializes all components within the app.
        /// </summary>
        public static void Initialize()
        {
            var projectInitializationPlugins = GetProjectInitializationPlugins();

            foreach (var plugin in projectInitializationPlugins)
            {
                plugin.PerformIocContainerRegistrations();
            }
        }

        /// <summary>
        /// Gets all the designated "project initialization plug-ins" within the app.
        /// </summary>
        private static IEnumerable<IProjectInitializationPlugin> GetProjectInitializationPlugins()
        {
            // Get all assemblies.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Get alls assemblies' initialization plug-in classes.
            IList<Type> projectInitializationPluginTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var initializationPluginTypesInCurrentAssemply = assembly.DefinedTypes
                    .Where(aX => aX.IsClass)
                    .Where(aX => typeof(IProjectInitializationPlugin).IsAssignableFrom(aX))
                    .Where(aX => aX.GetCustomAttribute<ProjectInitializationPluginAttribute>() != null)
                    .ToList();

                projectInitializationPluginTypes.AddElements(initializationPluginTypesInCurrentAssemply);
            }

            // Instanciate initialization plug-in classes.
            IList<IProjectInitializationPlugin> projectInitializationPlugins = new List<IProjectInitializationPlugin>();

            foreach (var initializationPluginType in projectInitializationPluginTypes)
            {
                var parameterlessConstructor = initializationPluginType.GetConstructor(Type.EmptyTypes);

                if (parameterlessConstructor == null)
                {
                    throw new StartUpException($"The initialization plug-in class {initializationPluginType.FullName} must have a parameter-less constructor.");
                }

                var plugin = (IProjectInitializationPlugin)Activator.CreateInstance(initializationPluginType);

                projectInitializationPlugins.Add(plugin);
            }

            return projectInitializationPlugins;
        }
    }
}

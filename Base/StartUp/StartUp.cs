//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.StartUp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Extensions;
    using RuntimeChecks;

    /// <summary>
    /// Starts up the application and initializes all the projects.
    /// </summary>
    public static class StartUp
    {
        /// <summary>
        /// Initializes all components within the app.
        /// </summary>
        public static void InitializeComponents()
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
            var assemblies = LoadAllAssemblies();

            // Get alls assemblies' initialization plug-in classes.
            IList<Type> projectInitializationPluginTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {               
                var types = assembly.DefinedTypes;
                var classes = types.Where(x => x.IsClass).ToList();
                var pluginInterfaceClasses = classes.Where(x => typeof(IProjectInitializationPlugin).IsAssignableFrom(x)).ToList();
                var pluginAttributeDesignatedClasse = pluginInterfaceClasses.Where(x => x.GetCustomAttribute<ProjectInitializationPluginAttribute>() != null).ToList();

                projectInitializationPluginTypes.AddElements(pluginAttributeDesignatedClasse);
            }

            // Instanciate initialization plug-in classes.
            IList<IProjectInitializationPlugin> projectInitializationPlugins = new List<IProjectInitializationPlugin>();

            foreach (var initializationPluginType in projectInitializationPluginTypes)
            {
                var parameterlessConstructor = initializationPluginType.GetConstructor(Type.EmptyTypes);

                if (parameterlessConstructor == null)
                {
                    throw new StartUpException($"The initialization plug-in class {initializationPluginType.FullName} must have a public parameter-less constructor.");
                }

                var plugin = (IProjectInitializationPlugin)Activator.CreateInstance(initializationPluginType);

                projectInitializationPlugins.Add(plugin);
            }

            return projectInitializationPlugins;
        }

        /// <summary>
        /// Loads and returns all references assemblies.
        /// </summary>
        private static IEnumerable<Assembly> LoadAllAssemblies()
        {
            // Get already assemblies, create a list.
            IList<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            // Load all assemblies (in the base dir) that are not yet loaded.
            var loadedPaths = loadedAssemblies.Select(x => x.Location).ToList();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var pathsToLoad = referencedPaths.Where(x => !loadedPaths.Contains(x, StringComparer.InvariantCultureIgnoreCase)).ToList();

            foreach (var path in pathsToLoad)
            {
                try
                {
                    var loadedAssembly = AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path));

                    // Add each additionally loaded assembly to the list.
                    loadedAssemblies.Add(loadedAssembly);
                }
                catch (BadImageFormatException ex)
                {
                    ArgumentChecks.AssertNotNull(ex, nameof(ex));

                    // The file apparently isn't a .NET assembly - do nothing.
                }
            }

            return loadedAssemblies;
        }
    }
}

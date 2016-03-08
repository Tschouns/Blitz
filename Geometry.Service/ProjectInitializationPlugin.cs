//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Service
{
    using Base.InversionOfControl;
    using Base.StartUp;
    using Geometry.Helpers;
    using Helpers;

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
            Ioc.Container.RegisterSingleton<ILineIntersectionHelper, LineIntersectionHelper>();
            Ioc.Container.RegisterSingleton<IPointTransformationHelper, PointTransformationHelper>();
            Ioc.Container.RegisterSingleton<IPolygonCalculationHelper, PolygonCalculationHelper>();
        }
    }
}

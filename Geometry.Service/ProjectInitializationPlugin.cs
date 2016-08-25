//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Services
{
    using Algorithms.Gjk;
    using Base.InversionOfControl;
    using Base.StartUp;
    using Elements;
    using Geometry.Algorithms.Gjk;
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
            // Algorithms
            Ioc.Container.RegisterSingleton<ISupportFunctions<Circle>, CircleSupportFunctions>();
            Ioc.Container.RegisterSingleton<ISupportFunctions<Polygon>, PolygonSupportFunctions>();
            Ioc.Container.RegisterSingleton(typeof(IGjkAlgorithm<Polygon, Polygon>), typeof(GjkAlgorithm<Polygon, Polygon>));
            Ioc.Container.RegisterSingleton(typeof(IGjkAlgorithm<Circle, Polygon>), typeof(GjkAlgorithm<Circle, Polygon>));
            //// TODO: check why the hell type unbound registrations don't work.
            ////Ioc.Container.RegisterSingleton(typeof(IGjkAlgorithm<,>), typeof(GjkAlgorithm<,>));

            // Helpers
            Ioc.Container.RegisterSingleton<ILineCalculationHelper, LineCalculationHelper>();
            Ioc.Container.RegisterSingleton<ILineIntersectionHelper, LineIntersectionHelper>();
            Ioc.Container.RegisterSingleton<IPointTransformationHelper, PointTransformationHelper>();
            Ioc.Container.RegisterSingleton<IPolygonCalculationHelper, PolygonCalculationHelper>();
            Ioc.Container.RegisterSingleton<IPolygonTransformationHelper, PolygonTransformationHelper>();
            Ioc.Container.RegisterSingleton<ITriangleCalculationHelper, TriangleCalculationHelper>();
        }
    }
}

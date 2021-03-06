﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Service
{
    using Base.InversionOfControl;
    using Base.StartUp;
    using Elements;
    using Forces;
    using Services.Elements;
    using Services.Elements.Shape;
    using Services.Forces;
    using Services.Helpers;
    using Services.World;
    using World;

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
            Ioc.Container.RegisterSingleton<IBodyCalculationHelper, BodyCalculationHelper>();
            Ioc.Container.RegisterSingleton<IElementFactory, ElementFactory>();
            Ioc.Container.RegisterSingleton<IForceFactory, ForceFactory>();
            Ioc.Container.RegisterSingleton<IIsaacNewtonHelper, IsaacNewtonHelper>();
            Ioc.Container.RegisterSingleton<IPhysicsFactory, PhysicsFactory>();
            Ioc.Container.RegisterSingleton<IShapeFactory, ShapeFactory>();
        }
    }
}

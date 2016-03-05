//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.StartUp
{
    using System;

    /// <summary>
    /// Used to identify <see cref="IProjectInitializationPlugin"/> classes which initialize projects.
    /// </summary>
    public sealed class ProjectInitializationPluginAttribute : Attribute
    {
    }
}

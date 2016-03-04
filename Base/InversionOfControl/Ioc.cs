//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.InversionOfControl
{
    /// <summary>
    /// Provides access to the global <see cref="IIocContainer"/>.
    /// </summary>
    public static class Ioc
    {
        /// <summary>
        /// Stores the global <see cref="IIocContainer"/> instance.
        /// </summary>
        private static readonly IIocContainer IocContainer = new UnityContainerWrapper();

        /// <summary>
        /// Gets the global <see cref="IIocContainer"/>.
        /// </summary>
        public static IIocContainer Container => IocContainer;
    }
}

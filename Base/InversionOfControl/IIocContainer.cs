//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.InversionOfControl
{
    /// <summary>
    /// Represents an IOC container.
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// Registers an singleton implementation for an interface.
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface type
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The implementation type
        /// </typeparam>
        void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : TInterface;

        /// <summary>
        /// Resolves the interface.
        /// </summary>
        TInterface Resolve<TInterface>()
            where TInterface : class;
    }
}

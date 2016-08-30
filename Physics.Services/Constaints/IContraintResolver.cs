//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Constaints
{
    using System.Collections.Generic;

    /// <summary>
    /// Resolves a set of contraints.
    /// </summary>
    public interface IContraintResolver
    {
        /// <summary>
        /// Resolves the specified set of contraints.
        /// </summary>
        void ResolveContraints(IEnumerable<IContraint> contraints);
    }
}

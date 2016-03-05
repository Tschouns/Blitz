//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.Extensions
{
    using System.Collections.Generic;
    using Base.RuntimeChecks;

    /// <summary>
    /// Provides extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds a the specified elements to the list.
        /// </summary>
        public static void AddElements<T>(this IList<T> list, IEnumerable<T> elements)
        {
            Checks.AssertNotNull(list, nameof(list));
            Checks.AssertNotNull(elements, nameof(elements));

            foreach (var element in elements)
            {
                list.Add(element);
            }
        }
    }
}

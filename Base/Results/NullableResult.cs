//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base.Results
{
    /// <summary>
    /// Represents a generic result containing a value of type <typeparamref name="T"/>, which can be <c>null</c>.
    /// It also provides a property <see cref="HasValue"/> to check whether the result actually has a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the result value
    /// </typeparam>
    public class NullableResult<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableResult{T}"/> class. The value this instance
        /// is initialized with can be <c>null</c>.
        /// </summary>
        public NullableResult(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value of type <typeparamref name="T"/>, or <c>null</c>.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets a value indicating whether the result actually has a value of type <typeparamref name="T"/>.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.Value != null;
            }
        }
    }
}

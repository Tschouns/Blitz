//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Elements
{
    /// <summary>
    /// Represents a body in the "physical world".
    /// </summary>
    /// <typeparam name="TShapeFigure">
    /// Type of the geometric figure which represents the shape of the body
    /// </typeparam>
    public interface IBody<TShapeFigure> : IPhysicalObject
    {
        /// <summary>
        /// Gets the inertia.
        /// </summary>
        double Inertia { get; }

        /// <summary>
        /// Gets the shape of the body.
        /// </summary>
        IShape<TShapeFigure> Shape { get; }

        /// <summary>
        /// Gets the current state of the body.
        /// </summary>
        BodyState CurrentState { get; }
    }
}

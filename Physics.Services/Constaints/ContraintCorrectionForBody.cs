//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Constaints
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Represents a correction to apply to a body in order to satisfy a contraint.
    /// </summary>
    public class ContraintCorrectionForBody<TShapeFigure>
        where TShapeFigure : class, IFigure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContraintCorrectionForBody{TShapeFigure}"/> class.
        /// </summary>
        public ContraintCorrectionForBody(
            IBody<TShapeFigure> body,
            Vector2 correctionVelocity,
            double angularCorrectionVelocity)
        {
            ArgumentChecks.AssertNotNull(body, nameof(body));

            this.Body = body;
            this.CorrectionVelocity = correctionVelocity;
            this.AngularCorrectionVelocity = angularCorrectionVelocity;
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        public IBody<TShapeFigure> Body { get; }

        /// <summary>
        /// Gets the correction velocity to add to the body.
        /// </summary>
        public Vector2 CorrectionVelocity { get; }

        /// <summary>
        /// Gets the angular correction velocity to add to the body.
        /// </summary>
        public double AngularCorrectionVelocity { get; }
    }
}

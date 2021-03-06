﻿//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Physics.Services.Constaints
{
    using Geometry.Elements;
    using Physics.Elements;

    /// <summary>
    /// Represents any contraint within the "physical world".
    /// </summary>
    public interface IContraint
    {
        /// <summary>
        /// Evaluates the contraint.
        /// </summary>
        ContraintEvaluationResult Evaluate();
    }
}

//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Display.SharpDx.Extensions
{
    using SharpDX.Mathematics.Interop;
    using System.Numerics;

    /// <summary>
    /// Provides extension methods for <see cref="Matrix3x2"/>.
    /// </summary>
    public static class Matrix3x2Extensions
    {
        public static RawMatrix3x2 ToSharpDxRawMatric3x2(this Matrix3x2 matrix3x2)
        {
            var rawMatrix3x2 = new RawMatrix3x2(
                matrix3x2.M11,
                matrix3x2.M12,
                matrix3x2.M21,
                matrix3x2.M22,
                matrix3x2.M31,
                matrix3x2.M32);

            return rawMatrix3x2;
        }
    }
}

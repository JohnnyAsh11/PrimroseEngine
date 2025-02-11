using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Primrose.Base;
using System.Linq.Expressions;
using System;

namespace Primrose.Base
{
    /// <summary>
    /// Math class that contains Linear Algebra methods.
    /// </summary>
    public static class GraphicMath
    {
        // Methods:

        /// <summary>
        /// Performs Linear Interpolation on 2 locational values.
        /// </summary>
        /// <param name="percent">0-1 percent distance from one location to the next.</param>
        /// <param name="num1">The first location having the value between found.</param>
        /// <param name="num2">The second location having the value between found.</param>
        /// <returns>The Linear Interpolation between the given locations.</returns>
        public static double Lerp(double num1, double num2, double percent)
        {
            return num1 + percent * (num2 - num1);
        }

        /// <summary>
        /// Converts a Vector4 into a Vector3 by removing the W value.
        /// </summary>
        /// <param name="vec4">Vector4 being converted to a Vector3.</param>
        /// <returns>A Vector3 with the Vector4's X, Y and Z values.</returns>
        public static Vector3 ToVector3(Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
        }
        /// <summary>
        /// Converts a Vector2 into a Vector3 by adding 0 for the Z value.
        /// </summary>
        /// <param name="vec2">Vector3 being converted to a Vector3.</param>
        /// <returns>A Vector3 with the Vector2's X and Y values.</returns>
        public static Vector3 ToVector3(Vector2 vec2)
        {
            return new Vector3(vec2.X, vec2.Y, 0.0f);
        }

        /// <summary>
        /// Applies the Rotation Matrix and then the Transformation Matrix to a Vector4.
        /// </summary>
        /// <param name="rotationMatrix">Rotation matrix applied to the Vector4.</param>
        /// <param name="translationMatrix">Transformation matrix applied to the Vector4.</param>
        /// <param name="position">Vector4 position being transformed.</param>
        /// <param name="order">Dictates the order in which the matrices are applied.</param>
        /// <returns>The transformed Vector4 position.</returns>
        public static Vector4 ApplyMatrices(Matrix rotationMatrix, Matrix translationMatrix, Vector4 position, MathOrder order)
        {
            if (order == MathOrder.TranslationFirst)
            {
                position = Vector4.Transform(position, translationMatrix);
                position = Vector4.Transform(position, rotationMatrix);
            }
            else if (order == MathOrder.RotationFirst)
            {
                position = Vector4.Transform(position, rotationMatrix);
                position = Vector4.Transform(position, translationMatrix);
            }

            return position;
        }
    }
}

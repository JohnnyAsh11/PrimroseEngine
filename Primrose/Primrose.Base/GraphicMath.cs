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
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="axisChanges">Specifies the degrees upon which the position is translating.</param>
        /// <returns>The translation matrix.</returns>
        public static Matrix Translate(Vector4 axisChanges)
        {
            // Starting with the identity matrix.
            Matrix translationMatrix = Matrix.Identity;

            // Applying the translation to the X, Y and Z axes.
            translationMatrix.M41 = axisChanges.X;
            translationMatrix.M42 = axisChanges.Y;
            translationMatrix.M43 = axisChanges.Z;

            return translationMatrix;
        }

        /// <summary>
        /// Performs a rotation along the Y axis and returns the resulting Vector3.
        /// </summary>
        /// <param name="position">Position being rotated.</param>
        /// <param name="degrees">Degree upon which we are rotating.</param>
        /// <returns>The resulting position of the rotation.</returns>
        public static void YAxisRotation(ref Vector4 position, float degrees)
        {
            Vector4 row1 = new Vector4((float)Math.Cos(degrees), 0, (float)Math.Sin(degrees), 0);
            Vector4 row2 = new Vector4(0, 1, 0, 0);
            Vector4 row3 = new Vector4(-(float)Math.Sin(degrees), 0, (float)Math.Cos(degrees), 0);
            Vector4 row4 = new Vector4(0, 0, 0, 1);

            Matrix rotationMatrix = new Matrix(
                row1,
                row2,
                row3,
                row4);

            position = Vector4.Transform(position, rotationMatrix);

            //return position;
        }

        /// <summary>
        /// Wraps the Vector4.Transform method within the 
        /// GraphicMath class for simplicity and consistency.
        /// </summary>
        /// <param name="matrix">The rotation matrix being applied to a Vector4.</param>
        /// <param name="position">The Vector4 position.</param>
        /// <returns>The resulting position post rotation.</returns>
        public static Vector4 ApplyRotationMatrix(Matrix matrix, Vector4 position)
        {
            return Vector4.Transform(position, matrix);
        }
    }
}

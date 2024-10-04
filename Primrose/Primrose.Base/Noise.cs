using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Transactions;
using Primrose.Base;

namespace Primrose.Base
{
    /// <summary>
    /// Perlin Noise class for wave like psuedo-random generation
    /// </summary>
    public class Noise
    {

        // Fields:
        private const int _PermutationCount = 255;
        private int[] _permutations;
        private Random _generator;
        private double _x;
        private double _y;

        // Properties: - NONE -

        // Constructors:
        /// <summary>
        /// Default constructor for the Perlin Noise generator.
        /// </summary>
        public Noise()
        {
            int[] halfPermutations = new int[_PermutationCount];
            int counter = 0;
            this._generator = new Random();

            // Setting the initial values of the permutation table.
            for (int i = 0; i < _PermutationCount; i++)
            {
                halfPermutations[i] = i;
            }

            // Shuffling the values within the array.
            for (int i = halfPermutations.Length - 1; i > 0; i--)
            {
                // Calculating a random index.
                int index = (int)(_generator.NextDouble() * (i - 1));

                // Swapping the values with this current index and the random index.
                halfPermutations[i] = halfPermutations[index];
                halfPermutations[index] = halfPermutations[i];
            }

            // Initializing the real permutation table.
            _permutations = new int[_PermutationCount * 2];

            // Copying the values to the real permutation table.
            for (int i = 0; i < _permutations.Length; i++)
            {
                // Checking if the counter has gone out of range.
                if (counter >= _PermutationCount)
                {
                    counter = 0;
                }

                // Assigning the permutation table values.
                _permutations[i] = halfPermutations[counter];

                // Incrementing the counter.
                counter++;
            }

            // Assigning values to the x and y coordinates.
            _x = _generator.NextDouble() * 10;
            _y = _generator.NextDouble() * 10;
        }

        //Methods:
        /// <summary>
        /// Performs the necessary calculations to find the y value in a fade algebraic function.
        /// </summary>
        /// <param name="x">the x value in the function calculation.</param>
        /// <returns>The y value or coordinate at the given x value of the algebraic function.</returns>
        private float Fade(float x)
        {
            // Performing the necessary fade function calculations.
            return ((6 * x - 15) * x + 10) * x * x * x;
        }

        /// <summary>
        /// Identifies a directional vector for the Perlin Noise calculation.
        /// </summary>
        /// <param name="permutationValue">Current value in the permutation table.</param>
        /// <returns>a direction vector.</returns>
        private Vector2 ConstantVector(int permutationValue)
        {
            int randomDirection = permutationValue % 3;
            
            if (randomDirection == 0)
            {
                return new Vector2(1.0f, 1.0f);
            }
            else if (randomDirection == 1)
            {
                return new Vector2(-1.0f, 1.0f);
            }
            else if (randomDirection == 2)
            {
                return new Vector2(1.0f, -1.0f);
            }
            else
            {
                return new Vector2(-1.0f, -1.0f);
            }
        }

        /// <summary>
        /// Calculated the next random value with the Perlin Noise algorithm.
        /// </summary>
        /// <returns>The next value in the Noise generation.</returns>
        public double NextNoise()
        {
            // X and Y byte positions.
            float X = (int)(Math.Floor(_x)) & _PermutationCount;
            float Y = (int)(Math.Floor(_y)) & _PermutationCount;

            // The difference of the x and rounded x.
            float xf = (float)(_x - Math.Floor(_x));

            // The difference of the y and rounded y.
            float yf = (float)(_y - Math.Floor(_y));

            // Calculating the dot product between the selected
            // corners in the Permutation table and corners of the local area.
            double dotTopRight = Vector2.Dot(
                new Vector2(xf - 1.0f, yf - 1.0f),
                ConstantVector(_permutations[(int)(_permutations[(int)(X + 1)] + Y + 1)]));
            double dotTopLeft = Vector2.Dot(
                new Vector2(xf, yf - 1.0f),
                ConstantVector(_permutations[(int)(_permutations[(int)(X)] + Y + 1)]));

            double dotBottomRight = Vector2.Dot(
                new Vector2(xf - 1.0f, yf),
                ConstantVector(_permutations[(int)(_permutations[(int)(X + 1)] + Y)]));
            double dotBottomLeft = Vector2.Dot(
                new Vector2(xf, yf),
                ConstantVector(_permutations[(int)(_permutations[(int)(X)] + Y)]));

            // Calculates the fade value.
            float u = Fade(xf);
            float v = Fade(yf);

            // Incrementing the containing x and y values.
            _x += 0.001f;
            _y += 0.001f;

            // Calculating the result.
            double result = GraphicMath.Lerp(
                u,
                GraphicMath.Lerp(v, dotBottomLeft, dotTopLeft),
                GraphicMath.Lerp(v, dotBottomRight, dotTopRight));

            return result;
        }
    }
}

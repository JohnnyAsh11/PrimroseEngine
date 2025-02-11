using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;
using System.Collections.Generic;
using System;
using Primrose.Interface;
using System.Security.Cryptography;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a VertexBuffer and renders its vertices to the game window.
    /// </summary>
    public class Renderer
    {
        // Fields:
        private List<VertexPositionColor> _vertices;
        private BasicEffect _shader;
        private VertexBuffer _buffer;
        private GraphicsDevice _graphics;
        private List<Line> _lines;

        // Properties:
        /// <summary>
        /// Get property for the Vertex count.
        /// </summary>
        public int Count
        {
            get { return _vertices.Count; }
        }

        /// <summary>
        /// Get property for the vertex list.
        /// </summary>
        public List<VertexPositionColor> Vertices
        {
            get { return _vertices; }
        }

        /// <summary>
        /// Get property for the line list.
        /// </summary>
        public List<Line> Lines
        {
            get { return _lines; }
        }

        // Constructors:
        /// <summary>
        /// Default constructor for the Renderer class.
        /// </summary>
        public Renderer(GraphicsDevice graphics)
        {
            _vertices = new List<VertexPositionColor>();
            _graphics = graphics;
            _shader = new BasicEffect(_graphics);
            _lines = new List<Line>();
        }

        // Public Methods:
        /// <summary>
        /// Adds a Vertex Position Color to the Vertex list.
        /// </summary>
        /// <param name="position">position of the vertex.</param>
        /// <param name="color">color of the vertex.</param>
        public void Add(Vector3 position, Color color)
        {
            _vertices.Add(
                new VertexPositionColor(position, color));
        }

        /// <summary>
        /// Adds a triangle to the renderer's vertex buffer.
        /// </summary>
        /// <param name="p1">The first vertex position of the triangle</param>
        /// <param name="p2">The second vertex position of the triangle</param>
        /// <param name="p3">The third vertex position of the triangle</param>
        /// <param name="color">color of the vertex.</param>
        public void AddTriangle(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Color color)
        {
            // Creating the triangle.
            _vertices.Add(
                new VertexPositionColor(p1, color));
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p3, color));

            _lines.Add(new Line(_graphics, p1, p2));
            _lines.Add(new Line(_graphics, p2, p3));
            _lines.Add(new Line(_graphics, p3, p1));
        }

        /// <summary>
        /// Creates a quad with the 4 given vertices.
        /// </summary>
        /// <param name="p1">The first position of the vertex.</param>
        /// <param name="p2">The second position of the vertex.</param>
        /// <param name="p3">The third position of the vertex.</param>
        /// <param name="p4">The fourth position of the vertex.</param>
        /// <param name="color">The color of the quad.</param>
        public void AddQuad(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector3 p4,
            Color color,
            VertexType vertexType)
        {
            switch (vertexType)
            {
                case VertexType.FilledWireFrame:

                    // Using both filled and wire quad methods.
                    AddFilledQuad(p1, p2, p3, p4, color);
                    AddWireQuad(p1, p2, p3, p4, color);
                    break;
                case VertexType.Filled:

                    // Only adding the filled quads to the buffer
                    AddFilledQuad(p1, p2, p3, p4, color);
                    break;
                case VertexType.WireFrame:

                    // Only adding the wireframe to the buffer.
                    AddWireQuad(p1, p2, p3, p4, color);
                    break;
            }
        }

        /// <summary>
        /// Creates the wire frame around a quad.
        /// </summary>
        private void AddWireQuad(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector3 p4,
            Color color)
        {
            // Creating the wire mesh.
            _lines.Add(new Line(_graphics, p1, p2));
            _lines.Add(new Line(_graphics, p2, p3));
            _lines.Add(new Line(_graphics, p4, p3));
            _lines.Add(new Line(_graphics, p4, p1));
        }

        /// <summary>
        /// Creates the Filled in area of a quad.
        /// </summary>
        private void AddFilledQuad(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector3 p4,
            Color color)
        {
            // First triangle.
            _vertices.Add(
                new VertexPositionColor(p1, color));
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p4, color));

            // Second triangle.
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p3, color));
            _vertices.Add(
                new VertexPositionColor(p4, color));
        }

        /// <summary>
        /// Clears out all 
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
        }
        /// <summary>
        /// Renders the vertex buffer to the screen.  Does not contain color changing logic.
        /// </summary>
        /// <param name="cam">Camera object used for matrices.</param>
        /// <param name="a_m4WorldMatrix">World matrix for the object being rendered.</param>
        public void Draw(Camera cam, Matrix a_m4WorldMatrix)
        {
            // Initializing the Buffer object.
            InitBuffer(_graphics);

            // Setting the BasicEffect shader.
            SetShaderEffects(cam, a_m4WorldMatrix);

            // If the buffer is not null,
            if (_buffer is not null)
            {
                // Actually render everything in the buffer.
                foreach (EffectPass pass in _shader.CurrentTechnique.Passes)
                {
                    // Applying the shader.
                    pass.Apply();

                    // Setting the VertexBuffer.
                    _graphics.SetVertexBuffer(_buffer);

                    // Rendering the Primitive triangles.
                    _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, _buffer.VertexCount / 3);
                }
            }

            // Rendering the lines.
            foreach (Line line in _lines)
            {
                line.Draw(Color.Magenta);
            }
        }

        /// <summary>
        /// Renders the vertex buffer to the screen.
        /// </summary>
        /// <param name="cam">Camera object used for matrices.</param>
        /// <param name="color">Color to render with.</param>
        /// <param name="a_m4WorldMatrix">World matrix of the object being rendered.</param>
        public void Draw(Camera cam, Color color, Matrix a_m4WorldMatrix)
        {
            // Initializing the Buffer object.
            InitBuffer(_graphics);

            // Setting the BasicEffect shader.
            SetShaderEffects(cam, a_m4WorldMatrix);

            // Checking the color of the vertices.
            SetColor(color);

            // If the buffer is not null,
            if (_buffer is not null)
            {
                // Actually render everything in the buffer.
                foreach (EffectPass pass in _shader.CurrentTechnique.Passes)
                {
                    // Applying the shader.
                    pass.Apply();

                    // Setting the VertexBuffer.
                    _graphics.SetVertexBuffer(_buffer);

                    // Rendering the Primitive triangles.
                    _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, _buffer.VertexCount / 3);
                }
            }

            // Rendering the lines.
            foreach (Line line in _lines)
            {
                line.Draw(Color.Magenta);
            }
        }

        // Private Methods:
        /// <summary>
        /// Sets all of the shader's fields.
        /// </summary>
        /// <param name="cam">Camera to pull matrix data from.</param>
        /// <param name="a_m4WorldMatrix">Matrix for the world position of the object.</param>
        private void SetShaderEffects(Camera cam, Matrix a_m4WorldMatrix)
        {
            _shader.VertexColorEnabled = true;
            _shader.View = cam.View;
            _shader.Projection = cam.Projection;
            _shader.World = a_m4WorldMatrix;
        }

        /// <summary>
        /// Creates the VertexBuffer object.
        /// </summary>
        /// <param name="graphics">GraphicsDevices used to create a VertexBuffer.</param>
        private void InitBuffer(GraphicsDevice graphics)
        {
            if (_vertices.Count == 0)
            {
                return;
            }

            // Creating the actual VertexBuffer.
            _buffer = new VertexBuffer(
                graphics,
                VertexPositionColor.VertexDeclaration,
                _vertices.Count,
                BufferUsage.None);

            // Setting the data as an actual buffer array.
            _buffer.SetData<VertexPositionColor>(_vertices.ToArray());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        private void SetColor(Color color)
        {
            // If the color is the same then break from the method.
            if (color == _vertices[0].Color)
            {
                return;
            }

            // Looping through and changing the color of all vertices.
            for (int i = 0; i < _vertices.Count; i++)
            {
                Vector3 position = _vertices[i].Position;
                _vertices[i] = new VertexPositionColor(position, color);
            }
        }

        /// <summary>
        /// Calculates and creates the vertices for a circle mesh.
        /// </summary>
        /// <param name="origin">Origin point of the circle.</param>
        /// <param name="color">Color of the circle being rendered.</param>
        /// <param name="subdivisions">The number of subdivisions for the circle</param>
        /// <param name="radius">Radius of the circle being rendered.</param>
        public void SetCircleVertices(Vector3 origin, Color color, int subdivisions, float radius)
        {
            // Clear the vertices before setting them.
            if (_vertices.Count > 0)
            {
                _vertices.Clear();
            }

            // Clearing the lines before anything else.
            if (_lines.Count > 0)
            {
                _lines.Clear();
            }

            // Making sure that there is a valid number of subdivisions
            if (subdivisions < 4)
            {
                subdivisions = 4;
            }

            // Declaring some general use variables for the next part of the process.
            List<Vector3> tempVertices = new List<Vector3>();
            float deltaAngle = (2.0f * MathHelper.Pi) / subdivisions;
            float xCalc = 0;
            float yCalc = 0;

            // Looping through all of the subdivisions and calculating the circle locations.
            for (int i = 0; i < subdivisions; i++)
            {
                xCalc = (float)Math.Cos(deltaAngle * i) * radius;
                yCalc = (float)Math.Sin(deltaAngle * i) * radius;

                tempVertices.Add(new Vector3(
                        xCalc + origin.X, 
                        yCalc + origin.Y, 
                        0.00f + origin.Z));
            }

            // Adding all of the vertices in the proper order to the _vertices list.
            for (int i = 0; i < subdivisions; i++)
            {
                AddTriangle(
                    origin,
                    tempVertices[i],
                    tempVertices[(i + 1) % subdivisions],
                    color);
            }
        }

        /// <summary>
        /// Creates all of the vertices for rendering a sphere primitive.
        /// </summary>
        /// <param name="origin">The origin position of the Sphere.</param>
        /// <param name="color">The color of the sphere primitive.</param>
        /// <param name="hori_Subdivisions">Subdivsions going along the side view.</param>
        /// <param name="vert_Subdivisions">Subdivisons going along the top down.</param>
        /// <param name="vertexType">The type of data being added into the vertex buffer.</param>
        /// <param name="radius">Radius of the sphere.</param>
        public void SetSphereVertices(Vector3 origin, Color color, int hori_Subdivisions, int vert_Subdivisions, float radius, VertexType vertexType)
        {
            // Clear the vertices before setting them.
            if (_vertices.Count > 0)
            {
                _vertices.Clear();
            }

            // Clearing the lines before anything else.
            if (_lines.Count > 0)
            {
                _lines.Clear();
            }

            // Making sure that there is a valid number of subdivisions
            if (hori_Subdivisions < 4)
            {
                hori_Subdivisions = 4;
            }
            if (vert_Subdivisions < 4)
            {
                vert_Subdivisions = 4;
            }

            // Declaring some general use variables for the next part of the process.
            List<Vector4> tempVertices = new List<Vector4>();
            float deltaAngle = (2.0f * MathHelper.Pi) / hori_Subdivisions;
            float xCalc = 0;
            float yCalc = 0;

            // Looping through all of the subdivisions and calculating the circle locations.
            for (int i = 0; i < hori_Subdivisions; i++)
            {
                // calculating the x/y position based on the current iteration and amount of subdivisions.
                xCalc = (float)Math.Cos(deltaAngle * i) * radius;
                yCalc = (float)Math.Sin(deltaAngle * i) * radius;

                // Creating the vertex.
                Vector4 vertex = new Vector4(
                        xCalc,
                        yCalc,
                        0.00f,
                        1.0f);

                tempVertices.Add(vertex);
            }

            // Adding all of the vertices in the proper order to the _vertices list.
            Matrix previousMatrix = Matrix.Identity;
            Matrix transformMatrix = Matrix.CreateTranslation(origin);
            float rotationDelta = (2.0f * MathHelper.Pi) / vert_Subdivisions;
            for (int i = 0; i < vert_Subdivisions + 1; i++)
            {
                // Creating the current matrix.
                Matrix rotationMatrix = Matrix.CreateRotationY(rotationDelta * i);

                for (int j = 0; j < hori_Subdivisions; j++)
                {
                    // Applying transformations to the sphere vertices.
                    Vector4 p1 = GraphicMath.ApplyMatrices(
                        previousMatrix, 
                        transformMatrix, 
                        tempVertices[(j + 1) % hori_Subdivisions],
                        MathOrder.RotationFirst);
                    Vector4 p2 = GraphicMath.ApplyMatrices(
                        previousMatrix, 
                        transformMatrix, 
                        tempVertices[j],
                        MathOrder.RotationFirst);
                    Vector4 p3 = GraphicMath.ApplyMatrices(
                        rotationMatrix, 
                        transformMatrix, 
                        tempVertices[j],
                        MathOrder.RotationFirst);
                    Vector4 p4 = GraphicMath.ApplyMatrices(
                        rotationMatrix,
                        transformMatrix,
                        tempVertices[(j + 1) % hori_Subdivisions],
                        MathOrder.RotationFirst);

                    // Adding the vertices to the renderer vertex list.
                    AddQuad(
                        GraphicMath.ToVector3(p1),
                        GraphicMath.ToVector3(p2),
                        GraphicMath.ToVector3(p3),
                        GraphicMath.ToVector3(p4),
                        color,
                        vertexType);
                }

                // Setting the previous matrix.
                previousMatrix = rotationMatrix;
            }
        }
    }
}

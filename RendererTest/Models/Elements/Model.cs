using log4net;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RendererTest.Shaders.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Elements.Models
{
    public class Model
    {
        private List<Vector4> vertices;
        private ILog logger = LogManager.GetLogger("Model");
        private int vbo;
        private Matrix4 modelMatrix = Matrix4.Identity;
        float angle = 0.0f;

        public Model()
        {
            vertices = new List<Vector4>();
        }

        public void addFace(Vector4 v1, Vector4 v2, Vector4 v3)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
        }

        public void Commit()
        {
            logger.InfoFormat("Creating model with {0} vertices", vertices.Count);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(BufferTarget.ArrayBuffer, Vector4.SizeInBytes * vertices.Count, vertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexPointer(4, VertexPointerType.Float, Vector4.SizeInBytes, 0);

            logger.Info("Total model size (bytes): " + Vector4.SizeInBytes * vertices.Count);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Update()
        {
            angle++;
            modelMatrix = Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 1.0f, 1.0f), angle);
        }

        public void Render(Matrix4 projViewMatrix,ShaderProgram program)
        {
            Matrix4 MVPmatrix = projViewMatrix * modelMatrix;

            program.Bind();
            program.SetVariable("MVP", MVPmatrix);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);

            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Count); // render the model

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(0);
            program.UnBind();
        }
    }
}

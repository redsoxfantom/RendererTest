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
        private List<Vector3> vertices;
        private ILog logger = LogManager.GetLogger("Model");
        private int vbo;
        private Matrix4 modelMatrix = Matrix4.Identity;
        private Quaternion quat = Quaternion.Identity;
        float angle = 0.0f;
        int numVerts = 0;

        public Model()
        {
            vertices = new List<Vector3>();
        }

        public void addFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 n1, Vector3 n2, Vector3 n3)
        {
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            numVerts += 3;

            vertices.Add(n1);
            vertices.Add(n2);
            vertices.Add(n3);
        }

        public void Commit()
        {
            logger.InfoFormat("Creating model with {0} vertices", numVerts);

            vbo = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector3.SizeInBytes * vertices.Count, vertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            logger.Info("Total model size (bytes): " + Vector3.SizeInBytes * vertices.Count);
        }

        public void Update()
        {
            angle+=0.01f;
            quat = Quaternion.FromEulerAngles(angle, angle, angle);
            modelMatrix = Matrix4.Rotate(quat);
        }

        public void Render(Matrix4 viewProjMatrix,ShaderProgram program)
        {
            Matrix4 MVPmatrix =  modelMatrix * viewProjMatrix;

            program.Bind(); // Set the active shader program
            program.SetVariable("MVP", MVPmatrix); // Send the current Model-View-Projection matrix
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (Vector3.SizeInBytes*6), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, (Vector3.SizeInBytes * 6), Vector3.SizeInBytes*3);

            GL.DrawArrays(PrimitiveType.Triangles, 0, numVerts); // render the model

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            program.UnBind(); // disable the active shader program
        }
    }
}

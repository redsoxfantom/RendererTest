using log4net;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Elements.Models
{
    public class Model
    {
        private List<Vertex> verticies;
        private List<Edge> edges;
        private ILog logger = LogManager.GetLogger("Model");
        private int vbo;

        public Model()
        {
            verticies = new List<Vertex>();
            edges = new List<Edge>();
        }

        public void AddVertex(double inX, double inY, double inZ, double inW = 1.0)
        {
            verticies.Add(new Vertex()
            {
                x = inX,
                y = inY,
                z = inZ,
                w = inW
            });
        }

        public void AddEdge(int vert1, int vert2)
        {
            Vertex v_1 = verticies[vert1];
            Vertex v_2 = verticies[vert2];

            edges.Add(new Edge()
            {
                v1 = v_1,
                v2 = v_2
            });
        }

        public void Commit()
        {
            logger.InfoFormat("Creating model with {0} vertices and {1} edges",verticies.Count,edges.Count);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            // Load the models verticies into a form the buffer can use
            List<Vector4> loadingVertices = new List<Vector4>();
            foreach(Edge e in edges)
            {
                Vertex v1 = e.v1;
                Vertex v2 = e.v2;
                Vector4 vec1 = new Vector4((float)v1.x, (float)v1.y, (float)v1.z, (float)v1.w);
                Vector4 vec2 = new Vector4((float)v2.x, (float)v2.y, (float)v2.z, (float)v2.w);

                loadingVertices.Add(vec1);
                loadingVertices.Add(vec2);
            }

            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, Vector4.SizeInBytes * loadingVertices.Count, loadingVertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexPointer(4, VertexPointerType.Float, Vector4.SizeInBytes, 0);

            logger.Info("Total model size (bytes): " + Vector4.SizeInBytes * loadingVertices.Count);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Render()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.DrawArrays(PrimitiveType.Lines, 0, edges.Count * 2);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}

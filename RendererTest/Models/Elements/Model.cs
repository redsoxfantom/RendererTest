using log4net;
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
        }

        public void Render()
        {

        }
    }
}

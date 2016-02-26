using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Elements.Models
{
    public struct Vertex
    {
        public double x;
        public double y;
        public double z;
        public double w;
    }

    public struct Edge
    {
        public Vertex v1;
        public Vertex v2;
    }
}

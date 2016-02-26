using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Elements.Models
{
    public struct Vertex
    {
        double x;
        double y;
        double z;
        double w;
    }

    public struct Edge
    {
        Vertex v1;
        Vertex v2;
    }
}

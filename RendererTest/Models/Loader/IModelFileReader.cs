using RendererTest.Elements.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Models.Loader
{
    public interface IModelFileReader
    {
        Model LoadFromFile(StreamReader file);
    }
}

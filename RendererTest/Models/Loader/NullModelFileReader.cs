using RendererTest.Elements.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Models.Loader
{
    public class NullModelFileReader : IModelFileReader
    {
        public Model LoadFromFile(FileStream file)
        {
            return new Model();
        }
    }
}

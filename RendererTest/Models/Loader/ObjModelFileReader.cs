using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendererTest.Elements.Models;

namespace RendererTest.Models.Loader
{
    public class ObjModelFileReader : IModelFileReader
    {
        public Model LoadFromFile(FileStream file)
        {
            Model loadedModel = new Model();

            return loadedModel;
        }
    }
}

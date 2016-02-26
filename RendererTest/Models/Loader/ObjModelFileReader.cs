using System.IO;
using RendererTest.Elements.Models;
using log4net;

namespace RendererTest.Models.Loader
{
    public class ObjModelFileReader : IModelFileReader
    {
        private ILog logger = LogManager.GetLogger("ObjModelFileReader");

        public Model LoadFromFile(StreamReader file)
        {
            Model loadedModel = new Model();

            file.

            return loadedModel;
        }
    }
}

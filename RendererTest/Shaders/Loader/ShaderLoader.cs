using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Shaders.Loader
{
    public class ShaderLoader
    {
        private String shaderRoot;

        public ShaderLoader()
        {
            String pathToExe = Assembly.GetExecutingAssembly().Location;
            shaderRoot = Path.GetDirectoryName(pathToExe) + @"\shaders\";
        }
    }
}

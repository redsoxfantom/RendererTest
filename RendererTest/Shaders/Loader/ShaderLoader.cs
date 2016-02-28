using log4net;
using OpenTK.Graphics.OpenGL;
using RendererTest.Shaders.Elements;
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
        private ILog logger = LogManager.GetLogger("ShaderLoader");

        public ShaderLoader()
        {
            String pathToExe = Assembly.GetExecutingAssembly().Location;
            shaderRoot = Path.GetDirectoryName(pathToExe) + @"\shaders\";
        }

        public IShader loadShader(String filename)
        {
            String shaderPath = Path.Combine(shaderRoot, filename);
            logger.Info("Attempting to load shader from " + shaderPath);
            
            try
            {
                String shaderContents = File.ReadAllText(shaderPath);
            }
            catch(Exception ex)
            {
                logger.Error("Error loading shader",ex);
                return new NullShader();
            }

            return new Shader();
        }
    }
}

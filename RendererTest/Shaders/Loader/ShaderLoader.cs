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

            logger.Info("Initializing shader loader with root path " + shaderRoot);
        }

        public ShaderProgram loadShaderProgram(params String[] filenames)
        {
            ShaderProgram program = new ShaderProgram();

            foreach(String filename in filenames)
            {
                loadShader(filename, program);
            }

            return program;
        }

        private void loadShader(string filename, ShaderProgram prog)
        {
            int shaderId = 0;
            String shaderPath = Path.Combine(shaderRoot, filename);
            logger.Info("Attempting to load shader from " + shaderPath);

            try
            {
                String shaderContents = File.ReadAllText(shaderPath);
                ShaderType type = determineShaderType(Path.GetExtension(shaderPath));

                shaderId = GL.CreateShader(type);
                GL.ShaderSource(shaderId, shaderContents);
                GL.CompileShader(shaderId);
                String info = GL.GetShaderInfoLog(shaderId);
                int statusCode;
                GL.GetShader(shaderId, ShaderParameter.CompileStatus, out statusCode);

                if(statusCode != 1)
                {
                    throw new Exception(info);
                }
                else
                {
                    prog.addShader(shaderId, type);
                    logger.Info("Successfully compiled shader " + shaderId);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error loading shader", ex);
                GL.DeleteShader(shaderId);
            }
        }

        private ShaderType determineShaderType(String fileExtension)
        {
            switch(fileExtension)
            {
                case ".vert":
                    return ShaderType.VertexShader;
                case ".frag":
                    return ShaderType.FragmentShader;
                default:
                    throw new Exception("Unrecognized shader file extension " + fileExtension);
            }
        }
    }
}

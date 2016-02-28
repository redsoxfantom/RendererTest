using log4net;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Shaders.Elements
{
    public class ShaderProgram
    {
        private int programId;
        private Dictionary<ShaderType, int> loadedShaders;
        private ILog logger = LogManager.GetLogger("ShaderProgram");

        public ShaderProgram()
        {
            loadedShaders = new Dictionary<ShaderType, int>();
            programId = 0;
        }

        public void addShader(int shaderId, ShaderType type)
        {
            loadedShaders.Add(type, shaderId);

            logger.InfoFormat("Added shader {0} of type {1} to program",shaderId,type);
        }

        public void Commit()
        {
            programId = GL.CreateProgram();

            logger.InfoFormat("Linking {0} shaders into program {1}",loadedShaders.Count,programId);

            foreach(var shaderId in loadedShaders.Values)
            {
                GL.AttachShader(programId, shaderId);
                GL.DeleteShader(shaderId);
            }
            GL.LinkProgram(programId);

            String info = GL.GetProgramInfoLog(programId);
            int statusCode;
            GL.GetProgram(programId, GetProgramParameterName.LinkStatus, out statusCode);
            if(statusCode != 1)
            {
                logger.Error("Failed to link program: "+info);
                GL.DeleteProgram(programId);
                programId = 0;
            }
            else
            {
                logger.InfoFormat("Shader program {0} successfully linked", programId);
            }
        }

        public void Bind()
        {
            if(programId == 0)
            {
                logger.Warn("Shader program could not be bound");
                GL.UseProgram(0);
            }
            else
            {
                GL.UseProgram(programId);
            }
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }

        public void SetVariable(string name, Matrix4 matrix)
        {
            if(programId > 0)
            {
                GL.UseProgram(programId);
                int location = GetVariableLocation(name);
                if(location != -1)
                {
                    GL.UniformMatrix4(location, false, ref matrix);
                }
                GL.UseProgram(0);
            }
        }

        private int GetVariableLocation(string name)
        {
            int location = GL.GetUniformLocation(programId, name);

            if (location != -1)
                logger.Error("Failed to find variable " + name);

            return location;
        }
    }
}

using log4net;
using RendererTest.Elements.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Models.Loader
{
    public class ModelLoader
    {
        private String modelRoot;

        private ILog logger = LogManager.GetLogger("ModelLoader");

        public ModelLoader(String modelRoot = null)
        {
            if(modelRoot != null)
            {
                this.modelRoot = modelRoot;
            }
            else
            {
                String pathToExe = Assembly.GetExecutingAssembly().Location;
                this.modelRoot = Path.GetDirectoryName(pathToExe) + @"\models\";
            }

            logger.Info("Initializing ModelLoader with root path " + this.modelRoot);
        }

        public Model LoadModel(String filename)
        {
            String pathToFile = Path.Combine(modelRoot, filename);
            logger.Info("Searching for model file in " + pathToFile);

            if(!File.Exists(pathToFile))
            {
                logger.Error("File does not exist!");
                return new Model();
            }

            Model loadedModel = null;
            try
            {
                FileStream file = File.OpenRead(pathToFile);
                IModelFileReader fileReader = getModelReader(Path.GetExtension(pathToFile));
                loadedModel = fileReader.LoadFromFile(file);
                loadedModel.Commit();

                logger.Info("Model file loaded using reader " + fileReader.GetType().Name);
            }
            catch(Exception ex)
            {
                logger.Error("Error occured loading model file", ex);
                loadedModel = new Model();
            }
            return loadedModel;
        }

        private IModelFileReader getModelReader(String fileExtension)
        {
            switch(fileExtension)
            {
                case ".obj":
                    return new ObjModelFileReader();
                default:
                    logger.Error("Unrecognized file format " + fileExtension);
                    return new NullModelFileReader();
            }
        }
    }
}

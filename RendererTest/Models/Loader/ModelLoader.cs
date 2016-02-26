﻿using log4net;
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

            Model loadedModel = new Model();
            return loadedModel;
        }
    }
}

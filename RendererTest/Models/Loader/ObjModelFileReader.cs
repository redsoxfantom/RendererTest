using System.IO;
using RendererTest.Elements.Models;
using log4net;
using System;

namespace RendererTest.Models.Loader
{
    public class ObjModelFileReader : IModelFileReader
    {
        Model loadedModel;

        public Model LoadFromFile(StreamReader file)
        {
            loadedModel = new Model();

            while(!file.EndOfStream)
            {
                string line = file.ReadLine();
                if(line.StartsWith("v"))
                {
                    handleAddVertex(line);
                }
                if(line.StartsWith("f"))
                {
                    handleAddFace(line);
                }
            }

            return loadedModel;
        }

        private void handleAddFace(string line)
        {
            string[] splitLine = line.Split(' ');
        }

        private void handleAddVertex(string line)
        {
            // Vertices are defined as follows:
            // 'v' x y z [w]
            string[] splitLine = line.Split(' ');

            double x = double.Parse(splitLine[1]);
            double y = double.Parse(splitLine[2]);
            double z = double.Parse(splitLine[3]);
            double w = 1.0;
            if(splitLine.Length > 4)
            {
                w = double.Parse(splitLine[4]);
            }

            loadedModel.AddVertex(x, y, z, w);
        }
    }
}

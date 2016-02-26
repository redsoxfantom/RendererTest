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
                string line = file.ReadLine().Trim();
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
            // Faces are defined as follows:
            // 'f' v1/[n1]/[t1] ...
            string[] splitLine = line.Split(' ');

            for(int i = 1; i < splitLine.Length - 1; i++)
            {
                string subline = splitLine[i].Split('/')[0];
                string nextSubLine = splitLine[i + 1].Split('/')[0];

                int vert1 = int.Parse(subline);
                int vert2 = int.Parse(nextSubLine);

                loadedModel.AddEdge(vert1, vert2);
            }
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

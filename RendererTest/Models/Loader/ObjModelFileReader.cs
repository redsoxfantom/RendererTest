using System.IO;
using RendererTest.Elements.Models;
using log4net;
using System;
using OpenTK;
using System.Collections.Generic;

namespace RendererTest.Models.Loader
{
    public class ObjModelFileReader : IModelFileReader
    {
        Model loadedModel;
        List<Vector3> vertices;

        public Model LoadFromFile(StreamReader file)
        {
            loadedModel = new Model();
            vertices = new List<Vector3>();

            while(!file.EndOfStream)
            {
                string line = file.ReadLine().Trim();
                if(line.StartsWith("v "))
                {
                    handleAddVertex(line);
                }
                if(line.StartsWith("f "))
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
            int[] vertexIndices = new int[3];

            for(int i = 1; i < splitLine.Length; i++)
            {
                string subline = splitLine[i].Split('/')[0];
                int vertIndex = int.Parse(subline);
                vertexIndices[i - 1] = vertIndex-1;
            }

            Vector3 v1 = vertices[vertexIndices[0]];
            Vector3 v2 = vertices[vertexIndices[1]];
            Vector3 v3 = vertices[vertexIndices[2]];

            loadedModel.addFace(v1, v2, v3);
        }

        private void handleAddVertex(string line)
        {
            // Vertices are defined as follows:
            // 'v' x y z [w]
            string[] splitLine = line.Split(' ');

            double x = double.Parse(splitLine[1]);
            double y = double.Parse(splitLine[2]);
            double z = double.Parse(splitLine[3]);

            vertices.Add(new Vector3((float)x, (float)y, (float)z));
        }
    }
}

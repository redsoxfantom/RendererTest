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
        List<Vector3> normals;

        public Model LoadFromFile(StreamReader file)
        {
            loadedModel = new Model();
            vertices = new List<Vector3>();
            normals = new List<Vector3>();

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
                if(line.StartsWith("vn "))
                {
                    handleAddNormal(line);
                }
            }

            return loadedModel;
        }

        private void handleAddFace(string line)
        {
            // Faces are defined as follows:
            // 'f' v1/[t1]/[n1] ...
            string[] splitLine = line.Split(' ');
            int[] vertexIndices = new int[3];
            int[] normalIndices = new int[3];

            for(int i = 1; i < splitLine.Length; i++)
            {
                string[] subSplitLine = splitLine[i].Split('/');
                int vertIndex = int.Parse(subSplitLine[0]);
                vertexIndices[i - 1] = vertIndex-1;

                int normIndex = int.Parse(subSplitLine[2]);
                normalIndices[i - 1] = normIndex - 1;
            }

            Vector3 v1 = vertices[vertexIndices[0]];
            Vector3 v2 = vertices[vertexIndices[1]];
            Vector3 v3 = vertices[vertexIndices[2]];
            Vector3 n1 = normals[normalIndices[0]];
            Vector3 n2 = normals[normalIndices[1]];
            Vector3 n3 = normals[normalIndices[2]];

            loadedModel.addFace(v1, v2, v3, n1, n2, n3);
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

        private void handleAddNormal(string line)
        {
            // Vertices are defined as follows:
            // 'v' x y z [w]
            string[] splitLine = line.Split(' ');

            double x = double.Parse(splitLine[1]);
            double y = double.Parse(splitLine[2]);
            double z = double.Parse(splitLine[3]);

            normals.Add(new Vector3((float)x, (float)y, (float)z));
        }
    }
}

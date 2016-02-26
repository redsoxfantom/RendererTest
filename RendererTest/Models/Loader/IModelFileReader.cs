using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTest.Models.Loader
{
    public interface IModelFileReader
    {
        Model LoadFromFile(String file);
    }
}

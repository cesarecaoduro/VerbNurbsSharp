using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerbNurbsSharp.Blazor.Babylon.Core;

namespace VerbNurbsSharp.Blazor.Babylon
{
    public interface IBabylonFactory
    {
        Task<Engine> CreateEngine(string canvasId, bool antialias = false);
        Task<Scene> CreateScene(Engine engine);
    }
}

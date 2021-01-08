using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerbNurbsSharp.Blazor.Babylon.Camera;
using VerbNurbsSharp.Blazor.Babylon.Core;
using VerbNurbsSharp.Blazor.Babylon.Lights;
using VerbNurbsSharp.Blazor.Babylon.Meshes;

namespace VerbNurbsSharp.Blazor.Babylon
{
    public interface IBabylonFactory
    {
        Task<ArcRotateCamera> CreateArcRotateCamera(string name, double alpha, double beta, double radius, Vector3 target, Scene scene, string canvasId);
        Task<Engine> CreateEngine(string canvasId, bool antialias = false);
        Task<HemisphericLight> CreateHemispehericLight(string name, Vector3 direction, Scene scene);
        Task<PointLight> CreatePointLight(string name, Vector3 direction, Scene scene);
        Task<Scene> CreateScene(Engine engine);
        Task<Mesh> CreateSphere(string name, ExpandoObject options, Scene scene);
        Task<Vector3> CreateVector3(double x, double y, double z);
    }
}

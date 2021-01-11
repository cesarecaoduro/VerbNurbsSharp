using Microsoft.JSInterop;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using VerbNurbsSharp.Blazor.Babylon.Camera;
using VerbNurbsSharp.Blazor.Babylon.Core;
using VerbNurbsSharp.Blazor.Babylon.Lights;
using VerbNurbsSharp.Blazor.Babylon.Meshes;

namespace VerbNurbsSharp.Blazor.Babylon
{
    public class BabylonFactory : IBabylonFactory
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BabylonFactory(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<ArcRotateCamera> CreateArcRotateCamera(string name, double alpha, double beta, double radius, Vector3 target, Scene scene, string canvasId)
        {
            return new ArcRotateCamera(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createArcRotateCamera", name, alpha, beta, radius, target, scene, canvasId));
        }

        public async Task<Engine> CreateEngine(string canvasId, bool antialias = false)
        {
            return new Engine(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createEngine", canvasId, antialias));
        }

        public async Task<HemisphericLight> CreateHemispehericLight(string name, Vector3 direction, Scene scene)
        {
            return new HemisphericLight(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createHemisphericLight", name, direction, scene));
        }

        public async Task<PointLight> CreatePointLight(string name, Vector3 direction, Scene scene)
        {
            return new PointLight(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createPointLight", name, direction, scene));
        }

        public async Task<Scene> CreateScene(Engine engine)
        {
            return new Scene(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createScene", engine));
        }

        public async Task<Mesh> CreateSphere(string name, ExpandoObject options, Scene scene)
        {
            return new Mesh(_jsRuntime, await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createSphere", name, options, scene));
        }

        public async Task<Vector3> CreateVector3(double x, double y, double z) => new Vector3(
            _jsRuntime, 
            await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createVector3", x, y, z));
        public async Task AddSphere(Scene scene) => await _jsRuntime.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createVector3", scene);


        public async Task DisposeMesh(Mesh mesh) => await _jsRuntime.InvokeVoidAsync("babylonInterop.removeObjectRef", mesh.JsObjectRefId);

    }
}

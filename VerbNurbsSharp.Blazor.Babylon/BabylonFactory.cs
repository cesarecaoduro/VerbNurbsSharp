using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using VerbNurbsSharp.Blazor.Babylon.Core;

namespace VerbNurbsSharp.Blazor.Babylon
{
    public class BabylonFactory : IBabylonFactory, IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BabylonFactory(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/VerbNurbsSharp.Blazor.Babylon/babylonInterop.js").AsTask());
        }

        public async Task<Scene> CreateScene(Engine engine)
        {
            var module = await moduleTask.Value;
            return new Scene(_jsRuntime, await module.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createScene", engine));
        }

        public async Task<Engine> CreateEngine(string canvasId, bool antialias = false)
        {
            var module = await moduleTask.Value;
            return new Engine(_jsRuntime, await module.InvokeAsync<JsRuntimeObjectRef>("babylonInterop.createEngine", canvasId, antialias));
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}

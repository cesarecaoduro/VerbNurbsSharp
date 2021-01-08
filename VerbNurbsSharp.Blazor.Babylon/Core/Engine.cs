using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbNurbsSharp.Blazor.Babylon.Core
{
    public class Engine : BabylonObject
    {
        public Engine(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }

        public async Task RunRenderLoop(Scene scene)
        {
            await _jsObjRef.JSRuntime.InvokeVoidAsync("babylonInterop.runRenderLoop", this, scene);
        }
    }
}

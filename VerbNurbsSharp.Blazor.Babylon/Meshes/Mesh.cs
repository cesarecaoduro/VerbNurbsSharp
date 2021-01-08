using Microsoft.JSInterop;

namespace VerbNurbsSharp.Blazor.Babylon.Meshes
{
    public class Mesh : BabylonObject
    {
        public Mesh(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}

using Microsoft.JSInterop;

namespace VerbNurbsSharp.Blazor.Babylon.Core
{
    public class Scene : BabylonObject
    {
        public Scene(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}
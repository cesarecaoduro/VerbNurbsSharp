using Microsoft.JSInterop;

namespace VerbNurbsSharp.Blazor.Babylon.Lights
{
    public class PointLight : BabylonObject
    {
        public PointLight(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}

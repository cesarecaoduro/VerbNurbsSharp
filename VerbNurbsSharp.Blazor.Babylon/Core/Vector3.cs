using Microsoft.JSInterop;

namespace VerbNurbsSharp.Blazor.Babylon.Core
{
    public class Vector3 : BabylonObject
    {
        public Vector3(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}

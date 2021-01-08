using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace VerbNurbsSharp.Blazor.Babylon
{
    public abstract class BabylonObject
    {
        protected JsRuntimeObjectRef _jsObjRef;

        [JsonPropertyName("__jsObjRefId")]
        public int JsObjectRefId => _jsObjRef.JsObjectRefId;

        public BabylonObject(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef)
        {
            _jsObjRef = objRef;
            _jsObjRef.JSRuntime = jsRuntime;
        }
    }
}

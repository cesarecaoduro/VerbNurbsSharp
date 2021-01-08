using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbNurbsSharp.Blazor.Babylon.Camera
{
    public class ArcRotateCamera : BabylonObject
    {
        public ArcRotateCamera(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbNurbsSharp.Blazor.Babylon.Lights
{
    public class HemisphericLight : BabylonObject
    {
        public HemisphericLight(IJSRuntime jsRuntime, JsRuntimeObjectRef objRef) : base(jsRuntime, objRef) { }
    }
}

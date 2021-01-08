export let babylonInterop = 
{
    objRefs :{},
    objRefId : 0,
    objRefKey : '__jsObjRefId',
    storeObjRef: function (obj) {
        var id = this.objRefId++;
        this.objRefs[id] = obj;
        var objRef = {};
        objRef[this.objRefKey] = id;
        return objRef;
    },
    removeObjectRef: function (id) {
        delete this.objRefs[id];
    },
    createEngine : function (canvasId, antialias) {
        var babylonCanvas = document.getElementById(canvasId);
        var babylonEngine = new BABYLON.Engine(babylonCanvas, antialias);
        window.addEventListener("resize", function () {
            babylonEngine.resize();
        });
        return this.storeObjRef(babylonEngine);
    },
    createScene : function (engine) {
        return this.storeObjRef(new BABYLON.Scene(engine));
    },
    runRenderLoop: function (engine, scene) {
        engine.runRenderLoop(function () {
            scene.render();
        });
    }
};

var babylonInterop = babylonInterop || {};

babylonInterop.objRefs = {};
babylonInterop.objRefId = 0;
babylonInterop.objRefKey = '__jsObjRefId';

babylonInterop.exports = {
    storeOnbjRef: function (obj) {
        var id = babylonInterop.objRefId++;
        babylonInterop.objRefs[id] = obj;
        var objRef = {};
        objRef[babylonInterop.objRefKey] = id;
        return objRef;
    },
    removeObjectRef: function (id) {
        delete babylonInterop.objRefs[id];
    },
    createEngine : function (canvasId, antialias) {
        var babylonCanvas = document.getElementById(canvasId);
        var babylonEngine = new BABYLON.Engine(babylonCanvas, antialias);
        window.addEventListener("resize", function () {
            babylonEngine.resize();
        });
        return babylonInterop.storeObjRef(babylonEngine);
    },
    createScene : function (engine) {
        return babylonInterop.storeObjRef(new BABYLON.Scene(engine));
    },
    runRenderLoop: function (engine, scene) {
        engine.runRenderLoop(function () {
            scene.render();
        });
    }
}
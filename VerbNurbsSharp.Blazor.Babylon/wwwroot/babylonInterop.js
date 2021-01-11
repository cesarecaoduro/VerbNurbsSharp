var babylonInterop = babylonInterop || {};

babylonInterop.objRefs = {};
babylonInterop.objRefId = 0;
babylonInterop.objRefKey = '__jsObjRefId';
babylonInterop.storeObjRef = function (obj) {
    var id = babylonInterop.objRefId++;
    babylonInterop.objRefs[id] = obj;
    var objRef = {};
    objRef[babylonInterop.objRefKey] = id;
    return objRef;
}


babylonInterop.removeObjectRef = function (id) {
    if (babylonInterop.objRefs[id] != null) {
        babylonInterop.objRefs[id].dispose();
        delete babylonInterop.objRefs[id];
    }
}

babylonInterop.AddSphere = function (scene) {
    var sphere = BABYLON.MeshBuilder.CreateSphere("sphere", { diameter: 2 }, scene); 
}

DotNet.attachReviver(function (key, value) {
    if (value &&
        typeof value === 'object' &&
        value.hasOwnProperty(babylonInterop.objRefKey) &&
        typeof value[babylonInterop.objRefKey] === 'number') {
        var id = value[babylonInterop.objRefKey];
        if (!(id in babylonInterop.objRefs)) {
            throw new Error("The JS object reference doesn't exist: " + id);
        }
        const instance = babylonInterop.objRefs[id];
        return instance;
    } else {
        return value;
    }
});

babylonInterop.attachGrid = function (scene) {

    var groundMaterial = new BABYLON.GridMaterial("groundMaterial", scene);
    groundMaterial.majorUnitFrequency = 5;
    groundMaterial.minorUnitVisibility = 0.45;
    groundMaterial.gridRatio = 2;
    groundMaterial.backFaceCulling = false;
    groundMaterial.mainColor = new BABYLON.Color3(1, 1, 1);
    groundMaterial.lineColor = new BABYLON.Color3(0, 0, 0);
    groundMaterial.opacity = 0.98;

    //abstract plane from its position and normal
    const abstractPlane = BABYLON.Plane.FromPositionAndNormal(new BABYLON.Vector3(0, 0, 0), new BABYLON.Vector3(0, 1, 0));
    const ground = BABYLON.MeshBuilder.CreatePlane("plane", { sourcePlane: abstractPlane, sideOrientation: BABYLON.Mesh.DOUBLESIDE, size: 20 });

    ground.material = groundMaterial;
}

babylonInterop.initCanvas = function (canvasId) {
    var babylonCanvas = document.getElementById(canvasId);
    var babylonEngine = new BABYLON.Engine(babylonCanvas, true);

    var scene = babylonInterop.createSceneWithSphere(babylonEngine, babylonCanvas);


    babylonEngine.runRenderLoop(function () {
        scene.render();
    });

    window.addEventListener("resize", function () {
        babylonEngine.resize();
    });
};


babylonInterop.createArcRotateCamera = function (name, alpha, beta, radius, target, scene, canvasId) {
    var camera = new BABYLON.ArcRotateCamera(name, alpha, beta, radius, target, scene);
    var canvas = document.getElementById(canvasId);
    camera.attachControl(canvas, true);
    return babylonInterop.storeObjRef(camera);
}

babylonInterop.createEngine = function (canvasId, antialias) {
    var babylonCanvas = document.getElementById(canvasId);
    var babylonEngine = new BABYLON.Engine(babylonCanvas, antialias);
    window.addEventListener("resize", function () {
        babylonEngine.resize();
    });
    return babylonInterop.storeObjRef(babylonEngine);
}

babylonInterop.createHemisphericLight = function (name, direction, scene) {
    return babylonInterop.storeObjRef(new BABYLON.HemisphericLight(name, direction, scene));
}

babylonInterop.createPointLight = function (name, direction, scene) {
    return babylonInterop.storeObjRef(new BABYLON.PointLight(name, direction, scene));
}

babylonInterop.createScene = function (engine) {
    return babylonInterop.storeObjRef(new BABYLON.Scene(engine));
}

babylonInterop.createSphere = function (name, options, scene) {
    babylonInterop.attachGrid(scene);
    return babylonInterop.storeObjRef(BABYLON.MeshBuilder.CreateSphere(name, options, scene));
}

babylonInterop.createVector3 = function (x, y, z) {
    return babylonInterop.storeObjRef(new BABYLON.Vector3(x, y, z));
}

babylonInterop.runRenderLoop = function (engine, scene) {
    engine.runRenderLoop(function () {
        scene.render();
    });
}
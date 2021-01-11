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

babylonInterop.createSceneWithSphere = function (engine, canvas) {
    scene = new BABYLON.Scene(engine);

    var camera = new BABYLON.ArcRotateCamera("Camera", Math.PI / 2, Math.PI / 2, 2, new BABYLON.Vector3(0, 0, 5), scene);
    camera.attachControl(canvas, true);

    var light1 = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(1, 1, 0), scene);
    var light2 = new BABYLON.PointLight("light2", new BABYLON.Vector3(0, 1, -1), scene);

    var sphere = BABYLON.MeshBuilder.CreateSphere("sphere", { diameter: 2 }, scene);
    return scene;
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
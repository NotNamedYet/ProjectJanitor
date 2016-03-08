using UnityEditor;
using System.Collections;
using UnityEngine;
using GalacticJanitor.Engine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GalacticJanitor.Game;
using MonoPersistency;
using System.Text;

public class EditorMenu
{

    [MenuItem("GameObject/GalacticJanitor/Add Engine", false, 0)]
    static void CreateGameController()
    {
        GameObject engine = GameObject.Find("_Engine");

        if (engine == null)
        {
            engine = new GameObject("_Engine");
            engine.transform.position = new Vector3(0, 0, 0);
        }

        if (!engine.GetComponent<GameController>())
        {
            engine.AddComponent<GameController>();
        }
    }

    [MenuItem("GameObject/GalacticJanitor/Add Teleporter", false, 0)]
    static void AddTeleporter()
    {
        GameObject parent = GameObject.Find("_Teleporters");

        if (parent == null)
        {
            parent = new GameObject("_Teleporters");
            parent.transform.position = new Vector3(0, 0, 0);
        }

        int index = 1;

        while (GameObject.Find("Teleporter#" + index) != null)
        {
            index++;
        }

        GameObject go = new GameObject("Teleporter#" + index);
        go.AddComponent<TeleporterController>();

        if (Selection.activeGameObject != null)
        {
            GameObject selected = Selection.activeGameObject;
            go.transform.position = selected.transform.position;

            if (selected.GetComponent<TeleporterVisual>() != null)
            {
                selected.transform.SetParent(go.transform);
                go.GetComponent<TeleporterController>().telepoterVisual = selected.GetComponent<TeleporterVisual>();
            }  
        }
        else
        {
            go.transform.position = new Vector3(0, 0, 0);
        }

        go.transform.SetParent(parent.transform);
        SphereCollider col = go.AddComponent<SphereCollider>();
        col.isTrigger = true;

        Selection.activeGameObject = go;
    }

    [MenuItem("GameObject/GalacticJanitor/Add NavMesh Parent", false, 0)]
    static void SetupNavmeshParent()
    {
        GameObject parent = new GameObject("_Navmeshes-" + SceneManager.GetActiveScene().name);
        GameObject planes = new GameObject("_NavPlanes");
        GameObject obstac = new GameObject("_NavObstacles");
        planes.transform.SetParent(parent.transform);
        obstac.transform.SetParent(parent.transform);
    }


    [MenuItem("GameObject/GalacticJanitor/Add TopDownCamera", false, 0)]
    static void Setup3DCamera()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        if (obj == null)
        {
            Debug.Log("MainCamera Tag not found... Let's create it");
            obj = new GameObject("TopDownCamera");
            obj.tag = "MainCamera";
        }
        else
        {
            obj.name = "TopDownCamera";
        }

        //CHECKING CAMERA...

        Camera cam = obj.GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogWarning("No camera found on MainCamera object.. Let's fix it.");
            obj.AddComponent<Camera>();

            cam = obj.GetComponent<Camera>();
        }

        //CHECKING OTHER COMPONENTS...

        if (!cam.GetComponent<GUILayer>())
        {
            obj.AddComponent<GUILayer>();
        }

        if (!cam.GetComponent<FlareLayer>())
        {
            obj.AddComponent<FlareLayer>();
        }

        if (!cam.GetComponent<AudioListener>())
        {
            obj.AddComponent<AudioListener>();
        }

        if (!cam.GetComponent<PixelScaling>())
        {
            obj.AddComponent<PixelScaling>();
        }

        if (!cam.GetComponent<TopDownCamera>())
        {
            obj.AddComponent<TopDownCamera>();
        }

        if (!cam.GetComponent<AudioSource>())
        {
            obj.AddComponent<AudioSource>();
        }

        cam.transform.localPosition = new Vector3(0f, 10f, 0f);
        cam.transform.localRotation = Quaternion.Euler(90f, 0, 0);
        cam.orthographic = true;
        cam.orthographicSize = 20;
        cam.nearClipPlane = .3f;
        cam.farClipPlane = 100;
        cam.depth = -1f;
        cam.rect = new Rect(0, 0, 1, 1);
        cam.useOcclusionCulling = true;
        cam.hdr = false;
        cam.renderingPath = RenderingPath.UsePlayerSettings;
        cam.targetTexture = null;
        cam.backgroundColor = Color.black;

        Debug.Log("Camera setup Done !");

        CheckWorldDirectory();

        Selection.activeGameObject = obj;
    }

    [MenuItem("GameObject/GalacticJanitor/Add SceneSound", false, 0)]
    static void SetupSceneSound()
    {
        GameObject obj = GameObject.Find("SceneSoundManager");

        if (obj == null) obj = new GameObject("SceneSoundManager");

        if (!obj.GetComponent<SceneAmbiance>())
        {
            obj.AddComponent<SceneAmbiance>();
        }

        if (!obj.GetComponent<AudioSource>())
        {
            obj.GetComponent<SceneAmbiance>().listenerMusic = obj.AddComponent<AudioSource>(); // music source
            obj.GetComponent<SceneAmbiance>().listenerMusic.spatialBlend = 0;
            obj.GetComponent<SceneAmbiance>().listenerMusic.maxDistance = 50f;
            obj.GetComponent<SceneAmbiance>().listenerAmbiance = obj.AddComponent<AudioSource>(); // ambiance sounds source
            obj.GetComponent<SceneAmbiance>().listenerAmbiance.spatialBlend = 0;
            obj.GetComponent<SceneAmbiance>().listenerAmbiance.maxDistance = 50f;
        }
    }

    private static void CheckWorldDirectory()
    {
        GameObject _world = GameObject.Find("_World");

        if (_world == null)
        {
            _world = new GameObject("_World");
        }

        CheckSubDirectory("_Grounds", _world);
        CheckSubDirectory("_Walls", _world);
        CheckSubDirectory("_Statics", _world);

    }

    private static void CheckSubDirectory(string name, GameObject parent)
    {
        GameObject obj = GameObject.Find(name);

        if (obj != null)
        {
            if (obj.transform.parent != parent.transform)
            {
                obj.transform.parent = parent.transform;
            }
        }
        else
        {
            obj = new GameObject(name);
            obj.transform.parent = parent.transform;
        }

    }

    

    

}

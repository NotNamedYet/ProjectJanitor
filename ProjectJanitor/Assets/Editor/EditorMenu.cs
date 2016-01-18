using UnityEditor;
using System.Collections;
using UnityEngine;
using GalacticJanitor.Persistency;
using GalacticJanitor.Engine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EditorMenu
{

    [MenuItem("GalacticJanitor/Add GameController")]
    static void CreateGameController()
    {
        GameObject parent = GameObject.Find("_Engine");

        if (parent == null)
        {
            parent = new GameObject("_Engine");
            parent.transform.position = new Vector3(0, 0, 0);
        }

        bool b = false;
        GameObject go;
        GameController ctrl = Object.FindObjectOfType<GameController>();

        if (ctrl != null)
        {
            go = ctrl.gameObject;
            go.name = "GameController";
            Debug.LogWarning("Already existing GameController, let's fix it...");
            b = true;
        }
        else
        {
            go = new GameObject("GameController");
            go.AddComponent<GameController>();
        }

        go.transform.position = new Vector3(0, 0, 0);
        go.transform.SetParent(parent.transform);
        Debug.Log("GameController " + (!b ? "added !" : "fixed !"));

    }

    [MenuItem("GalacticJanitor/Add Savable")]
    static void CreateSavable()
    {

        GameObject parent = GameObject.Find("_Savables");

        if (parent == null)
        {
            parent = new GameObject("_Savables");
            parent.transform.position = new Vector3(0, 0, 0);
        }

        GameObject go = new GameObject("Savable");

        go.transform.position = new Vector3(0, 0, 0);
        go.transform.SetParent(parent.transform);
        go.AddComponent<Savable>();

    }

    [MenuItem("GalacticJanitor/Check Scene")]
    static void CheckScene()
    {
        CheckSavables();
    }

    private static void CheckSavables()
    {
        bool allFine = true;
        int warn = 0;

        Debug.Log("CheckIngSavables...");
        Savable[] savables = Object.FindObjectsOfType<Savable>();

        GameObject parent = GameObject.Find("_Savables");

       
        if (parent == null)
        {
            parent = new GameObject("_Savables");
            parent.transform.position = new Vector3(0, 0, 0);
            Debug.Log("Missing _Savables holder generated !");
        }

        List<string> guids = new List<string>(savables.Length);
        List<Savable> duplicated = null;

        int fixes = 0;

        foreach (Savable s in savables)
        {
            if (s.savableObject == null)
            {
                Debug.LogWarning("Reference for " + s.gameObject.name + " is null !");
                if (!s.gameObject.name.Contains("#NULL"))
                {
                    string n = s.gameObject.name + "#NULL";
                    s.gameObject.name = n;
                }
                warn++;

            }

            if (!guids.Contains(s.uniqueID))
            {
                guids.Add(s.uniqueID);

                if (s.transform.parent != parent.transform)
                {
                    s.transform.SetParent(parent.transform);
                    fixes++;
                }
            }
            else
            {
                if (duplicated == null)
                {
                    duplicated = new List<Savable>();
                }
                duplicated.Add(s);
            }
        }

        if (fixes > 0)
        {
            Debug.Log(fixes + " Savable(s) moved under _Savables in hierarchy");
        }


        if (duplicated != null)
        {
            allFine = false;

            GameObject dupParent = GameObject.Find("_Savables#GUID_Duplicated");

            if (dupParent == null)
            {
                dupParent = new GameObject("_Savables#GUID_Duplicated");
                dupParent.transform.position = new Vector3(0, 0, 0);
            }

            foreach (Savable s in duplicated)
            {
                s.transform.SetParent(dupParent.transform);
                Debug.LogWarning("Duplicated GUI for Savable " + s.gameObject.name + " GUID: " + s.uniqueID);
                warn++;
            }
            Debug.Log(duplicated.Count + " Duplicated GuiD waiting to be fixed under _Savables#GUID_Duplicated in hierarchy");
        }
        
        if (allFine)
            Debug.Log("Savables checking : Success \n" + (warn > 0 ? "("+warn+") warnings" : "(OK)" ));
        else
            Debug.Log("Savables checking : Failed " + (warn > 0 ? "(" + warn + ") warnings" : ""));

    }

    [MenuItem("GalacticJanitor/Setup 3D Camera")]
    static void Setup3DCamera()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        if (obj == null)
        {
            Debug.Log("MainCamera Tag not found... Let's create it");
            obj = new GameObject("Main Camera");
            obj.tag = "MainCamera";
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


    }

    [MenuItem("GalacticJanitor/NavMesh Parent")]
    static void SetupNavmeshParent()
    {
        GameObject parent = new GameObject("_Navmeshes-" + SceneManager.GetActiveScene().name);
        GameObject planes = new GameObject("_NavPlanes");
        GameObject obstac = new GameObject("_NavObstacles");
        planes.transform.SetParent(parent.transform);
        obstac.transform.SetParent(parent.transform);

    }

}

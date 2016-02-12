using UnityEditor;
using MonoPersistency;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading;

public class MonoPersistentEditor
{

    [MenuItem("GameObject/MonoPersistent/Add SaveSystem", false, 0)]
    static void CreateSaveSystem()
    {
        GameObject _object;

        SaveSystem _ss = Object.FindObjectOfType<SaveSystem>();

        if (_ss)
        {
            _object = _ss.gameObject;
        }
        else
        {
            _object = new GameObject("_SaveSystem");
            _object.AddComponent<SaveSystem>();
            Debug.Log("A SaveSystem has been created.");
        }

        _object.name = "_SaveSystem";
        _object.transform.position = new Vector3(0, 0, 0);

    }

    [MenuItem("GameObject/MonoPersistent/Bake Scene", false, 0)]
    public static void BakeMonoPeristent()
    {
        CreateSaveSystem();

        MonoPersistent[] monos = Object.FindObjectsOfType<MonoPersistent>();

        if (monos.Length == 0)
        {
            Debug.Log("No MonoPersistent derived object found... Abort.");
            return;
        }

        foreach (MonoPersistent o in monos)
        {
            if (!o.bypassBaking)
            {
                if (o.GetComponent<MonoPersistentInitializer>() == null)
                    o.gameObject.AddComponent<MonoPersistentInitializer>();

                if (!MonoPersistentCache.validates.Contains(o) && !o.name.EndsWith("##"))
                {
                    StringBuilder buffer = new StringBuilder();



                    buffer.Append(string.IsNullOrEmpty(o.specificName) ? o.GetType().Name : o.specificName);
                    buffer.Append('-');
                    buffer.Append(new PrettyCode());
                    o.name = buffer.ToString();

                }

                if (!MonoPersistentCache.validates.Contains(o))
                {
                    MonoPersistentCache.validates.Add(o);
                }
            }
        }
    }
}

public static class MonoPersistentCache
{
    public static List<MonoPersistent> validates = new List<MonoPersistent>();
}

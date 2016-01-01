using UnityEngine;
using System.Collections;

public class PersistantOnLoad : MonoBehaviour {

	void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

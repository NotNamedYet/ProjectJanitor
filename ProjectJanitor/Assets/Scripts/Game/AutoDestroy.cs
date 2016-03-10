using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AutoDestroy : MonoBehaviour {

    public float m_destroyDelay;

	void OnEnable ()
    {
        Destroy(gameObject, m_destroyDelay);    	
	}
}

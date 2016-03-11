using UnityEngine;
using System.Collections;

public class SimpleTranslation : MonoBehaviour {

    public float m_TranslationSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * m_TranslationSpeed);
	}
}

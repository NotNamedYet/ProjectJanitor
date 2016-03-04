using UnityEngine;
using System.Collections;

public class DestroyBroker : MonoBehaviour {

    public GameObject m_ToDestoy;
    public float m_Delay;

    public void DestroyObject()
    {
        Destroy(m_ToDestoy);
    }

    public void DelayedDestroy()
    {
        Destroy(m_ToDestoy, m_Delay);
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class TechPlatform : MonoBehaviour {

    public LayerMask mask;
    public Sprite m_visualOn;
    public Sprite m_visualOff;
    SpriteRenderer m_renderer;

    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start ()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if ((mask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            m_renderer.sprite = m_visualOn;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((mask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            m_renderer.sprite = m_visualOff; 
        }
    }
}

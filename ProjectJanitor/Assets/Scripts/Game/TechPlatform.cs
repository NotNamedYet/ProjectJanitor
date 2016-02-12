using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class TechPlatform : MonoBehaviour {

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
        m_renderer.sprite = m_visualOn;
    }

    void OnTriggerExit(Collider other)
    {
        m_renderer.sprite = m_visualOff;
    }
}

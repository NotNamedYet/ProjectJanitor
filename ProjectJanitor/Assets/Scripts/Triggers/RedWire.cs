using UnityEngine;
using System.Collections;

public class RedWire : MonoBehaviour {

    public SpriteRenderer[] m_wires;
    public Color m_ColorOn;
    public Color m_ColorOff;

    public void SetSwitch(bool state)
    {
        if (m_wires != null)
            for (int i = 0; i < m_wires.Length; i++)
                m_wires[i].color = (state) ? m_ColorOn : m_ColorOff;
    }
}

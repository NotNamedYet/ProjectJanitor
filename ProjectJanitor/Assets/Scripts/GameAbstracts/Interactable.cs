using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;

public abstract class Interactable : MonoPersistent {

    
    private bool m_canInteract = true;

    public bool CanInteract
    {
        get
        {
            return m_canInteract;
        }
        set
        {
            m_canInteract = value;
        }
    }

    public abstract void OnInteract();
}

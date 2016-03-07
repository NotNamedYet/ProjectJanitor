using UnityEngine;
using System.Collections;
using GalacticJanitor.UI;
using GalacticJanitor.Engine;
using GalacticJanitor.Game;

[ExecuteInEditMode]
public class Activator : MonoBehaviour {

    public Interactable m_actor;
    public KeyCode m_keyCode;
    private bool m_candidateForAction;

    public bool playFocusScript;

    void OnEnable()
    {
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (m_candidateForAction)
        {
            if (Input.GetKeyDown(m_keyCode))
            {
                if (!playFocusScript)
                {
                    m_actor.OnInteract();

                    if (!m_actor.CanInteract)
                    {
                        GameController.TogglePlayerInteractor(false);
                    } 
                }
                else
                {
                    if (GetComponent<FocusTriggerConsole>())
                    {
                        FocusTriggerConsole trigger = GetComponent<FocusTriggerConsole>();
                        trigger.LaunchFocus(); 
                    }
                }
            }
        }
        if (m_actor) Debug.DrawLine(transform.position, m_actor.transform.position, Color.red);
	}

    void OnTriggerEnter(Collider other)
    {

        if (m_actor.CanInteract && other.CompareTag("Player"))
        {
            m_candidateForAction = true;
            GameController.TogglePlayerInteractor(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_candidateForAction = false;
            GameController.TogglePlayerInteractor(false);
        }
    }
}

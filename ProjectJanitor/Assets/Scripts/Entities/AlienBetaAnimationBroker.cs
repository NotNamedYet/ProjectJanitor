using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

public class AlienBetaAnimationBroker : MonoBehaviour {

    public AlienBeta m_Beta;

    [Range(0f, 1f)]
    public float m_FootStepVolume = 1;

    public void FootStepSound()
    {
        if (m_Beta.sndOnMove)
            m_Beta.listener.PlayOneShot(m_Beta.sndOnMove, m_FootStepVolume);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GalacticJanitor.UI
{
    [RequireComponent(typeof(Text))]
    public class MagicText : MonoBehaviour
    {
        char[] m_Table = { '0', '1', 'A', '%', 'L', 'R', 'X' };
        char[] m_Buffer;
        char[] m_BuilChar;

        string m_BackUp;
        Text m_TextComponent;
        string m_Build;
        int m_BufferStep;
        int m_BufferPos;

        [SerializeField]
        public Rollback[] m_Rollbacks;
        public float m_EraseDelay = .1f;
        int m_RollbackIndex = 0;
        bool invert;

        public bool m_CastOnStart;
        public int m_LetterSteps = 2;

        // Use this for initialization
        void Start()
        {
            m_TextComponent = GetComponent<Text>();
            m_BackUp = m_TextComponent.text;
            m_Buffer = m_TextComponent.text.ToCharArray();
            m_BuilChar = new char[m_Buffer.Length];
            m_TextComponent.text = "";

            if (m_CastOnStart)
                CastMagic();
        }

        //Reset the component to be able to restart it.
        public void ResetEffect()
        {
            StopCoroutine("MagicRoutine");

            m_Buffer = m_BackUp.ToCharArray();
            m_Build = "";
            m_BuilChar = new char[m_Buffer.Length];
            m_TextComponent.text = "";
            m_BufferStep = 0;
            m_BufferPos = 0;
            m_RollbackIndex = 0;
            
        }

        //Launch the effet...
        public void CastMagic()
        {
            StartCoroutine("MagicRoutine");
        }

        IEnumerator MagicRoutine()
        {
            int index = 0;
            while (index <= m_Buffer.Length - 1)
            {
                if (!invert) // Normal behavior
                {
                    char next;
                    bool letterSwitch = true;
                    for (int i = 0; i < m_LetterSteps; i++)
                    {
                        next = (letterSwitch)? next = '_' : m_Table[UnityEngine.Random.Range(0, m_Table.Length - 1)];
                        letterSwitch ^= true;

                        m_BuilChar[m_BufferStep] = next;
                        Compose(false);

                        yield return new WaitForEndOfFrame();
                    }

                    next = m_Buffer[index];
                    m_BuilChar[m_BufferStep] = next;
                    Compose(false);

                    index++;
                    m_BufferStep++;
                    m_BufferPos++;

                    if (m_Rollbacks != null && m_Rollbacks.Length > m_RollbackIndex)
                    {
                        if (m_BufferPos == m_Rollbacks[m_RollbackIndex].m_BreakPoint)
                        {
                            yield return new WaitForSeconds(m_Rollbacks[m_RollbackIndex].m_DelayIn);
                            invert = true;
                        }
                    }
                    
                }
                else
                {
                    Rollback r = m_Rollbacks[m_RollbackIndex];

                    for(int i = 0; i < r.m_RollBackAmount; i++)
                    {

                        m_BuilChar[m_BufferStep] = ' ';
                        m_BufferStep--;
                        Compose(true);
                        yield return new WaitForSeconds(m_EraseDelay);
                    }

                    m_BuilChar[m_BufferStep] = ' ';
                    Compose(false);

                    yield return new WaitForSeconds(r.m_DelayOff);
                    invert = false;
                    m_RollbackIndex++;
                }
                
            }
            yield return null;
        }

        void Compose(bool trim)
        {
            string txt = new string(m_BuilChar);

            if (trim)
                txt.TrimEnd();

            m_TextComponent.text = new string(m_BuilChar);
        }

        void Compile(char c)
        {
            m_Build = m_Build + c;
            m_TextComponent.text = m_Build.TrimEnd();
        }
    }

    [System.Serializable]
    public class Rollback
    {
        public int m_BreakPoint; // when ?
        public int m_RollBackAmount; // How long...
        public float m_DelayIn; // Wait before rolling back.
        public float m_DelayOff; // Wait after, then re-go... 
    }


}
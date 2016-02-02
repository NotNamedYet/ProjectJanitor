using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GalacticJanitor.UI
{
    [RequireComponent(typeof(Text))]
    public class TextTrublizer : MonoBehaviour
    {

        char[] table = { '0', '1', 'A', '%', 'L', 'R', 'X' };
        Text textComponent;
        char[] charachers;
        string fakeText;

        void Awake()
        {
            textComponent = GetComponent<Text>();
            charachers = textComponent.text.ToCharArray();
            textComponent.text = "";
        }

        // Use this for initialization
        void Start()
        {
            StartCoroutine(Trubelize());
        }

        IEnumerator Trubelize()
        {
            foreach (char c in charachers)
            {
                for (int i = 0; i < 2; i++)
                {
                    int max = table.Length - 1;
                    char cc = table[Random.Range(0, max)];

                    textComponent.text = fakeText + cc;
                    yield return new WaitForEndOfFrame();
                }
                fakeText = fakeText + c;
                textComponent.text = fakeText;

                yield return new WaitForEndOfFrame();
            }
            StopAllCoroutines();
        }
    } 
}

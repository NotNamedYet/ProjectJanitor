using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{

    public GameObject[] paragraph;

    public GameObject textDefilement;

    public float[] delay;

    int i = 0;
    bool isEnd = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (i == paragraph.Length && isEnd == true)
        {
            //textDefilement.transform.Translate(0, Time.deltaTime * 2, 0);

        }
    }

    public IEnumerator Wait()
    {
        for (i = 0; i < paragraph.Length; i++)
        {
            paragraph[i].SetActive(true);
            yield return new WaitForSeconds(delay[i]);
        }
        isEnd = true;
    }

}
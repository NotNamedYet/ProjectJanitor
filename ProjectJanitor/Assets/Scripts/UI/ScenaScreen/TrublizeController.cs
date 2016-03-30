using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrublizeController : MonoBehaviour {

    public GameObject[] paragraph;
    public GameObject standByText;
    public GameObject skipButton;

    public float[] delay;

    int i = 0;
    
    // Use this for initialization
    void Start () {
        skipButton.SetActive(true);
        StartCoroutine(Wait());
    }
	
	// Update is called once per frame
	void Update () {
        if(i == paragraph.Length && Input.anyKey)
        {
            SceneManager.LoadScene("Floor0_BadWakeUp");
        }
	}

    public IEnumerator Wait()
    {
        for(i = 0; i < paragraph.Length; i++)
        {
            paragraph[i].SetActive(true);
            yield return new WaitForSeconds(delay[i]);
        }
        skipButton.SetActive(false);
        StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        while (true)
        {
            standByText.SetActive(true);
            yield return new WaitForSeconds(1f);
            standByText.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

}
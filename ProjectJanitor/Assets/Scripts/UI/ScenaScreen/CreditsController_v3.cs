using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonoPersistency;

public class CreditsController_v3 : MonoBehaviour {

    [Header("Scenario")]
    public GameObject goDefilement;
    public GameObject[] paragraph;
    public float[] delay;

    [Header("Credits")]
    public GameObject[] section;
    public GameObject[] title;
    public float delayShowCredit;

    int i = 0;
    byte j = 0;
    bool scenarEnd = false;
    bool isEnd = false;


    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update ()
    {
        LoopGame();
	}

    public void LoopGame()
    {
        if (i == section.Length && isEnd == true)
        {
            SceneManager.LoadScene("scn_MainMenu");
            SaveSystem.instance.ResetRegistery();
        }
    }


    public IEnumerator Wait()
    {
        Debug.Log("Je start la corout Wait");
        for (i = 0; i < paragraph.Length; i++)
        {
            paragraph[i].SetActive(true);
            yield return new WaitForSeconds(delay[i]);
        }
        goDefilement.SetActive(false);
        StartCoroutine(ShowCredits());
    }

    public IEnumerator ShowCredits()
    {
        for(i=0; i<section.Length; i++)
        {
            title[i].SetActive(true);
            section[i].SetActive(true);
            yield return new WaitForSeconds(delayShowCredit);
            title[i].SetActive(false);
            section[i].SetActive(false);
        }
        isEnd = true;
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using GalacticJanitor.Engine;

public class CreditsController_v2 : MonoBehaviour
{
    [Header("Scenario")]
    public GameObject[] paragraph;
    public float[] delay;

    [Header("Credits")]
    public GameObject[] section;
    public GameObject[] title;
    public float delayShowCredit;

    [Header("Hider")]
    public GameObject hider;
    public float timing;

    int i = 0;
    byte j = 0;
    bool isEnd = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        ScenarEnd();
    }

    public void ScenarEnd()
    {
        if (i == paragraph.Length && isEnd == true)
        {
            Debug.Log("J'ai termin� la corout Wait et switch isEnd � true");
            //test.transform.Translate(0, Time.deltaTime * 2, 0);
            for (i = 0; i < paragraph.Length; i++)
            {
                paragraph[i].SetActive(false);
            }
            StartCoroutine(CreditsShow());
        }
    }

/*
    public void ToggleText(int i)
    {
        Debug.Log("J'appelle ToggleText pour l'index " + i);
        if (title[i].activeInHierarchy)
            title[i].SetActive(false);
        else
            title[i].SetActive(true);

        if (section[i].activeInHierarchy)
            section[i].SetActive(false);
        else
            section[i].SetActive(true);
    }
*/

    public void ShowText(int i)
    {
        title[i].SetActive(true);
        section[i].SetActive(true);
    }

    public void HideText(int i)
    {
        title[i].SetActive(false);
        section[i].SetActive(false);
    }

    public IEnumerator Wait()
    {
        Debug.Log("Je start la corout Wait");
        for (i = 0; i < paragraph.Length; i++)
        {
            paragraph[i].SetActive(true);
            yield return new WaitForSeconds(delay[i]);
        }
        isEnd = true;
    }

    public IEnumerator CreditsShow()
    {
        Debug.Log("Je start la corout CreditsShow");
        for (i = 0; i <= (section.Length-1); i++)
        {
            //ToggleText(i);
            ShowText(i);
            yield return new WaitForSeconds(delayShowCredit);
            hider.SetActive(true);
            while (j <= 254)
            {
                hider.GetComponent<Image>().color = new Color32(255, 255, 255, j);
                j++;
            }
            //StartCoroutine(HiderOn());
            //ToggleText(i);
            HideText(i);
            while (j >= 1)
            {
                hider.GetComponent<Image>().color = new Color32(255, 255, 255, j);
                j--;
                //yield return new WaitForSeconds(timing);
            }
            hider.SetActive(false);
            //StartCoroutine(HiderOff());
        }
    }

/*
    public IEnumerator HiderOn()
    {
        Debug.Log("L�, j'active et affiche l'image hider");
        hider.SetActive(true);
        while (j < 255)
        {
            hider.GetComponent<Image>().color = new Color32(255, 255, 255, j);
            j++;
            yield return new WaitForSeconds(timing);
        }
    }

    public IEnumerator HiderOff()
    {
        Debug.Log("Maintenant, je d�sactive sur un fondu l'image hider");
        while (j > 0)
        {
            hider.GetComponent<Image>().color = new Color32(255, 255, 255, j);
            j--;
            yield return new WaitForSeconds(timing);
        }
        hider.SetActive(false);
    }
*/

}
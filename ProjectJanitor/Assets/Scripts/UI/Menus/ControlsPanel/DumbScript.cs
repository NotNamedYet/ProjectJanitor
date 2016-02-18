using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DumbScript : MonoBehaviour {

    public GameObject zKeyLink;
    public GameObject zText;
    public GameObject sKeyLink;
    public GameObject sText;
    public GameObject qKeyLink;
    public GameObject qText;
    public GameObject dKeyLink;
    public GameObject dText;
    public GameObject eKeyLink;
    public GameObject eText;
    public GameObject rKeyLink;
    public GameObject rText;
    public GameObject escKeyLink;
    public GameObject escText;

    public GameObject corpusLink;
    public GameObject corpusText;
    public GameObject corpusButton;
    public GameObject lbLink;
    public GameObject lbText;
    public GameObject lbButton;
    public GameObject rbLink;
    public GameObject rbText;
    public GameObject rbButton;

    public GameObject descKeyPanel;
    public GameObject descMousePanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowZKey()
    {
        zKeyLink.SetActive(true);
        zText.SetActive(true);
        descKeyPanel.SetActive(true);
    }

    public void HideZKey()
    {
        zKeyLink.SetActive(false);
        zText.SetActive(false);
        descKeyPanel.SetActive(false);
    }

    public void ShowLB()
    {
        lbLink.SetActive(true);
        lbText.SetActive(true);
        descMousePanel.SetActive(true);
        lbButton.GetComponent<Image>().color = Color.red;
    }

    public void HideLB()
    {
        lbLink.SetActive(false);
        lbText.SetActive(false);
        descMousePanel.SetActive(false);
        lbButton.GetComponent<Image>().color = new Color(0.280f,0.280f,0.280f,1);
    }

    public void ShowRB()
    {
        rbLink.SetActive(true);
        rbText.SetActive(true);
        descMousePanel.SetActive(true);
        rbButton.GetComponent<Image>().color = Color.red;
    }

    public void HideRB()
    {
        rbLink.SetActive(false);
        rbText.SetActive(false);
        descMousePanel.SetActive(false);
        rbButton.GetComponent<Image>().color = new Color(0.280f, 0.280f, 0.280f, 1);

    }

}

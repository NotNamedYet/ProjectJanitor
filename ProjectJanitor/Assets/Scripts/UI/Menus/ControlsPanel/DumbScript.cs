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

    public void ShowDescKeyPanel()
    {
        descKeyPanel.SetActive(true);
    }

    public void HideDescKeyPanel()
    {
        descKeyPanel.SetActive(false);
    }

    public void ShowDescMousePanel()
    {
        descMousePanel.SetActive(true);
    }

    public void HideDescMousePanel()
    {
        descMousePanel.SetActive(false);
    }


    public void ShowZKey()
    {
        zKeyLink.SetActive(true);
        zText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideZKey()
    {
        zKeyLink.SetActive(false);
        zText.SetActive(false);
        HideDescKeyPanel();
    }

    public void ShowSKey()
    {
        sKeyLink.SetActive(true);
        sText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideSKey()
    {
        sKeyLink.SetActive(true);
        sText.SetActive(true);
        HideDescKeyPanel();
    }

    public void ShowQKey()
    {
        qKeyLink.SetActive(true);
        sText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideQKey()
    {
        qKeyLink.SetActive(false);
        qText.SetActive(false);
        HideDescKeyPanel();
    }

    public void ShowDKey()
    {
        dKeyLink.SetActive(true);
        dText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideDKey()
    {
        dKeyLink.SetActive(false);
        dText.SetActive(false);
        HideDescKeyPanel();
    }

    public void ShowEKey()
    {
        eKeyLink.SetActive(true);
        eText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideEKey()
    {
        eKeyLink.SetActive(false);
        eText.SetActive(false);
        HideDescKeyPanel();
    }

    public void ShowRKey()
    {
        rKeyLink.SetActive(true);
        rText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideRKey()
    {
        rKeyLink.SetActive(true);
        rText.SetActive(true);
        HideDescKeyPanel();
    }

    public void ShowEscKey()
    {
        escKeyLink.SetActive(true);
        escText.SetActive(true);
        ShowDescKeyPanel();
    }

    public void HideEscKey()
    {
        escKeyLink.SetActive(false);
        escText.SetActive(false);
        HideDescKeyPanel();
    }

    public void ShowLB()
    {
        lbLink.SetActive(true);
        lbText.SetActive(true);
        ShowDescMousePanel();
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour {

    [Header("Context")]
    public Image m_image;
    public Text m_magazineText;
    public Text m_stockText;

    [Header("Sprites")]
    public Sprite m_bullet;
    public Sprite m_grenade;
    public Sprite m_flame;
    


	// Use this for initialization
	void Start ()
    {
	
	}

    public void DisplayBullet()
    {
        m_image.sprite = m_bullet;
    }

    public void DisplayGrenade()
    {
        m_image.sprite = m_grenade;
    }

    public void DisplayFlame()
    {
        m_image.sprite = m_flame;
    }

    public void UpdateText(int magazine, int stock)
    {
        m_magazineText.text = magazine.ToString();
        m_stockText.text = stock.ToString();
    }
}

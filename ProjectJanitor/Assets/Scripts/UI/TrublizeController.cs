using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrublizeController : MonoBehaviour {

    public GameObject paragraph1;
    public GameObject paragraph2;
    public GameObject paragraph3;
    public GameObject paragraph4;
    public GameObject paragraph5;

    public float _delay1 = 1f;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
    }
	
	// Update is called once per frame
	void Update () {

	}

    public IEnumerator Wait()
    {
        paragraph1.SetActive(true);
        yield return new WaitForSeconds(_delay1);
        paragraph2.SetActive(true);
        yield return new WaitForSeconds(_delay1);
        paragraph3.SetActive(true);
        yield return new WaitForSeconds(_delay1);
        paragraph4.SetActive(true);
        yield return new WaitForSeconds(_delay1);
        paragraph5.SetActive(true);
    }

}

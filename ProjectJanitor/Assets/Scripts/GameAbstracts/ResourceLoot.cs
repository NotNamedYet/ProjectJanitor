using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

public abstract class ResourceLoot : MonoBehaviour {

    public int amount;

    [Tooltip("Check it if you want a random number, use min and maxRangeToRandom")]
    public bool useRandomAmount;
    public int minRangeToRandom;
    public int maxRangeToRandom;

    // Use this for initialization
    void Start()
    {
        if (useRandomAmount)
            amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
    }

    /// <summary>
    /// Use to make a random amount, if the flag useRandomAmount is checked.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    int MakeRandomAmount(int min, int max)
    {
        int result = Random.Range(min - 1, max + 1);
        return result;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController target = other.GetComponent<PlayerController>();
            OnLoot(target);
        }
    }

    protected abstract void OnLoot(PlayerController player);
}

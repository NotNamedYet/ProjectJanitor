using UnityEngine;
using System.Collections;

public class SpawnOnDeath : MonoBehaviour {

    public Popper[] popers;

    bool shuttingDown = false;

    void OnApplicationQuit()
    {
        shuttingDown = true;
    }

    void OnDestroy()
    {
        if (!shuttingDown)
        {
            if (popers.Length > 0)
            {
                foreach (Popper pop in popers)
                {
                    pop.DoPop(transform.position, transform.rotation);
                }
            }
        }
    }
}

[System.Serializable]
public class Popper
{
    public GameObject pop;
    public int maxNumber;
    public bool randomPop;

    public void DoPop(Vector3 pos, Quaternion rot)
    {
        int number = maxNumber;
        if (randomPop && maxNumber > 1)
        {
            System.Random rnd = new System.Random();
            number = rnd.Next(1, maxNumber);
        }

        if (number > 1)
        {
            for (int i = 0; i < number; i++)
            {
                Object.Instantiate(pop, pos, rot);
            }
        }
        else
        {
            Object.Instantiate(pop, pos, rot);
        }

        
    }
}

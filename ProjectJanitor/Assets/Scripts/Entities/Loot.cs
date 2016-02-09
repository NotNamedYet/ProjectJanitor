using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System;

public class Loot : MonoBehaviour {

    public LootType type;

    void Start()
    {
        GameObject o = Instantiate(Resources.Load("LootArmor"), transform.position, transform.rotation) as GameObject;
    }

}

public enum LootType
{
    HEART,
    ARMOR,
}

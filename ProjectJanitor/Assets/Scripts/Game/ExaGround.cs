using UnityEngine;
using System.Collections.Generic;

public class ExaGround : MonoBehaviour {

    public LayerMask mask;
    public GameObject effect;
    Dictionary<int, GameObject> effects;

    void Start()
    {
        effects = new Dictionary<int, GameObject>();
    }

    void OnTriggerExit(Collider col)
    {
        if ((mask.value & 1 << col.gameObject.layer) == 1 << col.gameObject.layer)
        {
            int id = col.GetInstanceID();

            if (effects.ContainsKey(id))
            {
                GameObject toRemove = effects[id];
                effects.Remove(id);
                Destroy(toRemove);
            } 
        }
    }

	void OnTriggerEnter(Collider col)
    {
        if ((mask.value & 1 << col.gameObject.layer) == 1 << col.gameObject.layer)
        {
            GameObject toAdd = CreateEffect(col.transform);
            toAdd.transform.SetParent(col.transform);
            toAdd.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            effects.Add(col.GetInstanceID(), toAdd);
        }
    }

    GameObject CreateEffect(Transform target)
    {
        return Instantiate(effect, target.position, target.rotation) as GameObject;
    }
}

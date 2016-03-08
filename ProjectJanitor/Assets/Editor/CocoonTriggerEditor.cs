using UnityEngine;
using System.Collections;
using UnityEditor;

public class CocoonTriggerEditor : Editor {

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	static void DrawLinks(CocoonTrigger trigger, GizmoType type)
    {
        if (trigger.m_LinkedCocoons == null)
            return;

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(trigger.transform.position, trigger.GetComponent<SphereCollider>().radius - .2f);
        Handles.Label(trigger.transform.position, trigger.gameObject.name);
        

        for (int i = 0; i < trigger.m_LinkedCocoons.Length; i++)
        {
            CocoonSpawner spawner = trigger.m_LinkedCocoons[i];
            if (spawner != null)
            {
                Gizmos.DrawLine(trigger.transform.position, spawner.transform.position);
                Handles.Label(spawner.transform.position, spawner.name);
            }
            
        }

    }
}

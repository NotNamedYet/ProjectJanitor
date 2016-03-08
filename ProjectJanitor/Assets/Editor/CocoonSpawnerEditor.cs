using UnityEngine;
using System.Collections;
using UnityEditor;

public class CocoonSpawnerEditor : Editor {

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	static void ShowSpawnPoint(CocoonSpawner spawner, GizmoType type)
    {
        if (spawner.m_SpawnLocation == null)
        {
            Gizmos.DrawWireSphere(spawner.transform.position, 1);
        }
        else
        {
            Gizmos.DrawLine(spawner.m_SpawnLocation.position, spawner.transform.position);
            Gizmos.DrawWireSphere(spawner.m_SpawnLocation.position, 1);
            Handles.Label(spawner.m_SpawnLocation.position, "SpawnPoint");
        }
    }
}

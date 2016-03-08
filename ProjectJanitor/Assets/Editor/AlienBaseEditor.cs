using UnityEngine;
using System.Collections;
using UnityEditor;

public class AlienBaseEditor : Editor {

	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawNavigationPath(NavMeshAgent agent, GizmoType type)
    {
        if (agent.path == null)
            return;

        Gizmos.color = Color.red;
        Vector3[] points = agent.path.corners;

        for (int i = 0; i < points.Length -1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }
}

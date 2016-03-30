using UnityEngine;
using System.Collections;

public class Cursor_Modified : MonoBehaviour {

    public bool m_customCursor = false;
    public Texture2D m_cursor;

    // Use this for initialization
    void Start () {
        if (m_customCursor)
            Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);
    }

}

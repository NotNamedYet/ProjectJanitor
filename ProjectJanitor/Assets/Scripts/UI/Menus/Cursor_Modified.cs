using UnityEngine;
using System.Collections;

public class Cursor_Modified : MonoBehaviour {

    public Texture newCursor;
    public int cursorSizeX = 20;
    public int cursorSizeY = 20;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;	
	}
	
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSizeX / 2, Event.current.mousePosition.y - cursorSizeY / 2, cursorSizeX, cursorSizeY), newCursor);
    }
}

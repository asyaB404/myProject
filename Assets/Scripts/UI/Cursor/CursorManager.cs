using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture_Normal;
    public Texture2D cursorTexture_Click;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture_Normal, Vector2.zero, cursorMode);
    }

    public void Update()
    {
        if(UIManager.Instance.currentState != UIState.GamePlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDown();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    public void MouseDown()
    {
        Cursor.SetCursor(cursorTexture_Click, Vector2.zero, cursorMode);
    }

    public void MouseUp()
    {
        Cursor.SetCursor(cursorTexture_Normal, Vector2.zero, cursorMode);
    }
}

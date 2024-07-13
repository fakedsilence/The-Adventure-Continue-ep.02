using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransionFromLargeToAwuNight : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite

    private bool hasEnteredTrigger = false;
    public SceneSwitcher sceneSwitcher;
    public static bool isOnce = true;  //只让场景转化一次

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && hasEnteredTrigger && isOnce)
        {
            GenericDialogueManager.isScrolling = true;
            sceneSwitcher.SwitchScene("AwuHomeNight");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将鼠标形状变为默认形状
            isOnce = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasEnteredTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasEnteredTrigger = false;
    }
}

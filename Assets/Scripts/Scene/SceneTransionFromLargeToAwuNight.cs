using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransionFromLargeToAwuNight : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite

    private bool hasEnteredTrigger = false;
    public SceneSwitcher sceneSwitcher;
    public static bool isOnce = true;  //ֻ�ó���ת��һ��

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
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //�������״��ΪĬ����״
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

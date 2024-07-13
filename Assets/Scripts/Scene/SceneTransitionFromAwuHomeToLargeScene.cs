using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionFromAwuHomeToLargeScene : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite
    public SceneSwitcher sceneSwitcher;
    private bool hasEnteredTrigger = false;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasEnteredTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasEnteredTrigger = false;
    }

    private void OnMouseUpAsButton()
    {
        if (hasEnteredTrigger && GenericDialogueManager.isScrolling == false)
        {
            GenericDialogueManager.isScrolling = true;
            SceneManager.LoadSceneAsync("LargeScene").completed += operation => SwitchBack();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将鼠标形状变为默认形状
        }
    }

    private void SwitchBack()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        if (sceneSwitcher != null)
        {
            sceneSwitcher.SwitchBack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionBehindMyHome : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite

    private bool hasEnteredTrigger = false;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUpAsButton()
    {
        if(hasEnteredTrigger && PickedUpItem.pickedupitemNum == 3 && GenericDialogueManager.isScrolling == false)
        {
            GenericDialogueManager.isScrolling = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将鼠标形状变为默认形状
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

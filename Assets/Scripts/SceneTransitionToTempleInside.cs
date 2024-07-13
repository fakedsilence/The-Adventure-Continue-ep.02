using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionToTempleInside : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite

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
        if (hasEnteredTrigger && BuringObect.isTransition)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //�������״��ΪĬ����״
        }
    }
}

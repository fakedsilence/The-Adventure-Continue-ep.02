using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionFromAwuNightToLargeSceneNight : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasEnteredTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasEnteredTrigger = false;
    }

    private void Update()
    {
        Debug.Log(GenericDialogueManager.isScrolling);
    }

    private void OnMouseUpAsButton()
    {
        if (hasEnteredTrigger && isOnce && GenericDialogueManager.isScrolling == false)
        {
            isOnce = false;
            if (hasEnteredTrigger && SceneTransionFromLargeToAwuNight.isOnce == false && SceneTransitionFromLargeSceneNightToChief.isOnce == false)
            {
                GenericDialogueManager.isScrolling = true;
                SceneManager.LoadScene("SceneTemple");
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将鼠标形状变为默认形状
                return;
            }
            GenericDialogueManager.isScrolling = true;
            SceneManager.LoadSceneAsync("LargeSceneNight").completed += operation => SwitchBack();
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

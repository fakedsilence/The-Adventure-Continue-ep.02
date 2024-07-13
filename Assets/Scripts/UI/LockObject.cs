using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockObject : MonoBehaviour
{
    public GameObject Box;

    private bool istriggered = false;

    public GameObject Panel;

    public PickedUpItem Item;

    public AudioSource pickedAudio;

    private bool isOnce = true;

    public Texture2D cursorSprite;

    public AudioSource lockaudio;

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
        istriggered = true;
    }

    private void OnMouseUpAsButton()
    {
        if(istriggered && isOnce)
        {
            Box.SetActive(true);
        }
    }

    private void Update()
    {
        if(MoveSystem.finish && isOnce)
        {
            lockaudio.Play();
            StartCoroutine(PickedUpItemEffect());
        }
    }

    IEnumerator PickedUpItemEffect()
    {
        isOnce = false;
        Panel.SetActive(true);
        pickedAudio.Play();
        UIInventoryPage.Instance.Add(Item);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        SceneManager.LoadScene("MirrorWorld");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BuringObect : MonoBehaviour
{
    public AudioSource paper;

    public Texture2D cursorSprite;

    public GameObject buringPanel;

    public Image sticksImage1;
    public Image sticksImage2;
    public Image sticksImage3;
    public Image sticksImage4;

    public Sprite[] sticks1;
    public Sprite[] sticks2;
    public Sprite[] sticks3;
    public Sprite[] sticks4;

    private bool isTriggered = false;

    public GameObject panel;

    private bool once = true;

    public static bool isTransition = false;

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
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && buringPanel.activeSelf)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = buringPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                buringPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
            }
        }
        if (sticksImage1.sprite == sticks1[0] && sticksImage2.sprite == sticks2[2]
            && sticksImage3.sprite == sticks3[0] && sticksImage4.sprite == sticks4[3] && once)
        {
            StartCoroutine(ViewEffect());
            once = false;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (isTriggered)
        {
            PlayerMovement.Movespeed = 0f; // Ω˚÷π»ÀŒÔ“∆∂Ø   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.EnableIsBackAnimation();
            PlayerMovement.Instance.DisableInput();
            PlayerMovement.Instance.animator.SetBool("IsWalking", false);
            buringPanel.SetActive(true);
        }
    }

    public void ChangeSticksImageSprite1()
    {
        Image image = sticksImage1.GetComponent<Image>();
        Sprite currentSprite = image.sprite;
        string currentSpriteName = currentSprite.name;
        int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
        spriteIndex = (spriteIndex + 1) % sticks1.Length;
        Sprite newSprite = sticks1[spriteIndex];
        image.sprite = newSprite;
    }

    public void ChangeSticksImageSprite2()
    {
        Image image = sticksImage2.GetComponent<Image>();
        Sprite currentSprite = image.sprite;
        string currentSpriteName = currentSprite.name;
        int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
        spriteIndex = (spriteIndex + 1) % sticks1.Length;
        Sprite newSprite = sticks1[spriteIndex];
        image.sprite = newSprite;
    }

    public void ChangeSticksImageSprite3()
    {
        Image image = sticksImage3.GetComponent<Image>();
        Sprite currentSprite = image.sprite;
        string currentSpriteName = currentSprite.name;
        int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
        spriteIndex = (spriteIndex + 1) % sticks1.Length;
        Sprite newSprite = sticks1[spriteIndex];
        image.sprite = newSprite;
    }

    public void ChangeSticksImageSprite4()
    {
        Image image = sticksImage4.GetComponent<Image>();
        Sprite currentSprite = image.sprite;
        string currentSpriteName = currentSprite.name;
        int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
        spriteIndex = (spriteIndex + 1) % sticks1.Length;
        Sprite newSprite = sticks1[spriteIndex];
        image.sprite = newSprite;
    }

    IEnumerator ViewEffect()
    {
        panel.SetActive(true);
        paper.Play();
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        isTransition = true;
    }
}

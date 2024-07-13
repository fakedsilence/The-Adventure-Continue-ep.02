using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxObject : MonoBehaviour
{
    public Texture2D cursorSprite;
    public GameObject lockPanel;
    public Sprite[] locks1;
    public Sprite[] locks2;
    public Sprite[] locks3;
    public Image lockImage1;
    public Image lockImage2;
    public Image lockImage3;

    public GameObject Child;

    public Animator childAnimator;

    private bool isTriggered = false;

    public static bool isTransition = false;

    private bool isOnce = false;

    public AudioSource doorLock;

    public AudioSource Unlock;

    public bool isLocked = true;

    private bool playOnce = true;

    private void Start()
    {
        Child.SetActive(false);
        childAnimator = Child.GetComponent<Animator>();
    }

    private void Update()
    {
        if (lockImage1.sprite == locks1[3] && lockImage2.sprite == locks2[4] && lockImage3.sprite == locks3[2] && playOnce)
        {
            if (playOnce)
            {
                Unlock.Play();
                playOnce = false;
            }
            isLocked = false;
            doorLock.enabled = false;
            StartCoroutine(WaitSeconds());
        }
        if (Input.GetMouseButtonDown(0) && lockPanel.activeSelf)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = lockPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                lockPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
            }
        }
        if(isOnce)
        {
            Debug.Log(1);
            Child.SetActive(true);
            childAnimator.SetBool("IsFade", true);
            isTransition = true;
            isOnce = false;
        }
    }

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

    private void OnMouseUpAsButton()
    {
        if (isTriggered)
        {
            PlayerMovement.Movespeed = 0f; // Ω˚÷π»ÀŒÔ“∆∂Ø   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.EnableIsBackAnimation();
            PlayerMovement.Instance.DisableInput();
            PlayerMovement.Instance.animator.SetBool("IsWalking", false);
            lockPanel.SetActive(true);
        }
    }

    public void ChangeLockImageSprite1()
    {
        if (isLocked)
        {
            doorLock.Play();
            Image image = lockImage1.GetComponent<Image>();
            Sprite currentSprite = image.sprite;
            string currentSpriteName = currentSprite.name;
            int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
            spriteIndex = (spriteIndex + 1) % locks1.Length;
            Sprite newSprite = locks1[spriteIndex];
            image.sprite = newSprite;
        }
    }

    public void ChangeLockImageSprite2()
    {
        if (isLocked)
        {
            doorLock.Play();
            Image image = lockImage2.GetComponent<Image>();
            Sprite currentSprite = image.sprite;
            string currentSpriteName = currentSprite.name;
            int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
            spriteIndex = (spriteIndex + 1) % locks2.Length;
            Sprite newSprite = locks2[spriteIndex];
            image.sprite = newSprite;
        }
    }

    public void ChangeLockImageSprite3()
    {
        if (isLocked)
        {
            doorLock.Play();
            Image image = lockImage3.GetComponent<Image>();
            Sprite currentSprite = image.sprite;
            string currentSpriteName = currentSprite.name;
            int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
            spriteIndex = (spriteIndex + 1) % locks3.Length;
            Sprite newSprite = locks3[spriteIndex];
            image.sprite = newSprite;
        }
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1f);
        lockPanel.SetActive(false);
        PlayerMovement.Movespeed = 5f;
        PlayerMovement.Instance.EnableInput();
        PlayerMovement.Instance.DisableIsBackAnimation();
        isOnce = true;
    }
}

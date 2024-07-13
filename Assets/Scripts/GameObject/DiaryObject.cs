using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryObject : MonoBehaviour
{
    public Texture2D cursorSprite;
    private bool isTriggered = false;

    public GameObject diaryPanel;

    public Sprite[] diarySprite;

    public Image diaryImage;

    public AudioSource bookFlip;

    private bool isCooldown = false;

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
        if (Input.GetMouseButtonDown(0) && diaryPanel.activeSelf)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = diaryPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                diaryPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (isTriggered)
        {
            PlayerMovement.Movespeed = 0f; // 禁止人物移动   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.EnableIsBackAnimation();
            PlayerMovement.Instance.DisableInput();
            PlayerMovement.Instance.animator.SetBool("IsWalking", false);
            diaryPanel.SetActive(true);
        }
    }

    public void ChangeLockImageSprite1()
    {
        bookFlip.Play();
        Image image = diaryImage.GetComponent<Image>();
        Sprite currentSprite = image.sprite;
        string currentSpriteName = currentSprite.name;
        int spriteIndex = int.Parse(currentSpriteName.Substring(currentSpriteName.Length - 1));
        spriteIndex = (spriteIndex + 1) % diarySprite.Length;
        Sprite newSprite = diarySprite[spriteIndex];
        image.sprite = newSprite;
    }

    public void OnButtonClick()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            StartCoroutine(ChangeLockImageSpriteWithCooldown());
        }
    }

    private IEnumerator ChangeLockImageSpriteWithCooldown()
    {
        // 冷却时间为1秒
        const float cooldownTime = 0.5f;

        // 等待冷却时间
        yield return new WaitForSeconds(cooldownTime);

        // 重置标志变量
        isCooldown = false;

        // 调用方法
        ChangeLockImageSprite1();
    }

}

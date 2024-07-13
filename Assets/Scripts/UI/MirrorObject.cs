using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorObject : MonoBehaviour
{
    public GameObject mirrorPanel;
    public GameObject image;
    public Sprite mirrorBreak;
    public Texture2D cursorSprite;
    private Image mirrorImage;

    private bool mirroring = false;
    public AudioSource breakAudioSource;

    private bool isTriggered = false;

    private bool showOnce = true;

    private void Start()
    {
        mirrorPanel.SetActive(false);
        mirrorImage = image.GetComponent<Image>();
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mirrorPanel.activeSelf && mirroring)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = mirrorPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                mirrorPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if(isTriggered)
        {
            PlayerMovement.Movespeed = 0f; // Ω˚÷π»ÀŒÔ“∆∂Ø   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.EnableIsBackAnimation();
            PlayerMovement.Instance.DisableInput();
            PlayerMovement.Instance.animator.SetBool("IsWalking", false);
            mirrorPanel.SetActive(true);
            if (showOnce)
            StartCoroutine(Break());
        }
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(2f);
        mirrorImage.sprite = mirrorBreak;
        breakAudioSource.Play();
        yield return new WaitForSeconds(1f);
        mirroring = true;
        showOnce = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewObject : MonoBehaviour
{
    public GameObject ViewPanel;

    public Texture2D cursorSprite;

    private bool isTriggered = false;

    private void Start()
    {
        ViewPanel.SetActive(false);
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
        if (Input.GetMouseButtonDown(0) && ViewPanel.activeSelf)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = ViewPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                ViewPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
                PlayerMovement.Instance.animator.SetBool("IsWalking", false);
            }
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
            ViewPanel.SetActive(true);
        }
    }
}



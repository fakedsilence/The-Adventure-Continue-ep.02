using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject_Hair : MonoBehaviour
{
    public Texture2D cursorSprite;

    public bool addOnce = true;

    public bool isPickingUpItem = false;

    public GameObject interactPanel;

    private string itemNameToFind = "����"; // Ҫ���ҵ���Ʒ����

    public PickedUpItem Hairpin;

    public Animator animator;

    public GameObject pickedupPanel;

    public AudioSource fire;

    public GameObject Hair;

    public static bool Ontriggered = false;

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
        Ontriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ontriggered = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && interactPanel.activeSelf && addOnce)
        {
            Vector2 clickPosition = Input.mousePosition;
            RectTransform panelRect = interactPanel.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, clickPosition))
            {
                interactPanel.SetActive(false);
                PlayerMovement.Movespeed = 5f;
                PlayerMovement.Instance.EnableInput();
                PlayerMovement.Instance.DisableIsBackAnimation();
                isPickingUpItem = false;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (addOnce && !isPickingUpItem && Ontriggered && !InteractiveObject.isCoroutineRunning && !PickedUpItem.isPickingUpItem)
        {
            PlayerMovement.Instance.EnableIsBackAnimation();
            isPickingUpItem = true;
            //PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;
            interactPanel.SetActive(true);
            PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (PickedUpItem.ispickedupCandy)
                // �ƶ���ɺ�ִ���ٳ�����ɾ������
                StartCoroutine(PickedUpEffects());
        }
    }

    IEnumerator PickedUpEffects()
    {
        addOnce = false;
        animator.SetBool("IsBurn", true);
        fire.Play();
        yield return new WaitForSeconds(3f);

        UIInventoryPage.Instance.Add(Hairpin);
        Hairpin.audiosource.Play();
        PickedUpItem.pickedupitemNum++;
        pickedupPanel.SetActive(true);
        interactPanel.SetActive(false);
        yield return new WaitForSeconds(2f);
        isPickingUpItem = false;
        pickedupPanel.SetActive(false);
        PlayerMovement.Instance.EnableInput();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //�������״���û�Ĭ����״
        //PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;
        PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
        Hair.SetActive(false);
        PlayerMovement.Instance.DisableIsBackAnimation();
    }
}
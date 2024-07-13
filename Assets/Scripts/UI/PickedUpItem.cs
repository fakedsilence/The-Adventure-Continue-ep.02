using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PickedUpItem : MonoBehaviour
{
    public string title;  //��Ʒ���Ƶ�Textmeshpro���

    [TextArea(1, 15)]
    public string description;  //��Ʒ������TextMeshPro���

    public int quantity;

    public Sprite image;

    public Texture2D cursorSprite;

    public bool addOnce = true;

    public static bool isPickingUpItem = false;

    public GameObject panel;

    public static int pickedupitemNum = 0;

    private UIInventoryPage inventoryPage;

    public static bool ispickedupCandy = false;

    public AudioSource audiosource;

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
        if (addOnce && !isPickingUpItem)
        {
            isPickingUpItem = true;
            addOnce = false;
            PlayerMovement player = PlayerMovement.Instance;
            Vector3 target = transform.position;
            player.MoveToTarget(target, () =>
            {
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;
                // �ƶ���ɺ�ִ���ٳ�����ɾ������
                StartCoroutine(PickedUpEffects());
            });
        }
    }

    IEnumerator PickedUpEffects()
    {
        PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�   
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log(1);
        audiosource.Play(); 
        panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        PlayerMovement.Instance.EnableInput();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //�������״���û�Ĭ����״
        if (this.title == "����")
            ispickedupCandy = true;
        UIInventoryPage.Instance.Add(this);
        pickedupitemNum++;
        gameObject.SetActive(false);
        isPickingUpItem = false;
        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;
        PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
    }
}
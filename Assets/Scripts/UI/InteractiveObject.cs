    using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite
    public GameObject commentBox;  //�Ի���Ķ���

    [SerializeField]
    private float displayTime = 2.0f;  //�Ի�����ʧ��ʱ��

    [SerializeField]
    private string commetLines;  //�Ի����е�ע��

    public TextMeshProUGUI commentText;  //�ı��Ķ���

    [SerializeField]
    private float textSpeed = 0.05f;  //�ַ������ļ��ʱ��

    bool isScrolling = true;  //ֻ������ʾһ��
    bool isGenerated = false;  //ֻ��ʾһ�ζԻ���

    public static bool isCoroutineRunning = false;  //ֻ��Э������һ��

    bool isInteracting = false;

    private void Start()
    {
        commentBox.SetActive(false);
    }

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
        if (!isCoroutineRunning && !isGenerated && !isInteracting && !PickedUpItem.isPickingUpItem)
        {
            PlayerMovement.Instance.EnableIsBackAnimation(); // ���ű�������
            PickedUpItem.isPickingUpItem = true;
            OnInteract();
            isCoroutineRunning = true;
            isInteracting = true;  // ��ʼ����
            PhotoDialogue.isRunning = true;
        }
    }

    public void OnInteract()
    {
        PlayerMovement player = PlayerMovement.Instance;
        Vector3 target = transform.position;
        player.MoveToTarget(target, () =>
        {
            // �ƶ���ɺ�ִ����ʾ�Ի���͹����ı�
            if (!isGenerated)
            {
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;
                commentBox.SetActive(true);
                StartCoroutine(ShowCommentBox());
                if (isScrolling)
                {
                    StartCoroutine(ScrollingText());
                    isScrolling = false;
                }
                isGenerated = true;
                
            }
        });
    }

    // ��ʾ�ı���
    IEnumerator ShowCommentBox()
    {
        PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(displayTime);
        PlayerMovement.Instance.EnableInput();  // �����������
        PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
        PlayerMovement.Instance.DisableIsBackAnimation();// ������������
        
        Debug.Log(1);
        commentBox.SetActive(false);
        isGenerated = false;
        isInteracting = false;  // ��������
        PhotoDialogue.isRunning = false;
        Debug.Log(2);
        isCoroutineRunning = false;
        PickedUpItem.isPickingUpItem = false;
        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  
    }

    // �����ı�
    IEnumerator ScrollingText()
    {
        commentText.text = "";

        foreach (char letter in commetLines)
        {
            commentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // �������������ó� false
        isScrolling = false;
    }
}

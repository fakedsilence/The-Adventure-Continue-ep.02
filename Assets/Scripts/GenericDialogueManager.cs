using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GenericDialogueManager : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite
    public GameObject dialogueBox;  //�Ի���Ķ���

    [SerializeField]
    private string[] dialogueLines;  //�Ի����е�ע��

    public TextMeshProUGUI nameText;  // ����
    public TextMeshProUGUI dialogueText;  //�ı��Ķ���

    [SerializeField]
    private float textSpeed = 0.02f;  //�ַ������ļ��ʱ��

    public static bool isScrolling = true;  //һ������ ֻ������ʾһ��
    bool isGenerated = false;  //ֻ��ʾһ�ζԻ���

    public static bool isRunning = false;  //ֻ��Э������һ��

    private int currentLine = 0;

    private bool istrue = true; //��ֹһ�л�û�������͵����������һ��

    private void Start()
    {
        dialogueBox.SetActive(false);
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
        
        if (!isRunning && !PickedUpItem.isPickingUpItem)
        {
            OnInteract();
            isRunning = true;
            PickedUpItem.isPickingUpItem = true;
        }
    }
    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (istrue == false)  //��ֹһ�жԻ���û����� �͵����������һ��
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);
                        PlayerMovement.Instance.DisableIsBackAnimation(); // ֹͣ���ű�������
                        PlayerMovement.Instance.EnableInput();  // �����������
                        PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
                        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  //���ýŲ���
                        isGenerated = false;
                        isRunning = false;
                        InteractiveObject.isCoroutineRunning = false;
                        PickedUpItem.isPickingUpItem = false;
                    }
                }
            }
        }
    }

    public void OnInteract()
    {
        InteractiveObject.isCoroutineRunning = true;
        PlayerMovement player = PlayerMovement.Instance;
        Vector3 target = transform.position;
        player.MoveToTarget(target, () =>
        {
            // �ƶ���ɺ�ִ����ʾ�Ի���͹����ı�
            if (!isGenerated)
            {
                PlayerMovement.Instance.EnableIsBackAnimation(); // ���ű�������
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //���ýŲ���
                PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�
                PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                dialogueBox.SetActive(true);
                if (isScrolling)
                {
                    StartCoroutine(ScrollingText());
                    isScrolling = false;
                }
                isGenerated = true;
            }
        });
    }

    // �����ı�
    IEnumerator ScrollingText()
    {
        istrue = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // �������������ó� false
        isScrolling = false;

        istrue = false;
    }
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", ""); //�Ƴ���������ǰ��n-
            currentLine++;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite
    public GameObject commentBox;  //�Ի���Ķ���

    [SerializeField]
    private string[] commetLines;  //�Ի����е�ע��

    public TextMeshProUGUI commentText;  //�ı��Ķ���

    [SerializeField]
    private float textSpeed;  //�ַ������ļ��ʱ��

    bool isScrolling = true;  //ֻ������ʾһ��
    bool isGenerated = false;  //ֻ��ʾһ�ζԻ���

    private bool isRunning = false;  //ֻ��Э������һ��

    private int currentLine = 0;

    private AudioSource audiosource;

    private bool istrue = false; //��ֹһ�л�û�������͵����������һ��

    public AudioSource pickedupTelephone;

    private void Awake()
    {
        DisableScriptsByTag("Disable");
    }
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
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
        if(!isRunning)
        {
            OnInteract();
            isRunning = true;
        }
    }
    private void Update()
    {
        if (commentBox.activeInHierarchy)
        {
            if(Input.GetMouseButtonUp(0))
            {
                if (istrue == false)  //��ֹһ�жԻ���û����� �͵����������һ��
                {
                    currentLine++;
                    if (currentLine < commetLines.Length)
                    {
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        commentBox.SetActive(false);
                        EnableScriptsByTag("Disable");
                        PlayerMovement.Instance.DisableIsBackAnimation(); // ֹͣ���ű�������
                        PlayerMovement.Instance.EnableInput();  // �����������
                        PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
                        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  //���ýŲ���
                        isGenerated = false;
                    }
                }
            }
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
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //���ýŲ���
                audiosource.Stop();
                pickedupTelephone.Play();
                PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�
                PlayerMovement.Instance.EnableIsBackAnimation(); // ���ű�������s
                PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                
                commentBox.SetActive(true);
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
        commentText.text = "";

        foreach (char letter in commetLines[currentLine])
        {
            commentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // �������������ó� false
        isScrolling = false;

        istrue = false;
    }

    // ����ָ����ǩ�����нű�
    void DisableScriptsByTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
        InteractiveObject.isCoroutineRunning = true;
        PickedUpItem.isPickingUpItem = true;

    }

    // ����ָ����ǩ�����нű�
    void EnableScriptsByTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }
        InteractiveObject.isCoroutineRunning = false;
        PickedUpItem.isPickingUpItem = false;
    }
}
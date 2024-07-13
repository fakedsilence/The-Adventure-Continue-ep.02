using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorFade : MonoBehaviour
{
    public Texture2D cursorSprite;  //���sprite

    private Material fadeMaterial;

    [SerializeField]
    private float fadeSpeed = 5f;

    private bool isFade = false;

    public GameObject dialogueBox;  //�Ի���Ķ���

    [SerializeField]
    private string[] dialogueLines;  //�Ի����е�ע��

    public TextMeshProUGUI nameText;  // ����
    public TextMeshProUGUI dialogueText;  //�ı��Ķ���

    [SerializeField]
    private float textSpeed = 0.02f;  //�ַ������ļ��ʱ��

    private int currentLine = 0;

    public GameObject obj;

    private bool isOnce = true;

    public bool isScrolling = true;  //һ������ ֻ������ʾһ��

    private bool showOnce = true;

    public GameObject ExceptObject;

    private bool ShowOnce = true;

    private void Start()
    {
        obj.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


    private void Update()
    {
        var renderer = GetComponent<Renderer>();
        fadeMaterial = renderer.material;
        if (isFade)
        {
            fadeMaterial.SetFloat("_Num", Mathf.Lerp(fadeMaterial.GetFloat("_Num"), 0f, fadeSpeed * Time.deltaTime));
        }
        if (fadeMaterial.GetFloat("_Num") < 0.01)
        {
            isFade = false;
            if (showOnce)
            {
                dialogueBox.SetActive(true);
                obj.SetActive(true);
                showOnce = false;
            }
        }

        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0) && dialogueText.text == dialogueLines[currentLine])
            {
                ExceptObject.SetActive(false);
                currentLine++;
                if (currentLine < dialogueLines.Length)
                {
                    CheckName();
                    StartCoroutine(ScrollingText());
                }
                else
                {
                    ExceptObject.SetActive(true);
                    dialogueBox.SetActive(false);
                    obj.SetActive(false);
                    Debug.Log(obj);
                    PlayerMovement.Instance.EnableInput();  // �����������
                    PlayerMovement.Movespeed = 5f; // �������ڿ����ƶ�
                    PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  //���ýŲ���
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOnce)
        {
            OnInteract();
            isOnce = false;
        }
    }

    private void OnInteract()
    {
        PlayerMovement player = PlayerMovement.Instance;
        Vector3 target = transform.position;
        player.MoveToTarget(target, () =>
        {
            Debug.Log(1);
            // �ƶ���ɺ�ִ����ʾ�Ի���͹����ı�
            PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //���ýŲ���
            PlayerMovement.Movespeed = 0f; // ��ֹ�����ƶ�
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.DisableInput();
            isFade = true;
        });
    }

    IEnumerator ScrollingText()
    {
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // �������������ó� false
        isScrolling = false;
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
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorFade : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite

    private Material fadeMaterial;

    [SerializeField]
    private float fadeSpeed = 5f;

    private bool isFade = false;

    public GameObject dialogueBox;  //对话框的对象

    [SerializeField]
    private string[] dialogueLines;  //对话框中的注释

    public TextMeshProUGUI nameText;  // 名字
    public TextMeshProUGUI dialogueText;  //文本的对象

    [SerializeField]
    private float textSpeed = 0.02f;  //字符滚动的间隔时间

    private int currentLine = 0;

    public GameObject obj;

    private bool isOnce = true;

    public bool isScrolling = true;  //一个场景 只滚动显示一次

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
                    PlayerMovement.Instance.EnableInput();  // 启用输入控制
                    PlayerMovement.Movespeed = 5f; // 人物现在可以移动
                    PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  //启用脚步声
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
            // 移动完成后执行显示对话框和滚动文本
            PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //禁用脚步声
            PlayerMovement.Movespeed = 0f; // 禁止人物移动
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

        // 滚动结束后设置成 false
        isScrolling = false;
    }
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", ""); //移除人物名称前的n-
            currentLine++;
        }
    }
}
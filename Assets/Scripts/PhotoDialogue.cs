using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotoDialogue : PickedUpItem
{
    public GameObject dialogueBox;  //对话框的对象

    [SerializeField]
    private string[] dialogueLines;  //对话框中的注释

    public TextMeshProUGUI nameText;  // 名字
    public TextMeshProUGUI dialogueText;  //文本的对象

    [SerializeField]
    private float textSpeed = 0.02f;  //字符滚动的间隔时间

    public static bool isScrolling = true;  //一个场景 只滚动显示一次
    bool isGenerated = false;  //只显示一次对话框

    public static bool isRunning = false;  //只让协程运行一次

    private int currentLine = 0;

    private bool istrue = true; //防止一行还没有输出完就点击鼠标输出下一行

    public AudioSource photoSource;

    private bool photoOnce = true;

    public GameObject cropsPanel;

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
        if (!isRunning && photoOnce)
        {
            PhotoedItem.isPhotoed = true;
            isRunning = true;
            OnInteract();
        }
    }
    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (istrue == false)  //防止一行对话还没输出完 就点击鼠标输出下一行
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
                        StartCoroutine(PickedUpEffects());
                        isGenerated = false;
                        InteractiveObject.isCoroutineRunning = false;
                        photoOnce = false;
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
            // 移动完成后执行显示对话框和滚动文本
            if (!isGenerated)
            {
                PlayerMovement.Instance.EnableIsBackAnimation(); // 播放背部动画
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //禁用脚步声
                PlayerMovement.Movespeed = 0f; // 禁止人物移动
                PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                dialogueBox.SetActive(true);
                if (isScrolling)
                {
                    StartCoroutine(ScrollingText());
                    isScrolling = false;
                    GenericDialogueManager.isScrolling = false;
                    PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;
                }
                isGenerated = true;
            }
        });
    }

    // 滚动文本
    IEnumerator ScrollingText()
    {
        istrue = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // 滚动结束后设置成 false
        isScrolling = false;

        istrue = false;
    }
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", ""); //移除人物名称前的n-
            currentLine++;
        }
    }

    IEnumerator PickedUpEffects()  //完成添加物体的效果
    {
        PlayerMovement.Movespeed = 0f; // 禁止人物移动
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        panel.SetActive(true);
        photoSource.Play();
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        PlayerMovement.Instance.EnableInput();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将光标形状设置回默认形状
        audiosource.Play();
        UIInventoryPage.Instance.Add(this);
        pickedupitemNum++;
        isPickingUpItem = false;
        PhotoedItem.isPhotoed = false;
        cropsPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        cropsPanel.SetActive(false);
        PlayerMovement.Movespeed = 5f; // 人物现在可以移动
        PlayerMovement.Instance.DisableIsBackAnimation(); // 停止播放背部动画
        isRunning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite
    public GameObject commentBox;  //对话框的对象

    [SerializeField]
    private string[] commetLines;  //对话框中的注释

    public TextMeshProUGUI commentText;  //文本的对象

    [SerializeField]
    private float textSpeed;  //字符滚动的间隔时间

    bool isScrolling = true;  //只滚动显示一次
    bool isGenerated = false;  //只显示一次对话框

    private bool isRunning = false;  //只让协程运行一次

    private int currentLine = 0;

    private AudioSource audiosource;

    private bool istrue = false; //防止一行还没有输出完就点击鼠标输出下一行

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
                if (istrue == false)  //防止一行对话还没输出完 就点击鼠标输出下一行
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
                        PlayerMovement.Instance.DisableIsBackAnimation(); // 停止播放背部动画
                        PlayerMovement.Instance.EnableInput();  // 启用输入控制
                        PlayerMovement.Movespeed = 5f; // 人物现在可以移动
                        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  //启用脚步声
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
            // 移动完成后执行显示对话框和滚动文本
            if (!isGenerated)
            {
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;  //禁用脚步声
                audiosource.Stop();
                pickedupTelephone.Play();
                PlayerMovement.Movespeed = 0f; // 禁止人物移动
                PlayerMovement.Instance.EnableIsBackAnimation(); // 播放背部动画s
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
    // 滚动文本
    IEnumerator ScrollingText()
    {
        istrue = true;
        commentText.text = "";

        foreach (char letter in commetLines[currentLine])
        {
            commentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // 滚动结束后设置成 false
        isScrolling = false;

        istrue = false;
    }

    // 禁用指定标签的所有脚本
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

    // 启用指定标签的所有脚本
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
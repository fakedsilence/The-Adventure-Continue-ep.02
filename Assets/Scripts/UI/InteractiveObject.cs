    using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Texture2D cursorSprite;  //光标sprite
    public GameObject commentBox;  //对话框的对象

    [SerializeField]
    private float displayTime = 2.0f;  //对话框消失的时间

    [SerializeField]
    private string commetLines;  //对话框中的注释

    public TextMeshProUGUI commentText;  //文本的对象

    [SerializeField]
    private float textSpeed = 0.05f;  //字符滚动的间隔时间

    bool isScrolling = true;  //只滚动显示一次
    bool isGenerated = false;  //只显示一次对话框

    public static bool isCoroutineRunning = false;  //只让协程运行一次

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
            PlayerMovement.Instance.EnableIsBackAnimation(); // 播放背部动画
            PickedUpItem.isPickingUpItem = true;
            OnInteract();
            isCoroutineRunning = true;
            isInteracting = true;  // 开始交互
            PhotoDialogue.isRunning = true;
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

    // 显示文本框
    IEnumerator ShowCommentBox()
    {
        PlayerMovement.Movespeed = 0f; // 禁止人物移动
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(displayTime);
        PlayerMovement.Instance.EnableInput();  // 启用输入控制
        PlayerMovement.Movespeed = 5f; // 人物现在可以移动
        PlayerMovement.Instance.DisableIsBackAnimation();// 结束背部动画
        
        Debug.Log(1);
        commentBox.SetActive(false);
        isGenerated = false;
        isInteracting = false;  // 结束交互
        PhotoDialogue.isRunning = false;
        Debug.Log(2);
        isCoroutineRunning = false;
        PickedUpItem.isPickingUpItem = false;
        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;  
    }

    // 滚动文本
    IEnumerator ScrollingText()
    {
        commentText.text = "";

        foreach (char letter in commetLines)
        {
            commentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // 滚动结束后设置成 false
        isScrolling = false;
    }
}

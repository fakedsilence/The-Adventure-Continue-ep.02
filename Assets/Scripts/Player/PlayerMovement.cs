using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float Movespeed = 5f;
    private Rigidbody2D rb;
    public float moveInput;

    private static PlayerMovement instance;

    private bool isInputEnabled = true;

    public AudioSource audioSource; // 声明AudioSource变量

    public float turnDelay = 0.2f; // 表示角色转向时的延迟时间

    private float lastTurnTime; // 表示上一次转向的时间

    float x;

    public Animator animator;

    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
            }
            return instance;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // 初始化AudioSource组件
        animator = GetComponent<Animator>();
    }

    public void Movement()
    {

        if (!isInputEnabled) return; // 如果输入控制被禁用，退出方法

        moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * Movespeed, rb.velocity.y);
        if (moveInput > 0)
        {
            animator.SetBool("IsWalking", true);
            // 添加转向延迟
            if (Time.time - lastTurnTime > turnDelay)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                lastTurnTime = Time.time;
            }
        }
        else if (moveInput < 0)
        {
            animator.SetBool("IsWalking", true);
            // 添加转向延迟
            if (Time.time - lastTurnTime > turnDelay)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                lastTurnTime = Time.time;
            }
        }
        else
            animator.SetBool("IsWalking", false);

        if (Mathf.Abs(moveInput) > 0 && !audioSource.isPlaying) // 如果角色正在移动且音频剪辑未被播放
        {
            audioSource.Play();
            audioSource.volume = 1; // 播放音频剪辑
        }
        else if (Mathf.Abs(moveInput) == 0 && audioSource.isPlaying) // 如果角色停止移动且音频剪辑正在播放
        {
            //将音频逐渐减小为0 避免出现杂音
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, 0.1f).OnComplete(() =>
            {
                audioSource.Stop();
            });
        }
    }

    public void MoveToTarget(Vector3 Target, Action onTweenComplete = null)
    {
        animator.SetBool("IsWalking", true);
        audioSource.volume = 1;
        // 在移动前禁用输入控制
        DisableInput();

        float duration = Mathf.Abs(Target.x - this.transform.position.x) / Movespeed * 2;
        if (Target.x >= this.transform.position.x)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        // 使用 DOTween 播放移动动画，并在动画结束时调用 onTweenComplete 回调函数
        transform.DOMoveX(Target.x, duration).OnComplete(() =>
        {
            animator.SetBool("IsWalking", false);
            onTweenComplete?.Invoke();
        }).OnUpdate(() =>
        {
            // 移动过程中播放脚步声音
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }).OnKill(() =>
        {
            // 停止播放脚步声音
            audioSource.Stop();
        });
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //启用输入控制
    public void EnableInput()
    {
        isInputEnabled = true;
    }

    //禁用输入控制
    public void DisableInput()
    {
        isInputEnabled = false;
    }

    public void EnableIsBackAnimation()
    {
        animator.SetBool("IsBack", true);
    }

    public void DisableIsBackAnimation()
    {
        animator.SetBool("IsBack", false);
    }
}
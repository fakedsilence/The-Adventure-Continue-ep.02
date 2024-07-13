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

    public AudioSource audioSource; // ����AudioSource����

    public float turnDelay = 0.2f; // ��ʾ��ɫת��ʱ���ӳ�ʱ��

    private float lastTurnTime; // ��ʾ��һ��ת���ʱ��

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
        audioSource = GetComponent<AudioSource>(); // ��ʼ��AudioSource���
        animator = GetComponent<Animator>();
    }

    public void Movement()
    {

        if (!isInputEnabled) return; // ���������Ʊ����ã��˳�����

        moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * Movespeed, rb.velocity.y);
        if (moveInput > 0)
        {
            animator.SetBool("IsWalking", true);
            // ���ת���ӳ�
            if (Time.time - lastTurnTime > turnDelay)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                lastTurnTime = Time.time;
            }
        }
        else if (moveInput < 0)
        {
            animator.SetBool("IsWalking", true);
            // ���ת���ӳ�
            if (Time.time - lastTurnTime > turnDelay)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                lastTurnTime = Time.time;
            }
        }
        else
            animator.SetBool("IsWalking", false);

        if (Mathf.Abs(moveInput) > 0 && !audioSource.isPlaying) // �����ɫ�����ƶ�����Ƶ����δ������
        {
            audioSource.Play();
            audioSource.volume = 1; // ������Ƶ����
        }
        else if (Mathf.Abs(moveInput) == 0 && audioSource.isPlaying) // �����ɫֹͣ�ƶ�����Ƶ�������ڲ���
        {
            //����Ƶ�𽥼�СΪ0 �����������
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
        // ���ƶ�ǰ�����������
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

        // ʹ�� DOTween �����ƶ����������ڶ�������ʱ���� onTweenComplete �ص�����
        transform.DOMoveX(Target.x, duration).OnComplete(() =>
        {
            animator.SetBool("IsWalking", false);
            onTweenComplete?.Invoke();
        }).OnUpdate(() =>
        {
            // �ƶ������в��ŽŲ�����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }).OnKill(() =>
        {
            // ֹͣ���ŽŲ�����
            audioSource.Stop();
        });
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //�����������
    public void EnableInput()
    {
        isInputEnabled = true;
    }

    //�����������
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
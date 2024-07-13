using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoedItem : PickedUpItem
{
    public AudioSource photoEffect;
    private bool PhotoOnce = true;

    public static bool isPhotoed = false;

    public GameObject cropsPanel;

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
        if (addOnce && !isPickingUpItem && PhotoOnce && !isPhotoed)
        {
            PlayerMovement.Instance.EnableIsBackAnimation(); // 播放背部动画
            isPickingUpItem = true;
            addOnce = false;
            PhotoDialogue.isRunning = true;

            PlayerMovement player = PlayerMovement.Instance;
            Vector3 target = transform.position;
            player.MoveToTarget(target, () =>
            {
                PlayerMovement.Instance.GetComponent<AudioSource>().enabled = false;
                // 移动完成后执行再场景中删除物体
                StartCoroutine(PickedUpEffects());
                PhotoOnce = false;
            });
        }
    }

    IEnumerator PickedUpEffects()  //完成添加物体的效果
    {
        PlayerMovement.Movespeed = 0f; // 禁止人物移动
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        panel.SetActive(true);
        photoEffect.Play();
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        PlayerMovement.Instance.EnableInput();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  //将光标形状设置回默认形状
        UIInventoryPage.Instance.Add(this);
        audiosource.Play();
        pickedupitemNum++;
        isPickingUpItem = false;
        PhotoDialogue.isRunning = false;
        isPhotoed = true;
        cropsPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        cropsPanel.SetActive(false);
        PlayerMovement.Instance.GetComponent<AudioSource>().enabled = true;
        PlayerMovement.Movespeed = 5f; // 人物现在可以移动
        PlayerMovement.Instance.DisableIsBackAnimation();
    }
}

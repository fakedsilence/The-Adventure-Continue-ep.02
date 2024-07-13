using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MajiangObject : MonoBehaviour
{
    public Texture2D cursorSprite;
    public GameObject majiangPanel;
    public Animator animator;

    public GameObject Box;

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
        PlayerMovement player = PlayerMovement.Instance;
        Vector3 target = transform.position;
        player.MoveToTarget(target, () =>
        {
            PlayerMovement.Movespeed = 0f; // Ω˚÷π»ÀŒÔ“∆∂Ø   
            PlayerMovement.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.Instance.EnableIsBackAnimation();
            majiangPanel.SetActive(true);
            StartCoroutine(MajiangEffect());
        });
    }

    IEnumerator MajiangEffect()
    {
        animator.SetBool("Majiang", true);
        yield return new WaitForSeconds(2);
        majiangPanel.SetActive(false);
        PlayerMovement.Movespeed = 5f;
        PlayerMovement.Instance.EnableInput();
        Box.SetActive(true);
        PlayerMovement.Instance.DisableIsBackAnimation();
    }
}

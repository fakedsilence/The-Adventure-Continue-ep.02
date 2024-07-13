using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [TextArea(1, 8)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private bool isScrolling = false;
    [SerializeField] private float textSpeed;

    private bool ShowOnce = true;

    public GameObject END;

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
        END.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && ShowOnce)
        {
            dialogueBox.SetActive(true);
            if (isScrolling == false)
            {
                currentLine++;

                if (currentLine < dialogueLines.Length)
                {
                    CheckName();
                    StartCoroutine(ScrollingText());
                }
                else
                {
                    if (InventoryController.isInitialize)
                    {
                        ShowOnce = false;
                        dialogueBox.SetActive(false);  //BOX HIDE
                        END.SetActive(true);
                    }
                }
            }
        }
    }

    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", ""); //移除人物名称前的n-
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;  //SHOW EACH WORD
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
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

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
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
                        ChangeScene();
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

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
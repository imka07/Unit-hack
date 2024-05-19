using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTitles : MonoBehaviour
{
    [SerializeField] private Text dialogTxt, helpText;
    [SerializeField] private float dialogTxtSpeed;
    [SerializeField] private string[] lines;
    private int index;

    private void Start()
    {
        dialogTxt.text = string.Empty;
    }

    public void NextMessage()
    {
        if (dialogTxt.text == lines[index])
        {
            IsNextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogTxt.text = lines[index];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.instance.battariesCount == 3)
            {
                GameManager.instance.StartTitles();
                StartDialog();
            }
            else
            {
                StartCoroutine(HelpText());
            }
        }
    }

    IEnumerator HelpText()
    {
        helpText.text = "Недостаточно модулей";
        yield return new WaitForSeconds(2.5f);
        helpText.text = string.Empty;
    }


    public void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogTxt.text += c;
            yield return new WaitForSeconds(dialogTxtSpeed);
        }
    }

    void IsNextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogTxt.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            GameManager.instance.ReturnToMenu();
        }
    }
}

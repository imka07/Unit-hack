using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monologe : MonoBehaviour
{
    [SerializeField] private Text dialogTxt;
    [SerializeField] private float dialogTxtSpeed;
    [SerializeField] private string[] lines;
    private int index;
    MenuManager menuManager;

    private void Start()
    {
        dialogTxt.text = string.Empty;
        menuManager = GetComponent<MenuManager>();
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
    
    public void  StartDialog()
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
            menuManager.ToGame();
        }
    }
}

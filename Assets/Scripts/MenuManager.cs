using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private Animator SettingsAnim;

    public void ToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
        SettingsAnim.SetTrigger("Start");
    }

    public void CLoseSettings()
    {
        SettingsAnim.SetTrigger("End");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


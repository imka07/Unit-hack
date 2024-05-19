using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel, Tiltles;
    [SerializeField] private Animator SettingsAnim;
    Monologe monologe;


    private void Start()
    {
        monologe = GetComponent<Monologe>();
    }

    public void StartTitles()
    {
        Tiltles.SetActive(true);
        monologe.StartDialog();
    }

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


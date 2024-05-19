using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject pausePanel, crossHair, diePanel;
    public bool isGameActive = true;
    GameObject player;
    public int battariesCount = 3;
    [SerializeField] private Text battariesText;
    public GameObject titlesPanel;

    [SerializeField] private AudioSource batteryPickUp;
    [SerializeField] Image hpFill;

    void OpenPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameActive = false;
            pausePanel.SetActive(true);
            crossHair.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void AddBattaries()
    {
        battariesCount += 1;
        battariesText.text = battariesCount.ToString() + "/3";
        batteryPickUp.Play();
    }

    public void ClosePause()
    {
            isGameActive = true;
            pausePanel.SetActive(false);
            crossHair.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
    }

    public void StartTitles()
    {
        titlesPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Awake()
    {
        instance = this;
        battariesCount = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerManager>().OnHpChange += UpdateHp;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        OpenPause();
    }

    public void GameOver()
    {
        isGameActive = false;
        diePanel.SetActive(true);
        crossHair.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UpdateHp(float maxHp, float currentHp)
    {
        hpFill.fillAmount = currentHp / maxHp;
    }
}

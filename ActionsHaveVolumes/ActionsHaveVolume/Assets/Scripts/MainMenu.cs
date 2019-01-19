using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TMP_Dropdown difficulty;

    private void Start()
    {
        difficulty.value = PlayerPrefs.GetInt("Difficulty", 1);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Difficulty", 1));
        if (difficulty.value == 0)
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }

        if (difficulty.value == 1)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }

        if (difficulty.value == 2)
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

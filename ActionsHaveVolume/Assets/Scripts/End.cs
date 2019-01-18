using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public int Atime;
    public int Btime;
    public int Ctime;
    public int Dtime;
    public int Acoins;
    public int Bcoins;
    public int Ccoins;
    public int Dcoins;
    public int Fcoins;
    public int timer;
    private int min;
    private int sec;
    private int prevTotal;

    private float time;

    public GameObject endScreen;

    public Text timeText;
    public Text moneyEarned;
    public Text moneyFromTime;
    public Text total;

    public MissionGiver giver;
    public CharMovement player;

    

    void Start()
    {
        //PlayerPrefs.SetInt("Total", 0);
        //PlayerPrefs.SetInt("Level", 0);
    }

    void Update()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
        if (!giver.compeate && giver.ready2go)
        {
            time += 1 * Time.deltaTime;
            timer = Mathf.RoundToInt(time);
        }
        if (giver.compeate)
        {
            sec = timer % 60;
            min = timer / 60;
            timeText.text = min.ToString() + ":" + sec.ToString();
        }
    }

    public void Show()
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
        moneyEarned.text = player.money.ToString();
        
        if (timer >= Dtime)
        {
            moneyFromTime.text = Fcoins.ToString();
            PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + player.money + Fcoins);
            Debug.Log("F");
        }else if (timer >= Ctime)
        {
            moneyFromTime.text = Dcoins.ToString();
            PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + player.money + Dcoins);
            Debug.Log("D");
        }
        else if (timer >= Btime)
        {
            moneyFromTime.text = Ccoins.ToString();
            PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + player.money + Ccoins);
            Debug.Log("C");
        }
        else if (timer >= Atime)
        {
            moneyFromTime.text = Bcoins.ToString();
            PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + player.money + Bcoins);
            Debug.Log("B");
        }
        else
        {
            moneyFromTime.text = Acoins.ToString();
            PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + player.money + Acoins);
            Debug.Log("A");
        }
        total.text = PlayerPrefs.GetInt("Total", 0).ToString();
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("Total", prevTotal);
        SceneManager.LoadScene(1);
    }
    public void Next()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
        SceneManager.LoadScene(2);
        
    }
}

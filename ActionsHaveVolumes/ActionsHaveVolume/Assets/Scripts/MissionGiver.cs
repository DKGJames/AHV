using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionGiver : MonoBehaviour {
    
    //How many total enemies there are 
    public int enemies;
    //How many total boss stages there are 
    public int bosses;

    //Wether or not the player can move 
    public bool ready2go;
    //If all of the enemies are destroyed
    public bool bossReady;
    public bool compeate;
    public bool isActive = true;

    public GameObject quest;

    public Text enemyCount;
    public Text bossesCount;

    void Update () {

        bossesCount.text = bosses.ToString();
        enemyCount.text = enemies.ToString();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isActive)
            {
                Time.timeScale = 1;
                isActive = false;
                quest.SetActive(false);
                ready2go = true;
            }else if (!isActive)
            {
                Time.timeScale = 0;
                isActive = true;
                quest.SetActive(true);
            }
        }

        if (bosses <= 0)
        {
            compeate = true;
        }

        //Checks if all the enemys are destroyed
        if (enemies <= 0)
        {
            bossReady = true;
        }
	}

    //Removing dead enemys
    public void EnemyKilled()
    {
        enemies--;
    }
    //Removing dead enemys
    public void BossKilled()
    {
        bosses--;
    }
}

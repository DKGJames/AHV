using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Heath of the enemy
    public int[] heath;
    //Range of how much gold the enemy drops
    public int[] minGold;
    public int[] maxGold;
    public static int difficulty;

    //The ammount that the enemy is pushed back when it is hit
    public float pushForce;

    //Tells wich way the player was attacking 
    private GameObject player;
    //Gold to drop when the enemy dies
    public GameObject gold;

    //Adds the force
    private Rigidbody2D rb;

    //Ticks of the enemy on the players hit list
    public MissionGiver removeEnemy;

    //Sets what the player is
    void Start()
    {
        //Sets all of the components on the enemy
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        difficulty = PlayerPrefs.GetInt("Difficulty", 1);
    }

	void LateUpdate ()
    {
        //Checks if the enemys health is zero
		if (heath[difficulty] <= 0)
        {
            //Marks enemy as killed and removes it from the quest
            removeEnemy.EnemyKilled();

            ////Gets a random number on how much gold is dropped
            int goldOutput = Random.Range(minGold[difficulty], maxGold[difficulty]);
            for (int i = 0; i < goldOutput; i++)
            {
                //Instantiates coins and launches them in random directions
                GameObject coin = Instantiate(gold, transform.position, Quaternion.identity);
                int direction = Random.Range(0, 360);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0, 0) * 65 * Time.deltaTime);
            }
            //Destroys enemy  
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        //Marks enemy as killed and removes it from the quest
        removeEnemy.EnemyKilled();

        ////Gets a random number on how much gold is dropped
        int goldOutput = Random.Range(minGold[difficulty], maxGold[difficulty]);
        for (int i = 0; i < goldOutput; i++)
        {
            //Instantiates coins and launches them in random directions
            GameObject coin = Instantiate(gold, transform.position, Quaternion.identity);
            int direction = Random.Range(0, 360);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0, 0) * 65 * Time.deltaTime);
        }
        //Destroys enemy  
        Destroy(gameObject);
    }

    //Removes health and bounces the enemy
    public void TakeDamage(int damage)
    {
        //Removes health
        heath[difficulty] -= damage;
        //Pushes the enemy based on where the player is
        if (player.transform.position.x > transform.position.x)
        {
            rb.AddForce(Vector2.left * pushForce * Time.deltaTime * 1000);
        }
        else
        {
            rb.AddForce(Vector2.right * pushForce * Time.deltaTime * 1000);
        }
    }
}

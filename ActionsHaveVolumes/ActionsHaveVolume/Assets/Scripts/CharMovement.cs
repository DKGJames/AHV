using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharMovement : MonoBehaviour {

    //How much health the player has
    public int health = 10;
    //How much money the player has
    public int money;

    //How much he gets pushed back when he hits and enemy
    public float pushForce;
    public float velShake;
    public float velocity;
    //Tells wich way the player wants to move the enemy
    private float horizontalMove;
    private float prevPos;

    //Wether or not the player is jumping
    private bool jump;
    private bool grounded;

    //For pushing the player
    private Rigidbody2D rb;

    //Makes sure the player gets the mission
    public MissionGiver isReady;
    //Controlls player movement
    private CharacterController2D controller;
    
    //Shows the payers health
    public Slider healthSlider;

    //Allows the player to do different animations
    public Animator knightAnim;
    public Animator camAnim;

    void Start()
    {
        //Gets components that are on the player
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }

    void Update () {

        //Sets the health slider to reflect the players health 
        healthSlider.value = health;

        //Checks if the player has gotten the mission
        if (isReady.ready2go)
        {
            //Sets wich way the player is turning to wich button you are pushing
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Moves sets the direction to left
                horizontalMove = -1;
                //Turns the player around
                transform.eulerAngles = new Vector3(0, 180, 0);
                //Starts the run animation on the player
                knightAnim.SetBool("Run", true);          
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //Moves sets the direction to right
                horizontalMove = 1;
                //Turns the player around
                transform.eulerAngles = new Vector3(0, 0, 0);
                //Starts the run animation on the player
                knightAnim.SetBool("Run", true);
            }
            else
            {
                //Stops he player
                horizontalMove = 0;
                //Stops the run animation on the player
                knightAnim.SetBool("Run", false);
            }

            //Checks if you are jumping
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump = true;
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].Destroy();
                }
            }
            velocity = Mathf.Abs(transform.position.y - prevPos);
            prevPos = transform.position.y;
    
        }

        //Kills the player if his heath is zero
        if (health <= 0)
        {
            //Reloads the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate()
    {
        //Sends the users input to be prossed and used to move the player 
        controller.Move(horizontalMove, false, jump);
        //Allows the player to jump again
        jump = false;
    }

    //Allows the enemy to remove player health
    public void MinusHealth(int rate, float x)
    {
        //Decreases the players health
        health -= rate;
        camAnim.SetTrigger("Shake");
        //Bounces the player away from the enemy
        if (x > transform.position.x)
        {
            rb.AddForce(Vector2.left * pushForce * Time.deltaTime * 1000);
        }
        else
        {
            rb.AddForce(Vector2.right * pushForce * Time.deltaTime * 1000);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Checks if you run ino a coin
        if (coll.gameObject.CompareTag("coin"))
        {
            //Destroys the coin
            Destroy(coll.gameObject);
            //Adds money to the player
            money++;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (velocity >= velShake)
        {
            camAnim.SetTrigger("Shake");
            Debug.Log("Shake");
        }    
    }
}

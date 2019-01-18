using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    public int prevHealth;
    public int[] dammage;

    //How fast it moves
    public float speed;

    //States wich way the slime is moving
    private bool movingRight = true;

    //Allows to add the bounce beck force
    private Rigidbody2D rb;

    //Tells the slime when to turn 
    public Transform groundDetection;

    //Allows to remove heath
    private Enemy enemyScript;

    public Animator anim;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        prevHealth = enemyScript.heath[Enemy.difficulty];
    }

    void Update () {
        if (enemyScript.heath[Enemy.difficulty] < prevHealth)
        {
            anim.SetTrigger("Hit");
        }
        prevHealth = enemyScript.heath[Enemy.difficulty];

        //Moves the slime continuesly
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Shoots down a ray to make sure the slime is on the ground
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2);
        //If the ray dosent hit anything
        if (groundInfo.collider == false)
        {
            //Turns it based on wich direction it is facing
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        int hitPlayer = 0;
        //Checks if it hits the enviorment to rotate it
        if (coll.gameObject.CompareTag("enviorment")) 
        {
            Debug.Log("Hit Wall");
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
         
        //Checks if it hit the player to damage him
        if (coll.gameObject.tag == "Player")
        {
            hitPlayer++;      
        }

        if (hitPlayer > 0)
        {
            coll.gameObject.GetComponent<CharMovement>().MinusHealth(dammage[Enemy.difficulty], transform.position.x);
            Debug.Log("Player Damage");
        } 
    }  
}

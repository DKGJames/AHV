using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadKnight : MonoBehaviour {

    //How much damage the weapon does 
    public int[] attackDamage;
    //Allows to check if the enemy has been hurt
    private int prevHealth;
    
    //The range of the weapon 
    public float attackRange;
    //Waits before the enemy attacks again
    public float timeBtwAttack;
    //How fast the enemy moves
    public float speed;

    //Wich way the enemy is facing
    private bool movingRight = true;
    //If the waitime is over and the enemy can attack
    private bool canSwing = true;

    //Where the attack range is drawn at
    public Transform attackPos;
    //Where the enemy turning point is
    public Transform groundDetaction;
    
    //Allows the pushBack
    private Rigidbody2D rb;
    
    //Allows to remove heath
    private Enemy enemyScript;

    //Changes to hurt animation when the enemy is hit
    public Animator anim;

    //Chacks if what you hit is an enemy
    public LayerMask whatIsPlayer;

    //Box collider for the sword, removed when swinging
    public BoxCollider2D swordCollider;

    void Start()
    {
        //Sets components
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        //Sets the previos heath to show if the health goes down
        prevHealth = enemyScript.heath[Enemy.difficulty];
    }

    void Update()
    {
        /*Checks if the enemy loses heath and if it has 
        it plays a hurt animation and turns the enemy*/
        if (enemyScript.heath[Enemy.difficulty] < prevHealth)
        {
            anim.SetTrigger("Hit");
            StartCoroutine(Turn());
        }
        //Sets the previos heath to show if the health goes down
        prevHealth = enemyScript.heath[Enemy.difficulty];

        //Moves the enemy constantly
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Sends down a ray to se if the enemy is at the edge
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetaction.position, Vector2.down, 2);
        
        //Turns the enemy if it reaches the edge
        if (groundInfo.collider == false)
        {
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

        //Checks if you can punch
        if (canSwing)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
            //Checks if the player is in the attack radius
            if (player.Length > 0)
            {
                //Removes the collider on the sword
                swordCollider.enabled = false;
                //Plays the attack animation
                anim.SetTrigger("Attack");
                //Does damage to the player
                player[0].GetComponentInParent<CharMovement>().MinusHealth(attackDamage[Enemy.difficulty], transform.position.x);
                //Adds back to collider on the sword
                swordCollider.enabled = true;
                //Starts the waittime for the enemy to attack again
                canSwing = false;
                StartCoroutine(Attack());         
            }   
        }     
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Checks if the enemy his a wall and if it has, to turn around
        if (coll.gameObject.CompareTag("enviorment"))
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    IEnumerator Attack()
    {
        //Waits a short time and lets the enemy attack again
        yield return new WaitForSeconds(timeBtwAttack);
        canSwing = true;
    }

    IEnumerator Turn()
    {
        //Turns around after a short ammount of time
        yield return new WaitForSeconds(0.5f);

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

    //Draws the range of attack
    void OnDrawGizmosSelected()
    {
        //drawns punch range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

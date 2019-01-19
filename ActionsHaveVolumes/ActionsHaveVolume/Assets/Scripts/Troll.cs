using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{

    //!!!!IN PROGRESS, NEEDS TO CHANGE ATTACK FROM JUST TOUCHING THE PLAYER, TO MEELEE!!!!\\

    //How much damage the weapon does 
    public int attackDamage;
    //Allows to check if the enemy has been hurt
    private int prevHealth;

    //How fast it moves
    public float speed;
    //The point where the enemy can turn
    public float turnyPos;
    //The range of the weapon 
    public float attackRange;
    //Waits before the enemy attacks again
    public float timeBtwAttack;

    //States wich way the slime is moving
    private bool movingRight = true;
    //Checks if the enemy can start turning
    public bool canTurn;
    //If the waitime is over and the enemy can attack
    private bool canSwing = true;

    //Next button to go to the endscreen
    public GameObject next;

    //Allows the enemy to be bounced back
    private Rigidbody2D rb;

    //Allows to remove heath
    private Boss enemyScript;

    //Allows the enemy to see if it is ready to be dropped
    public MissionGiver giver;

    //Tells the slime when to turn 
    public Transform groundDetection;
    //Where the attack range is drawn at
    public Transform attackPos;

    //Allows the enemy to become invisible
    public SpriteRenderer render;

    //Changes to hurt animation when the enemy is hit
    public Animator anim;

    //Chacks if what you hit is an enemy
    public LayerMask whatIsPlayer;

    //Box collider for the sword, removed when swinging
    public EdgeCollider2D swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Gets all of the components on the enemy
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {

        /*Checks if the enemy loses heath and if it has 
        it plays a hurt animation and turns the enemy*/
        if (enemyScript.heath < prevHealth)
        {
            StartCoroutine(Turn());
            prevHealth = enemyScript.heath;
        }

        //Sets the previos heath to show if the health goes down
        prevHealth = enemyScript.heath;

        //If it is time for the boss stage
        if (giver.bossReady)
        {
            //Makes the enemy visible
            rb.simulated = true;
            //Adds physics and drops it from the sky
            render.enabled = true;
        }

        //If the enemy has fallen far enough start turning
        if (transform.position.y <= turnyPos)
        {
            //Lets the enemy turn
            canTurn = true;
        }

        //If the enemy can turn
        if (canTurn)
        {
            //Moves the enemy continuesly
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        //Shoots down a ray to see if the enemy has reaches the edge
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2);
        //If enemy is at the edge
        if (groundInfo.collider == false)
        {
            //If the enemy can turn 
            if (canTurn)
            {
                //Turns it based on wich direction it is facing
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
                player[0].GetComponentInParent<CharMovement>().MinusHealth(attackDamage, transform.position.x);
                //Adds back to collider on the sword
                swordCollider.enabled = true;
                //Starts the waittime for the enemy to attack again
                canSwing = false;
                StartCoroutine(Attack());
            }
        }

        //If the health is zero
        if (enemyScript.heath <= 0)
        {
            //Sets the button active to go to the endscreen
            next.SetActive(true);
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
        Debug.Log("Turning");
        //Turns around after a short ammount of time
        yield return new WaitForSeconds(0.6f);

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

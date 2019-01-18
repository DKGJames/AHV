using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {  
    
    //How much damage the weapon does 
    public int swordSlashDamage;
    //Checks what weapon you are using
    public int weapon;
    
    //The range of the weapon 
    public float swordSlashRange;

    //Where the range is drawn at
    public Transform swordSlashPos;

    //Changes animations
    public Animator knightAnim;
    public Animator camAnim;

    //Chacks if what you hit is an enemy
    public LayerMask whatIsEnemy;
    public LayerMask whatIsChest;

    void Update () {
        //Checks if you want to attack
        if (Input.GetMouseButtonDown(0))
        {
            if (weapon == 0)
            {
                //Starts the attack animation
                knightAnim.SetTrigger("Slash");
                //Checks if there are any enemies in the attack range
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(swordSlashPos.position, swordSlashRange, whatIsEnemy);
                //Checks if there are any chests in the attack range
                Collider2D[] chestsToDamage = Physics2D.OverlapCircleAll(swordSlashPos.position, swordSlashRange, whatIsChest);
                if (enemiesToDamage.Length > 0)
                {
                    camAnim.SetTrigger("Shake");
                    //If there are any enemys in the area, it does damage to them
                    if (enemiesToDamage[0].GetComponent<Enemy>() != null)
                    {
                        enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(swordSlashDamage);
                    }
                    //If there are any bosses in the area, it does damage to them
                    else if (enemiesToDamage[0].GetComponent<Boss>() != null)
                    {
                        enemiesToDamage[0].GetComponent<Boss>().TakeDamage(swordSlashDamage);
                    }   
                } else if (chestsToDamage.Length > 0)
                {
                    camAnim.SetTrigger("Shake");
                    //If there are any enemys in the area, it does damage to them
                    chestsToDamage[0].GetComponent<Chest>().DestroyChest(swordSlashDamage);
                }
            }
        }
	}

    //Draws the range of attack
    void OnDrawGizmosSelected()
    {
        if (weapon == 0)
        {
            //Shows a visual for the attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(swordSlashPos.position, swordSlashRange);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    public int crowCount;

    public float minFlyX;
    public float maxFlyX;
    public float minFlyY;
    public float maxFlyY;
    public CharMovement player;

    public GameObject crow;
    
    void Start()
    {
        for (int i = 0; i < crowCount; i++)
        {
            Crow();
        }
    }

    void Crow()
    {
        float x = Random.Range(minFlyX, maxFlyX);
        float y = Random.Range(minFlyY, maxFlyY);
        Instantiate(crow, new Vector2(x, y), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Kill enemy");
            coll.GetComponent<Enemy>().Destroy();
        }
        if (coll.gameObject.CompareTag("coin"))
        {
            Destroy(coll.gameObject);
            player.money++;
        }
        if (coll.gameObject.name == "Crow NPC(Clone)")
        {
            Destroy(coll.gameObject);
            Crow();
        }

        if (coll.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }
}

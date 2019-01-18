using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int heath;
    public int maxCoin;
    public int minCoin;

    public GameObject gold;

    void Update()
    {
        if (heath <= 0)
        {
            int goldOutput = Random.Range(minCoin, maxCoin);
            for (int i = 0; i < goldOutput; i++)
            {
                GameObject coin = Instantiate(gold, transform.position, Quaternion.identity);
                int direction = Random.Range(0, 360);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0, 0) * 65 * Time.deltaTime);
            }
            Destroy(gameObject);
        }
    }

    public void DestroyChest(int damage)
    {
        heath -= damage;
    }
}

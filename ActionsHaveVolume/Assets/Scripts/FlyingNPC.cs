using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingNPC : MonoBehaviour
{

    public float speed;

    void Start()
    {
        StartCoroutine(Turn());
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    IEnumerator Turn()
    {
        float waitTime = Random.Range(1, 4);
        yield return new WaitForSeconds(waitTime);
        float rot = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, 0, rot);
        StartCoroutine(Turn());
    }
}

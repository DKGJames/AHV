using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public Transform player;

    private Vector3 pos;

    private void Start()
    {
        pos.z = -10;
    }

    void LateUpdate () {
        pos.x = player.position.x;
        pos.y = player.position.y;
        transform.position = pos;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public Vector2 minVector, maxVector;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}

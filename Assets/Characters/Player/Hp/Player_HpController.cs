using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HpController : MonoBehaviour
{
    GameObject player;
    float hp, maxhp, minhp;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>().gameObject;
        hp = GetComponent<Slider>().value;
        maxhp = GetComponent<Slider>().maxValue;
        minhp = GetComponent<Slider>().minValue;
        minhp = 0;
        maxhp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10f, player.transform.position.z);
    }
}

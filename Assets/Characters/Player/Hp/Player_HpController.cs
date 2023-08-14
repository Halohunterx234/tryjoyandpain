using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HpController : MonoBehaviour
{//this script is not use
    Player player;
    float barhp, barmaxhp, barminhp;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();//GetComponentInParent<Player>();
        barhp = GetComponent<Slider>().value;
        barmaxhp = GetComponent<Slider>().maxValue;
        barminhp = GetComponent<Slider>().minValue;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10f, player.transform.position.z);

    }
    
    //Set data of the hp bar
    public void Set_Values(int hp, int maxhp, int minhp)
    {
        barhp = hp; barmaxhp = maxhp; barminhp = minhp; 
    }
}

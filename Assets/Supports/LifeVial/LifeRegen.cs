using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRegen : MonoBehaviour
{
    //Stats
    public int lifeAmt;
    public float cd, maxcd;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        cd = 0;
    }
    
    private void Update()
    {
        if (maxcd == 0) return;
        if (cd < maxcd)
        {
            cd += Time.deltaTime;
            return;
        }
        cd = 0;
        if (player.hp < player.maxHp)
        {
            player.hp += lifeAmt;
            player.publicCheckHealth();
        }
    }
}

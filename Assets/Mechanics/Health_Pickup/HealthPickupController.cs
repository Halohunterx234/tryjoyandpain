using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.hp != player.maxHp || player.hp <= 0)
            {
                player.ModifyHp(1);
            }
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirFire : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    //Directional Fire Method
    //Algorithm to fire the bullet
    //Input Bullet + PlayerPos + BulletSpeed + FireDir
    protected void DirectionalFire(GameObject bullet, Vector3 firePos, float rot)
    {
        Vector3 fireDir = new Vector3(player.transform.localScale.x, 0, 0);
        Instantiate(bullet, firePos, Quaternion.Euler(0f, 0f, rot));
    }
}

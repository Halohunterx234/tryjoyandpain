using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //Template for all weapons
    //Include the necessary code like for shooting/reloading/updating/etc
    //Weapon Stats
    protected float weaponCD, weaponCDMax;
    [SerializeField]
    protected GameObject weaponProjectile;
    [SerializeField]
    protected Transform weaponFirePoint;

    //References
    GameObject player;

    private void Awake()
    {
        //Reset just in case
        weaponCD = 0;
    }

    private void Start()
    {
        weaponFirePoint = transform.Find("FirePoint");
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (weaponCD >= weaponCDMax)
        {
            weaponCD = 0;
            Fire();
        }
        else weaponCD += Time.deltaTime;
    }

    private void Fire()
    {
        Instantiate(weaponProjectile, (Vector2)weaponFirePoint.position + new Vector2(player.transform.localScale.x, 0), Quaternion.identity);
    }
}

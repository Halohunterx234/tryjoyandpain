using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cdReduceItem : MonoBehaviour
{
    [SerializeField]
    ArrayList available_weapons;
    GameObject player;

    //stats
    protected float cd_reduction;

    private void Awake()
    {
        available_weapons = new ArrayList();
        player = FindObjectOfType<Player>().gameObject;
        cd_reduction = 0.20f;
    }
    private void Start()
    {
        StartCoroutine(time());
    }

    private void Update()
    {
        //run whenever player obtains a new weapon or anything that affects their weapons


    }
    private void UpdateWeapons()
    {
        //Get a updated list of obtained weapons
        int children = player.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject child = player.transform.GetChild(i).gameObject;
            if (child != null && child.GetComponent<Weapons>())
            {
                available_weapons.Add(child);
            }
        }
    }
    //Update the max cd of the obtained weapons accordingly
    private void CDReduce()
    {
        foreach (GameObject weapon in available_weapons)
        {
            print("changing cd");
            weapon.GetComponent<Weapons>().UpdateCD(cd_reduction);
        }
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(5);
        UpdateWeapons();
        CDReduce();
    }


}

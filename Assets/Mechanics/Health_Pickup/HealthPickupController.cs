using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public AudioSource aSource;
    public AudioClip healSound;

    bool isPickedUp;

    private void Start()
    {
         aSource = GetComponent<AudioSource>();
        isPickedUp = false;
        
    }

    //when is picked up by player
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isPickedUp) return;
        if (collision.gameObject.GetComponent<Player>())
        {
            Player player = collision.gameObject.GetComponent<Player>();
            StartCoroutine(makeDingDing());
           
           
            //add health if player isnt at max health or dead
            if (player.hp != player.maxHp || player.hp <= 0)
            {
                player.ModifyHp(1);
            }
            //Destroy(this.gameObject);
        }
    }
    //play sound 
    IEnumerator makeDingDing()
    {
        isPickedUp = true;
        SpriteRenderer sr;
        sr = GetComponent<SpriteRenderer>();

        sr.color += new Color(0, 0, 0, -100);

        aSource.clip = healSound;
        aSource.Play();


        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}

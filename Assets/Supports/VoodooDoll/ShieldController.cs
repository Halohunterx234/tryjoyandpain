using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    //in charge of maintaining the shield bar ui and updating the stats accordingly
    public Slider slide;
    public Color low, high;
    public Vector3 offset;

    public int maxShield, shield;
    public float maxCD, cd;

    Transform player;
    private void Start()
    {
        offset = new Vector3(0, -0.75f, 0);
        //start off with a maxed out shield
        cd = maxCD;
        slide.gameObject.SetActive(true);
        low = Color.cyan; high = Color.cyan;
        player = this.gameObject.transform;
    }

    private void Update()
    {
        //allow the shield bar to be visible
        slide.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        print(transform.parent);
        //regen shield if cd is up
        if (cd >= maxCD)
        {
            cd = 0;
            shield = maxShield;
            SetShield();
        }
        else cd += Time.deltaTime;
    }

    //update the shield ui
    public void SetShield()
    {
        if (shield <= 0)
        {
            slide.gameObject.SetActive(false);
            return;
        }
        else if (slide.gameObject.activeSelf == false) slide.gameObject.SetActive(true);
        slide.maxValue = maxShield;
        slide.minValue = 0;
        slide.value = shield;
        slide.fillRect.GetComponentInChildren<Image>().color = low;
        //slide.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slide.normalizedValue);
    }
}

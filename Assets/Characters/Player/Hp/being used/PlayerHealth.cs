using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slide;
    public Color low;
    public Color high;
    public Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, -.5f, 0);
    }
    private void Update()
    {
        //allows the health bar to be visible
        slide.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
    //sets the health bar and updates it appropriately
    public void SetHealth(float health, float maxHealth, float minHealth)
    {
        //slide.gameObject.SetActive(health < maxHealth);
        print("health = " + health + maxHealth + minHealth);
        slide.maxValue = maxHealth;
        slide.minValue = minHealth;
        slide.value = health;
        slide.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slide.normalizedValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Buff AI (For Reference)")]
public class buff : ScriptableObject
{
    //each individual debuff will be a unique child of this class
    //each child then will be attached to a entity 
    [Range(0f, 10f)]
    public float buff_length;
    private float buff_currentLength;
    public float buff_cd;
    [Range(0f, 5f)]
    public float buff_cdMax;
    [Range(0f, 5f)]
    public int buff_damage;
    [Range(0f, 2f)]
    public float buff_speedScale;
    private float og_speed;
    public Color buff_color;

    private GameObject entity_go;
    private Entity entity;
    private SpriteRenderer sr;
    private Component buff_component;
    public void init(GameObject tobebuffed, Component buff_script)
    {
        entity_go = tobebuffed;
        entity = entity_go.GetComponent<Entity>();
        buff_cd = 0; buff_currentLength = 0;
        sr = entity_go.GetComponent<SpriteRenderer>();
        sr.color = buff_color;
        buff_component = buff_script;
    }
    public void CD_Update()
    {
        if (buff_cd >= buff_cdMax)
        {
            buff_cd = 0;
            buff_Proc();
        }
        buff_cd += Time.deltaTime;
        buff_currentLength += Time.deltaTime;
        if (buff_currentLength >= buff_length)
        {
            Disable(buff_component);
        }
    }

    public void buff_Proc()
    {
        sr.color = buff_color;
        if (buff_damage > 0)
        {
            entity.GetDamaged(buff_damage);
        }
        if (buff_speedScale != 1)
        {
            og_speed = entity.moveSpeed;
            entity.moveSpeed *= buff_speedScale;
        }
    }

    public void Disable(Component buff)
    {
        if (buff_speedScale != 1)
        {
            entity.moveSpeed = og_speed;
        }
        Destroy(buff);
    }
}

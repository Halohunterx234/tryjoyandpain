using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//type of buff
//DoT -> Damage Over Time -> Buff will only proc every CD till end of duration
//Constant -> Buff will be constant till end of duration
public enum BuffType
{
    DoT,
    Constant
}
[CreateAssetMenu(menuName = "Create new Buff AI (For Reference)")]
public class buff : ScriptableObject
{
    //each individual debuff will be a unique child of this class
    //each child then will be attached to a entity 
    private float og_speed;
    public Color buff_color;
    public BuffType buff_type;

    [Header("Stats")]
    [Range(0f, 10f)]
    public float buff_length;
    [SerializeField]
    private float buff_currentLength;
    [Range(0f, 10f)]
    public int buff_damage;
    [Range(0f, 2f)]
    public float buff_speedScale;

    [Header("FOR DoT BUFFS")]
    [Range(0f, 10f)]
    public float buff_effectDuration;
    private float buff_cd;
    [Range(0f, 5f)]
    public float buff_cdMax;



    /*
    private GameObject entity_go;
    private Entity entity;
    private SpriteRenderer sr;
    private BuffSuperClass buff_component;
    public void init(GameObject tobebuffed, BuffSuperClass buff_script)
    {
        entity_go = tobebuffed;
        entity = entity_go.GetComponent<Entity>();
        buff_cd = 0; buff_currentLength = 0;
        sr = entity_go.GetComponent<SpriteRenderer>();
        sr.color = buff_color;
        buff_component = buff_script;
    }
    public void CD_Update(float time)
    {
        if (buff_cd >= buff_cdMax)
        {
            buff_cd = 0;
            buff_Proc();
        }
        buff_cd += time;
        buff_currentLength += time;
        Debug.Log(buff_currentLength);
        if (buff_currentLength >= buff_length)
        {
            Debug.Log("disable");
            Disable(buff_component);
        }
    }

    public void buff_Proc()
    {
        sr.color = buff_color;
        if (buff_damage > 0)
        {
            entity.GetDamaged_ByBuff(buff_damage, buff_color);
        }
        if (buff_speedScale != 1)
        {
            //og_speed = entity.moveSpeed;
            entity.moveSpeed *= buff_speedScale;
            Debug.Log("entity" + entity.moveSpeed);
        }
    }

    public void Disable(BuffSuperClass buff)
    {
        if (buff_speedScale != 1)
        {
            //entity.moveSpeed = og_speed;
            entity.moveSpeed /= buff_speedScale;
            Debug.Log("entity" + entity.moveSpeed);
        }
        Destroy(buff);
    }
    */
}

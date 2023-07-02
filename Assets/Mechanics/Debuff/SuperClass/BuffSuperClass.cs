using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    IceSlow,
    Poison,
    Shocked,
    None,
}
public class BuffSuperClass : MonoBehaviour
{
    public Buffs buff;

    [Header("Buff References")]
    public buff IceDebuff;
    public buff PoisonDebuff;
    public buff ShockedDebuff;

    public buff currentBuff;

    //References
    private GameObject entity_go;
    private Entity entity;
    public SpriteRenderer sr;
    private BuffSuperClass buff_component;

    //Internal Variables to be set by the corresponding buff SO
    private float buff_length;
    public float buff_currentLength;
    public float buff_cd;
    private float buff_cdMax;
    private float buff_effectDuration;
    private int buff_damage;
    private float buff_speedScale;
    private float og_speed;
    private Color buff_color;
    private BuffType buff_type;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        print(sr);
    }

    // Update is called once per frame
    void Update()
    {
       CD_Update(Time.deltaTime);
    }

    public void init_buff(buff buff_ref)
    {
        currentBuff = buff_ref;
        //extract data
        buff_length = buff_ref.buff_length;
        buff_cdMax = buff_ref.buff_cdMax;
        buff_damage = buff_ref.buff_damage;
        buff_speedScale = buff_ref.buff_speedScale;
        buff_color = buff_ref.buff_color;
        buff_type = buff_ref.buff_type;
        buff_effectDuration = buff_ref.buff_effectDuration;
        init(this.gameObject, this);
    }

    public void init(GameObject tobebuffed, BuffSuperClass buff_script)
    {
        //input extracted data into this script's variables
        //reset cooldowns
        entity_go = tobebuffed;
        entity = entity_go.GetComponent<Entity>();
        buff_cd = 0; buff_currentLength = 0;
        //start the debuff immediately from t=0s if buff type is constant;
        if (buff_type == BuffType.Constant) buff_Proc();
        buff_component = buff_script;
    }
    public void CD_Update(float time)
    {
        //set the color of the entity to the color of the debuff
        if (sr.color != buff_color) sr.color = buff_color;
        //proc buff when the cooldown is up
        if (buff_type == BuffType.DoT && buff_cd >= buff_cdMax)
        {
            buff_cd = 0;
            buff_Proc();
        }
        buff_cd += time;
        buff_currentLength += time;
        //when the buff is up, disable it
        if (buff_currentLength >= buff_length)
        {
            Debug.Log("disable");
            Disable(this, true);
        }
    }

    //activates the effects of the buff;
    public void buff_Proc()
    {
        print(buff_type);
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        //if (sr.color != buff_color) sr.color = buff_color;
        if (buff_type == BuffType.DoT)
        {
            entity.GetDamaged(buff_damage, buff_color);
            StartCoroutine(resetBuff(buff_effectDuration));
        }
        if (buff_speedScale != 0 && buff_speedScale != 1)
        {
            //og_speed = entity.moveSpeed;
            entity.moveSpeed *= buff_speedScale;
            Debug.Log("entity" + entity.moveSpeed);
        }
    }

    public void Disable(BuffSuperClass buff, bool DoDestroy=false)
    {
        if (buff_speedScale != 1)
        {
            //entity.moveSpeed = og_speed;
            entity.moveSpeed /= buff_speedScale;
            Debug.Log("entity" + entity.moveSpeed);
        }
        if (DoDestroy)
        {
            sr.color = Color.white;
            Destroy(buff);
        }
    }
    IEnumerator resetBuff(float time)
    {
        yield return new WaitForSeconds(time);
        Disable(this);
    }

    //if the same buff were to be applied on the entity again, dont stack 
    //instead, just restart the buff length
    public void ResetCD()
    {
        buff_currentLength = 0;
    }
}

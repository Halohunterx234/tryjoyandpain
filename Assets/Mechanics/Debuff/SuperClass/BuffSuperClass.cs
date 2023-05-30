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

    // Start is called before the first frame update
    void Start()
    {
        currentBuff.init(this.gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {
        currentBuff.CD_Update(Time.deltaTime);
    }

    public void init_buff(buff buff_ref)
    {
        currentBuff = buff_ref;
    }
}

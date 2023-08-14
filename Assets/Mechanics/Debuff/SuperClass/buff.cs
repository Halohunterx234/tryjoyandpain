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

}

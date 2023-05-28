using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Create new Enemies")]
public class EnemiesSuperClassSO : ScriptableObject
{
    [Header("Enemies stat")]
    [Range(0f, 30f)]
    public float moveSpeed;
    [Range(0f, 50f)]
    public int health;
    [Range(0f, 30f)]
    public int collisionDmg;
    [Range(0f, 30f)]
    public int exp;
}

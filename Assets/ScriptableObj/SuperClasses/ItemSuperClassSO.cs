using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Item")]
public class ItemSuperClassSO : ScriptableObject
{
    //Variables needed for all items
    public int level; public int iProjectiledamage; public float iProjectileknockBack;//if any
    [Range(-360f, 360f)]
    public float iProjectileRot;
    public GameObject iGO; public GameObject iProjectileGO;
    public float CD; public float CDMax;
    public bool canActiviate;
    //public Transform iPosition;
    [Range(-10f, 10f)]
    public float iProjectileXOffset;
    [Range(-10f, 10f)]
    public float iProjectileYOffset;
    [Range(1f, 10f)]
    public float iProjectileDespawn;
    [Range(1f, 100f)]
    public float iProjectileSpeed;
    public int iProjectileMode;

}

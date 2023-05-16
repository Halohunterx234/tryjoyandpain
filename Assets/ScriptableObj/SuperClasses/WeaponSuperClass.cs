using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSuperClass : MonoBehaviour, Attack
{
    [SerializeField]
    private GameObject player;
    public List<ItemSuperClassSO> levels;
    [SerializeField]
    private ItemSuperClassSO iSO;
    public int level;
    [SerializeField]
    private float CD;
    [SerializeField]
    private float CDMax;
    [SerializeField]
    private Transform firePoint;

    //To be inherited by items (all items)
    public virtual void OnFire()
    {
        print("fr firing");
        GameObject Projectile = Instantiate(iSO.iProjectileGO, (Vector2)firePoint.position + new Vector2(iSO.iProjectileXOffset, iSO.iProjectileYOffset) * player.transform.localScale.x, Quaternion.identity);
        Projectiles p = Projectile.GetComponent<Projectiles>();
        //set references
        p.projectileDamage = iSO.iProjectiledamage;
        p.projectileKnockback = iSO.iProjectileknockBack;
        p.projectileSpeed = iSO.iProjectileSpeed;
        p.projectileMode = iSO.iProjectileMode;
        p.projectileDespawnTime = iSO.iProjectileDespawn;
        p.projectileRot = iSO.iProjectileRot;
        print(Projectile.transform.position);
    }
    protected void init()
    {
        player = FindObjectOfType<Player>().gameObject;
        level = 1;
        iSO = levels[level-1];
        print(levels[level-1]);
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        //iSO.iPosition = firePoint;
    }
    protected void CDUpdate()
    {
        if (CD >= CDMax)
        {
            CD = 0;
            OnFire();
        }
        else CD += Time.deltaTime;
    }
    private void Update()
    {
        
    }

    public void UpdateLevel()
    {
        print("levelled");
        level = (level >= levels.Count) ? level : level + 1;
        iSO = levels[level-1];
        UpdateData();
    }

    public void UpdateData()
    {
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        print("updated the CD");
    }


    
}

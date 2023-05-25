using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSuperClass : MonoBehaviour, Attack
{
    [SerializeField]
    private GameObject player;
    public List<ItemSuperClassSO> levels;
    [SerializeField]
    public ItemSuperClassSO iSO;
    public int level;
    [SerializeField]
    private float CD;
    [SerializeField]
    private float CDMax;
    [SerializeField]
    private Transform firePoint;

    [Header("e")]
    public ProjectileSO projectileSO;

    [Header("AI")]
    public projAI projAIMode;
    public enumfireAI fireAIMode;

    [Header("Fire AI (Reference)")]
    public FireAI fireAI;


    //To be inherited by items (all items)
    public virtual void OnFire()
    {
        //pass all information about the projectile to the fireAI method 
        fireAI.StartFire(iSO, firePoint, projAIMode, player, fireAIMode);     
    }
    protected void init()
    {
        player = FindObjectOfType<Player>().gameObject;
        level = 1;
        iSO = levels[level-1];
        print(levels[level-1]);
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        //projectileSO.init(iSO);
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
        projAIMode = iSO.projAIMode;
        fireAIMode = iSO.fireMode;
        print(fireAI);
        UpdateData();
    }

    public void UpdateData()
    {
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        print("updated the CD");
    }
}

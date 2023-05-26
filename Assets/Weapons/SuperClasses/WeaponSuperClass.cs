using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class levelproj
{
    public List<ItemSuperClassSO> projectile;
    public List<ItemSuperClassSO> get_projectiles()
    {
        return projectile;
    }
}

public abstract class WeaponSuperClass : MonoBehaviour, Attack
{
    [SerializeField]
    private GameObject player;
    public List<ItemSuperClassSO> level;
    [SerializeField]
    public ItemSuperClassSO iSO;
    public int levelNum;
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

    [Header("Levels ")]
    public List<levelproj> levels = new List<levelproj>();

    //To be inherited by items (all items)
    public virtual void OnFire()
    {
        //run through each projectile under the levels
        levelproj currentlevel = levels[levelNum-1];
        foreach (ItemSuperClassSO projectile in currentlevel.get_projectiles())
        {
            print(projectile);
            //pass all information about the projectile to the fireAI method
            fireAI.StartFire(projectile, firePoint, projectile.projAIMode, player, projectile.fireMode);
        }
        
    }
    protected void init()
    {
        //update its values with the first level's values
        player = FindObjectOfType<Player>().gameObject;
        levelNum = 1;
        //iSO = level[levelNum-1];
        //print(level[levelNum-1]);
        //CDMax = iSO.CDMax;
        //CD = iSO.CD;
        //projAIMode = iSO.projAIMode;
        //fireAIMode = iSO.fireMode;
        //projectileSO.init(iSO);
        //iSO.iPosition = firePoint;
    }

    //cd manager
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

    //updates the data 
    public void UpdateLevel()
    {
        print("levelled");
        levelNum = (levelNum >= level.Count) ? levelNum : levelNum + 1;
        //iSO = level[levelNum-1];
        //projAIMode = iSO.projAIMode;
        //fireAIMode = iSO.fireMode;
        print(fireAI);
        //UpdateData();
    }

    //updates only cd idk why and i kinda forgot why
    public void UpdateData()
    {
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        print("updated the CD");
    }
}

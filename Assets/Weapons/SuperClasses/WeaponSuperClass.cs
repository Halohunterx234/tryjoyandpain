using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create a class thats basically is a nested list -> list<list<itemsuperclassSO>> levelproj
//cuz unity cant serialize nested list (aka cant show nested stuff in inspector)
//so we convert it into a class that then can be shown
//dont ask me why but i think classes r more dynamic and memory safe
[System.Serializable]
public class levelproj
{
    //so each level is a list of projectile scriptable objects
    //thus, if you wanna make a new projectile for a specific level
    //you js gotta make it as a scritpable object n then add it
    //probabbly should make it as a folder for each level
    //cuz each individual projectile most likely will be different
    //so i will also do a big renaming change
    //pistol_level1 will become a folder and inside will be like 
    //bullet_up or bullet_up_fast or some shit like that
    //basically each SO will be a unique type with unique behaviour
    [Header("List of Projectiles (Per Level)")]
    public List<ItemSuperClassSO> projectilesList;

    
    public List<ItemSuperClassSO> get_projectiles()
    {
        return projectilesList;
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
        iSO = levels[levelNum-1].get_projectiles()[0];
        foreach (ItemSuperClassSO projectile in currentlevel.get_projectiles())
        {
            print(projectile);
            for (int i = 0; i <= projectile.iProjectileSpawnCount-1 ;i++)
            {
                //pass all information about the projectile to the fireAI method
                StartCoroutine(fireAI.StartFire(projectile.iProjectileSpawnCount, i, projectile, firePoint, projectile.projAIMode, player, projectile.fireMode, projectile.iProjectileSpawnDelay));
            }
         
        }
        
    }
    protected void init()
    {
        //update its values with the first level's values
        player = FindObjectOfType<Player>().gameObject;
        levelNum = 1;
        iSO = levels[levelNum-1].get_projectiles()[0];
        //iSO = level[levelNum-1];
        //print(level[levelNum-1]);
        CDMax = iSO.CDMax;
        CD = iSO.CD;
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
        levelNum = (levelNum >= levels.Count) ? levelNum : levelNum + 1;
        iSO = levels[levelNum-1].get_projectiles()[0];
        //projAIMode = iSO.projAIMode;
        //fireAIMode = iSO.fireMode;
        print(fireAI);
        UpdateData();
    }

    //updates only cd idk why and i kinda forgot why
    public void UpdateData()
    {
        CDMax = iSO.CDMax;
        CD = iSO.CD;
        print("updated the CD");
    }
}

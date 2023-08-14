using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Cultist : Entity
{
    //Parent Class for Cultist Enemies
    protected GameObject player;
    Vector3 playerPos;
    //public EnemiesSuperClassSO eSO;
    public AiSuperClassSO aiSO;
    public EnemiesSuperClassSO eSO;
    protected Cultist c;
    [SerializeField]
    protected float CD, CDMax;


    public void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c = GetComponent<Cultist>();
        spawnsDamageTxt = true; spawnsXpOrb = spawnsDamageTxt;
        aiSO.insertData(c,eSO);
        xp = eSO.exp;
        particleSize = eSO.particleSize;
    }


    //Simple cooldown method for any inherited members to use
    protected void CDUpdate()
    {
        if (CD >= CDMax)
        {
            CD = 0;
            OnFire();
        }
        else CD += Time.deltaTime;
    }

    public virtual void OnFire()
    {
        //run through each projectile under the levels
    }

    //Run the ai mode every frame
    private void Update()
    {
        aiSO.StartAi(player, moveSpeed, this.gameObject);
        //Chase();
        CDUpdate();
    }
}

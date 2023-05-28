using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enumAi
{
    chase,
}

[CreateAssetMenu(menuName = "Create EnemyAI (reference)")]
public class AiSuperClassSO : ScriptableObject
{
    [Header("Modes")]
    public enumAi aiMode;

    public void StartAi(GameObject player,float moveSpeed,GameObject enemy)
    {

        switch (aiMode)
        {
            case enumAi.chase:
                Chase(player,moveSpeed,enemy);
                break;
        }
    }

    public System.Action Chase(GameObject player,float moveSpeed,GameObject enemy)
    {
        if (player == null) return null;
        Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();
        Vector3 playerDir = player.transform.position - enemy.transform.position;
        enemyrb.velocity = playerDir.normalized * moveSpeed;
        //Change the rotation if necessary
        if (enemy.transform.position.x > player.transform.position.x && enemy.transform.localScale.x < 0)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
        }
        else if (enemy.transform.position.x < player.transform.position.x && enemy.transform.localScale.x > 0)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
        }
        return null;
    }
    public void insertData(Cultist c, EnemiesSuperClassSO eSO)
    {
        c.hp = eSO.health;
        c.moveSpeed = eSO.moveSpeed;
        c.collisionDmg = eSO.collisionDmg;
        c.xp = eSO.exp;
    }
}

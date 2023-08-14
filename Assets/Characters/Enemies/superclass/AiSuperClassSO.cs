using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum enumAi
{
    chase,
    shooting,
    boss,
    wave
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
            case enumAi.shooting:
                shoot(player, moveSpeed, enemy);
                break;
            case enumAi.boss:
                boss(player, moveSpeed, enemy);
                break;
            case enumAi.wave:
                boss(player, moveSpeed, enemy);
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

    public System.Action shoot(GameObject player, float moveSpeed, GameObject enemy)
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

    public System.Action boss(GameObject player, float moveSpeed, GameObject enemy)
    {
        //no movement behaviour, it is to be coded inside the boss script directly
        if (player == null) return null;
        Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D> ();
        //only change rotaton to be facing player
        Debug.Log("rotaitng");
        if (enemy.transform.position.x > player.transform.position.x && enemy.transform.localScale.x < 0)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y);
        }
        else if (enemy.transform.position.x < player.transform.position.x && enemy.transform.localScale.x > 0)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x , enemy.transform.localScale.y);
        }
        return null;
    }

    public System.Action wave(GameObject player, float moveSpeed, GameObject enemy)
    {
        if (player == null) return null;
        Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();
        enemyrb.velocity = Vector2.up * moveSpeed;
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
    public IEnumerator StartFire(float delay, bool canfire, EnemiesSuperClassSO eISO, Transform firePoint,GameObject enemy, Vector3 playerDir)
    {
        if (canfire == false) yield break;
        canfire = false;
        yield return new WaitForSeconds(delay);
        GameObject Projectile = Instantiate(eISO.eProjectileGO, (Vector2)firePoint.position + new Vector2(eISO.iProjectileXOffset, eISO.iProjectileYOffset) * Mathf.Sign(enemy.transform.localScale.x), Quaternion.identity);
        EnemyProjectile p = Projectile.GetComponent<EnemyProjectile>();
        Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
        rb.velocity = (playerDir * p.speed);
        canfire = true;
    }
    

    public void insertData(Cultist c, EnemiesSuperClassSO eSO)
    {
        c.hp = eSO.health;
        c.moveSpeed = eSO.moveSpeed;
        c.collisionDmg = eSO.collisionDmg;
        c.xp = eSO.exp;
    }
}

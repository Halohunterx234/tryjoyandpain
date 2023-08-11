using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int dmg;
    public float speed;
    public float despawnTime;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(Despawn(despawnTime));
    }

    private IEnumerator Despawn(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this);
    }
}
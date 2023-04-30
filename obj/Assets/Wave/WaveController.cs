using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    protected int waveCount;
    [SerializeField]
    protected int enemyCount;
    protected ArrayList waveList;
    [SerializeField]
    protected float waveTimer, waveMaxTimer;
    protected float distance = 10f;
    protected float maxDistance = 20f;

    public GameObject player, enemy;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (waveTimer >= waveMaxTimer)
        {
            waveTimer = 0;
            SpawnWave();
        }
        else waveTimer += Time.deltaTime;
    }

    protected void SpawnWave()
    {
        float indiv_c = enemyCount < 4 ? enemyCount : enemyCount / 4 ;
        float extra_c = enemyCount % 4 == 0 ? 0: enemyCount % 4;
        //Loop through each direction
        for (int i = 0; i < 4; i++)
        {
            for (int j = extra_c <= 0 ? 0: -1; j < indiv_c; j++)
              {
                Vector3 spawn = getSpawnDir(i);
                Instantiate(enemy, spawn, Quaternion.identity);
              }
            extra_c -= 1;
        }
    }

    private Vector3 getSpawnDir(int no)
    {
        Vector3 playerPos = player.transform.position;
        if (no == 0) return (playerPos + Vector3.up + new Vector3(Random.Range(-maxDistance, maxDistance), distance, 0));
        else if (no == 1) return (playerPos + Vector3.right + new Vector3(distance, Random.Range(-maxDistance, maxDistance), 0));
        else if (no == 2) return (playerPos + Vector3.down + new Vector3(Random.Range(-maxDistance, maxDistance), -distance, 0));
        else return (playerPos + Vector3.left + new Vector3(-distance, Random.Range(-maxDistance, maxDistance), 0));
    }
    protected void FillWave()
    {
        //fills up a arraylist with 
    }
}

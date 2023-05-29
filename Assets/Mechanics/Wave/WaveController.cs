using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    protected float timer=0;
    [SerializeField]
    protected int waveSwitched=0;


    public GameObject player, enemy;
    public GameObject fastBoi;
    public GameObject bBM;
    public GameObject bigBoi;
    public GameObject switcher;

    public TextMeshProUGUI time_text;

    float extra_c;
    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        waveTimer = waveMaxTimer;
        switcher = enemy;
        time_text = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        
        if (player == null) return;
        if (waveTimer >= waveMaxTimer)
        {
            waveTimer = 0;
            if (waveCount == 2)
            {
                enemyCount += 1;
                waveCount = 0;
            }
            waveMaxTimer = Random.Range(7.5f, 10f);
            SpawnWave();
        }
        else waveTimer += Time.deltaTime;
        if (Mathf.RoundToInt(timer) % 300 == 0 && timer >= 1)
        {
            print(Mathf.RoundToInt(timer));
            waveSwitched += 1;
            SwitchEnemy();
        }
        timer += Time.deltaTime;
        string minutes = (Mathf.RoundToInt(timer) / 60) >= 10 ? (Mathf.RoundToInt(timer) / 60).ToString() : "0" + (Mathf.RoundToInt(timer) / 60).ToString();
        string seconds = (Mathf.RoundToInt(timer) % 60) >= 10 ? (Mathf.RoundToInt(timer) % 60).ToString() : "0" + (Mathf.RoundToInt(timer) % 60).ToString();
        time_text.text = minutes + ":" + seconds;
    
    }

    protected void SpawnWave()
    {
        float indiv_c = enemyCount < 4 ? 1 : enemyCount / 4 ;
        if (enemyCount > 5) extra_c = enemyCount % 4 == 0 ? 0: enemyCount % 4;
        //Loop through each direction
        for (int i = 0; i < 4; i++)
        {
            for (int j = extra_c <= 0 ? 0: -1; j < indiv_c; j++)
              {
                Vector3 spawn = getSpawnDir(i);
                Instantiate(switcher, spawn, Quaternion.identity);
              }
            extra_c -= 1;
        }
        waveCount += 1;
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

    protected void SwitchEnemy()
    {
        if (waveSwitched % 4 == 1) switcher = fastBoi;
        if (waveSwitched % 4 == 2) switcher = bBM;
        if (waveSwitched % 4 == 3) switcher = bigBoi;
    }
}

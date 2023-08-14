using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    protected int waveCount;
    [SerializeField]
    protected int enemyCount;//amount of enemy spawned in a wave
    [SerializeField]
    protected float waveTimer, waveMaxTimer;//float used for timer of the wave
    protected float distance = 10f;//this is just distance variable for variable
    protected float maxDistance = 20f;//this is just distance variable for variable
    [SerializeField]
    protected float timer=0;

    private bool onceOnly=true;
    private float timeOfActivation;

    public GameObject player, enemy;
    public GameObject fastBoi;
    public GameObject bBM;
    public GameObject bigBoi;
    public GameObject rangeBoi;

    //bosses
    public GameObject miniboss;
    public bool miniBossSpawned;

    public TextMeshProUGUI time_text;
    public string time_text_string;
    float extra_c;
    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        waveTimer = waveMaxTimer;
        time_text = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        miniBossSpawned = false;
    }

    private void Update()
    {        
        if (player == null) return;
        timer += Time.deltaTime;
        string minutes = (Mathf.RoundToInt(timer) / 60) >= 10 ? (Mathf.RoundToInt(timer) / 60).ToString() : "0" + (Mathf.RoundToInt(timer) / 60).ToString();
        string seconds = (Mathf.RoundToInt(timer) % 60) >= 10 ? (Mathf.RoundToInt(timer) % 60).ToString() : "0" + (Mathf.RoundToInt(timer) % 60).ToString();
        time_text.text = minutes + ":" + seconds;
        time_text_string = minutes + ":" + seconds;//this is just timer text
        if (waveTimer >= waveMaxTimer)
        {
            waveTimer = 0;
            if (waveCount == 2)//for every 2 waves increase enemy count by 1
            {
                enemyCount += 1;
                waveCount = 0;//reset wave count
            }
            waveMaxTimer = Random.Range(12f, 16f);//this is where the rate of the waves is at
            SpawnWave();//this obviously spawn waves
        }
        else waveTimer += Time.deltaTime;

        if (Mathf.RoundToInt(timer) % 300 == 0 && Mathf.RoundToInt(timer) != 0 )//so for every 5 minutes the enemy count resets
        {
            if (!onceOnly)
            {
                return;
            }
            onceOnly = false;
            print(Mathf.RoundToInt(timer));
            enemyCount = 1;
            timeOfActivation = Time.time;
        }
        if (!onceOnly) CheckingActivation();  

        //spawn miniboss if its 07:00
        if ((Mathf.RoundToInt(timer) / 60) >= 1 && !miniBossSpawned)
        {
            miniBossSpawned = true;
            Instantiate(miniboss, player.transform.position + (10*Mathf.Abs(-player.transform.localScale.x) * Vector3.one), Quaternion.identity);
        }
    }

    protected void CheckingActivation()
    {
        if (Time.time >= timeOfActivation + 1f) onceOnly = true;
    }

    protected void SpawnWave()
    {
        float indiv_c = enemyCount < 4 ? 1 : enemyCount / 4 ;//indiv_c is the amount of enemy spawned
        if (enemyCount > 5) extra_c = enemyCount % 4 == 0 ? 0: enemyCount % 4;
        //if the amount of enemy spawned in each directoin is not equal put the extra in one of the direction
        for (int i = 0; i < 4; i++)//Loop through each direction
        {
            for (int j = extra_c <= 0 ? 0: -1; j < indiv_c; j++)
              {
                Vector3 spawn = getSpawnDir(i);
                GameObject selectedEnemy = EnemySpawned();
                Instantiate(selectedEnemy, spawn, Quaternion.identity);
              }
            extra_c -= 1;
        }
        waveCount += 1;
    }

    private Vector3 getSpawnDir(int no)//where the enemy will spawn
    {
        Vector3 playerPos = player.transform.position;
        if (no == 0) return (playerPos + Vector3.up + new Vector3(Random.Range(-maxDistance, maxDistance), distance, 0));// on the north of the player
        else if (no == 1) return (playerPos + Vector3.right + new Vector3(distance, Random.Range(-maxDistance, maxDistance), 0));// on the west of the player
        else if (no == 2) return (playerPos + Vector3.down + new Vector3(Random.Range(-maxDistance, maxDistance), -distance, 0));// on the south of the player
        else return (playerPos + Vector3.left + new Vector3(-distance, Random.Range(-maxDistance, maxDistance), 0));// on the east of the player
    }
    protected void FillWave()
    {
        //fills up a arraylist with 
    }

    GameObject EnemySpawned()//this is to determine what type of enemy is spawned
    {
        int value=10;//this is the maxiumum value of chance for the random chance
        int minutes = Mathf.RoundToInt(Mathf.RoundToInt(timer)/60);
        int addvalue = minutes % 5 == 0 ? 1 : minutes % 5;
        //every 1 minute the chance of getting the new enemy to spawn increases
        GameObject selectedEnemy;
        if (minutes < 5) value = 10;
        else if (minutes < 10) value += 1 * addvalue;
        else if (minutes < 15) value += 10 + 1 * addvalue;
        else if (minutes < 20) value += 20 + 1 * addvalue;
        else if (minutes < 25) value += 30 + 1 * addvalue;
        else value += 40;

        int enemyNo = Random.Range(1, value);//this is where the chance of an enemy spawn is
        if (enemyNo <= 10) selectedEnemy = enemy;//this spawn normal enemy
        else if (enemyNo <= 20) selectedEnemy = fastBoi;//this spawn fast enemy
        else if (enemyNo <= 30) selectedEnemy = rangeBoi;//this spawn range enemy
        else if (enemyNo <= 40) selectedEnemy = bBM;//this spawn big enemy
        else { selectedEnemy = bigBoi; }//this spawn bigger enemy

        return selectedEnemy;
    }
}

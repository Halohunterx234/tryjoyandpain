    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    protected int waveCount; //the current wave
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

    //enemy types
    public GameObject player, enemy;
    public GameObject fastBoi;
    public GameObject bBM;
    public GameObject bigBoi;
    public GameObject rangeBoi;

    //bosses
    public GameObject miniboss;

    //waves
    public GameObject firstWave;
    public GameObject secondWave;
    public GameObject thirdWave;
    public bool firstWaveSpawned;
    public bool secondWaveSpawned;
    public bool thirdWaveSpawned;
    public bool miniBossSpawned;
    
    public bool hellWaveOneSpawned;
    public bool hellWaveTwoSpawned;

    //timer
    public TextMeshProUGUI time_text;
    public string time_text_string;
    float extra_c;

    //Audio Stuff
    public AudioSource aSource;
    public AudioClip warning;

    //Dialogue stuff
    DialogueController dc;

    private void Start()
    {
        //Set references
        player = FindObjectOfType<Player>().gameObject;
        waveTimer = waveMaxTimer;
        time_text = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        aSource = GetComponent<AudioSource>();
        dc = FindObjectOfType<DialogueController>();

        //Event Spawn Fail-Saves
        miniBossSpawned = false;
        firstWaveSpawned = false;
        secondWaveSpawned = false;
        thirdWaveSpawned = false;
        hellWaveOneSpawned = false;
        hellWaveTwoSpawned = false;
    }

    private void Update()
    {        
        if (player == null) return;
        timer += Time.deltaTime;
        //Convert the current timer into string to throw into timer text
        string minutes = (Mathf.RoundToInt(timer) / 60) >= 10 ? (Mathf.RoundToInt(timer) / 60).ToString() : "0" + (Mathf.RoundToInt(timer) / 60).ToString();
        string seconds = (Mathf.RoundToInt(timer) % 60) >= 10 ? (Mathf.RoundToInt(timer) % 60).ToString() : "0" + (Mathf.RoundToInt(timer) % 60).ToString();
        time_text.text = minutes + ":" + seconds;
        time_text_string = minutes + ":" + seconds;//this is just timer text
        //spawn a new wave every x number of seconds
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

       

        //spawn miniboss if its 07:00 once only
        if ((Mathf.RoundToInt(timer) / 60) >= 7 && !miniBossSpawned)
        {
            List<string> dialogue = new List<string>() { 
                "I'm getting chills up my spine, something's coming...",
                "I sense a presence, with supernatural powers... I gotta be careful.", 
                "Finally, someone who's worthy enough to put up a challenge." 
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);


            miniBossSpawned = true;
            Instantiate(miniboss, player.transform.position + (30*Mathf.Abs(-player.transform.localScale.x) * Vector3.one), Quaternion.identity);
        }

         //spawns first push wave at 5:00 once only
        if ((Mathf.RoundToInt(timer) / 60) >= 5 && !firstWaveSpawned)
        {
           
            List<string> dialogue = new List<string>() { 
                "Wha...What are those footsteps I hear?",
                "tsk, those cunning fools think they can corner me? Well, in fact they are the ones who are in a corner", 
                "A huge horde of enemies are coming, I should exercise caution" 
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);

            firstWaveSpawned = true;
            aSource.clip = warning;
            aSource.Play();
            Instantiate(firstWave,new Vector3(19,player.transform.position.y - 9,0), Quaternion.identity);

        }

        //spawns second push wave at 12:00 once only
        if ((Mathf.RoundToInt(timer) / 60) >= 12 && !secondWaveSpawned)
        {
            
            List<string> dialogue = new List<string>() { 
                "It's the footsteps again, but this time... heavier? Oh no...",
                "Even more enemies?? When will this ever end...",
                "Time to ramp it up a notch!" 
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);

            secondWaveSpawned = true;
            aSource.clip = warning;
            aSource.Play();
            Instantiate(secondWave, new Vector3(-10, -10, 0), Quaternion.identity);
        }

        //spawns second push wave at 16:00 once only
        if ((Mathf.RoundToInt(timer) / 60) >= 16 && !thirdWaveSpawned)
        {

            List<string> dialogue = new List<string>() {
                "It's the footsteps again, but this time... heavier? Oh no...",
                "Even more enemies?? When will this ever end...",
                "Time to ramp it up a notch!"
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);

            thirdWaveSpawned = true;
            aSource.clip = warning;
            aSource.Play();
            Instantiate(thirdWave, new Vector3( 46, 5, 0), Quaternion.identity);
        }



        //spawn miniboss and big boi wave at 22:00
        if ((Mathf.RoundToInt(timer) / 60) >= 22 && !hellWaveOneSpawned)
        {
            List<string> dialogue = new List<string>() { 
                "Oh no, RUNNNNNNNNNNNNN!!!!!", 
                "The ground is trembling like crazy, there is no way this is good", 
                "This is getting harder and harder... But I can't give up now, not for her." 
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);

            for (int i = 0; i < 5; i++)
            {
                Instantiate(miniboss, player.transform.position + new Vector3(0, i, 0) + (30 * Mathf.Abs(-player.transform.localScale.x) * Vector3.one), Quaternion.identity);
            }
           
            hellWaveOneSpawned = true;
            aSource.clip = warning;
            aSource.Play();
            Instantiate(thirdWave, player.transform.position + new Vector3(0, -10, 0), Quaternion.identity);
        }

        //spawn miniboss and big boi wave at 26:00
        if ((Mathf.RoundToInt(timer) / 60) >= 26 && !hellWaveTwoSpawned)
        {
            List<string> dialogue = new List<string>() {
                "Oh no, RUNNNNNNNNNNNNN!!!!!",
                "The ground is trembling like crazy, there is no way this is good",
                "This is getting harder and harder... But I can't give up now, not for her."
            };

            dc.SpawnDialogue(dialogue[Random.Range(0, dialogue.Count - 1)]);

            for (int i = 0; i < 10; i++)
            {
                Instantiate(miniboss, player.transform.position + new Vector3(0, i, 0) + (30 * Mathf.Abs(-player.transform.localScale.x) * Vector3.one), Quaternion.identity);
               
            }
            for( int i = 0; i < 5; i++)
            {
                Instantiate(thirdWave, player.transform.position + new Vector3(0, -10+i*2, 0), Quaternion.identity);
            }


            hellWaveTwoSpawned = true;
            aSource.clip = warning;
            aSource.Play();
           
        }

    }

    protected void CheckingActivation()
    {
        if (Time.time >= timeOfActivation + 1f) onceOnly = true;
    }

    //spawn a balanced number of enemies in all directions around the player
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
        else if (minutes < 10) value += 5 +1 * addvalue;
        else if (minutes < 15) value += 15 + 1 * addvalue;
        else if (minutes < 20) value += 25 + 1 * addvalue;
        else if (minutes < 25) value += 35 + 1 * addvalue;
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

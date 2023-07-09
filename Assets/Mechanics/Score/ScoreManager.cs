using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //To calculate the total amount of score 
    // Contributions to score
    // 1. Enemy Kills
    // 2. Time Played

    public int Score;
    public float ScoreMultiplier;

    private void Start()
    {
        Score = 0; //start of the game
        ScoreMultiplier = 1; //base multipler, is changed by permaMod-upgrades, items, etc
    }

    //Get time played in seconds and convert it to score
    public void Calc_TimeScore()
    {
        Score += Mathf.RoundToInt(Time.time * 5 * ScoreMultiplier);
    }

    //Convert enemy kill into score

    public void Calc_KillCount(int killCount)
    {
        Score += Mathf.RoundToInt(killCount * 10 * ScoreMultiplier);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int playerScore = 0;
    public int Player_Score => playerScore;

    [SerializeField] private Text score_Text;

    private void Awake()
    {
        playerScore = 0;
    }

    public void SetScore(int score)
    {
        playerScore += score;
        score_Text.text = $"Score : {playerScore}";
        SaveScore();
        if (playerScore >= 10 && !EnemySpawner.IsBossStage)
        {
            FindAnyObjectByType<EnemySpawner>().StartBossStage();
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", playerScore);
    }
}

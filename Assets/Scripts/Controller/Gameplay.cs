using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Gameplay : MonoBehaviour
{
    public static Gameplay instance;
    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }   
    }

    private void Awake()
    {
        _MakeInstance();
    }
    [SerializeField] private Text scoreText, endScoreText;
    [SerializeField] private GameObject gameOverPanel;

    public void _SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void _EndScore(int score)
    {
        endScoreText.text = "" + score;
    }
    
    public void _ShowPanel()
    {
        gameOverPanel.SetActive(true);
    }
}

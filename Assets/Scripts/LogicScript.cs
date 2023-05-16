using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    [ContextMenu("Increase Score")]
    void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }
    void Start()
    {
    }

    void Update()
    {
    }
} // CLASS END
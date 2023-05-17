using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public const float blockHeight = 1.1073f;

    public int attempts = 3;
    public GameObject[] hearts;

    public BlockSpawner blockSpawner;

    [HideInInspector]
    public BlockScript currentBlock;

    public CameraFollow cameraScript;

    public int playerScore = 0;
    public int highestScore = 0;
    public Text scoreText;

    public List<GameObject> landedBlocks;

    public Transform point;

    void Awake()
    {
        instance = this;
        playerScore = 0;
        attempts = 3;
    }

    void Start()
    {
        blockSpawner.SpawnBlock();
    }

    void Update()
    {
        ParseInput();

        float highestBlockY = highestBlock() + blockHeight / 2 + 3.84f;
        playerScore = (int)Math.Round(highestBlockY / blockHeight);
        if (playerScore > highestScore)
        {
            highestScore = playerScore;
        }

        MoveCamera(Math.Max(0.0f, highestBlockY - 2.0f));

        scoreText.text = playerScore.ToString();
        if (attempts < 1)
        {
            Destroy(hearts[0].gameObject);
            Invoke("RestartGame", 1.0f);
        }
        else if (attempts < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (attempts < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    void ParseInput()
    {
        if (!PauseMenu.isPaused && Input.GetMouseButtonDown(0))
        {
            currentBlock.DropBlock();
        }
    }

    public void SpawnNewBlock()
    {
        Invoke("NewBlock", 3.0f);
    }

    private void NewBlock()
    {
        blockSpawner.SpawnBlock();
    }

    public void MoveCamera(float ycoord)
    {
        cameraScript.targetPos.y = ycoord;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
    }

    public void loseAttempt()
    {
        attempts--;
    }

    public float highestBlock()
    {
        float highest = -3.84f;
        foreach (var block in landedBlocks)
        {
            try
            {
                if (block.transform.position.y > highest)
                {
                    highest = block.transform.position.y;
                }
            }
            catch (Exception _)
            {
                ;
            }
        }
        return highest;
    }
} // CLASS END
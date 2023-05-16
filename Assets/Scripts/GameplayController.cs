using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    //public GameObject ropeFixPoint;

    public BlockSpawner blockSpawner;

    [HideInInspector]
    public BlockScript currentBlock;

    public CameraFollow cameraScript;
    private int moveCount;

    public int playerScore;
    public Text scoreText;

    void Awake()
    {
        playerScore = 0;
        instance = this;
    }

    void Start()
    {
        blockSpawner.SpawnBlock();
    }

    void Update()
    {
        //scoretext.text = playerscore.tostring();
        ParseInput();
    }

    void ParseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBlock.DropBlock();
        }
    }

    public void SpawnNewBlock()
    {
        Invoke("NewBlock", 1.0f);
    }

    private void NewBlock()
    {
        blockSpawner.SpawnBlock();
    }

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 3)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 4.431f;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
    }
} // CLASS END
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public BlockSpawner blockSpawner;

    [HideInInspector]
    public BlockScript currentBlock;

    public CameraFollow cameraScript;
    private int moveCount;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        blockSpawner.SpawnBlock();
    }

    void Update()
    {
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
            //cameraScript.targetPos.y += 2.0f;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
    }
} // CLASS END
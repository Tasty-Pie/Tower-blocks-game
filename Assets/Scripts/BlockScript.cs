using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private const float maxDisplacement = 2.2f;
    private const float oscillationVelocity = 5.0f;
    private bool directedToTheRight = true;

    private bool dropped;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    void Start()
    {
        dropped = false;
        directedToTheRight = (UnityEngine.Random.Range(0, 2) == 0);
        GameplayController.instance.currentBlock = this;
    }

    void Update()
    {
        MoveBox();
    }

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0.0f;
    }

    void MoveBox()
    {
        if (!dropped)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += (directedToTheRight ? 1 : -1) * oscillationVelocity * Time.deltaTime;
            if (newPosition.x > maxDisplacement)
            {
                directedToTheRight = !directedToTheRight;
                newPosition.x = maxDisplacement;
            }
            else if (newPosition.x < -maxDisplacement)
            {
                directedToTheRight = !directedToTheRight;
                newPosition.x = -maxDisplacement;
            }
            transform.position = newPosition;
        }
    }

    public void DropBlock()
    {
        dropped = true;
        myBody.gravityScale = UnityEngine.Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBlock();
        GameplayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    void OnCollisionEnter2D(Collision2D collidedObject)
    {
        if (ignoreCollision)
        {
            return;
        }
        if (collidedObject.gameObject.tag == "Platform" || collidedObject.gameObject.tag == "Block")
        {
            Invoke("Landed", 1.0f);
            ignoreCollision = true;
        }
    }

    void OnTriggerEnter2D(Collider2D triggeredObject)
    {
        if (ignoreTrigger)
        {
            return;
        }
        if (triggeredObject.gameObject.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;
            Invoke("RestartGame", 1.0f);
        }
    }
} // CLASS END
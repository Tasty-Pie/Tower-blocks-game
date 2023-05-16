using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private AnchoredJoint2D myJoint;
    private bool dropped;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    void Start()
    {
        dropped = false;
        GameplayController.instance.currentBlock = this;
    }

    void Update()
    {
    }

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myJoint = GetComponent<AnchoredJoint2D>();
        myBody.AddForce(transform.right * 100.0f);
    }

    public void DropBlock()
    {
        if (myJoint != null)
        {
            myJoint.breakForce = 0;
            myJoint = null;
        }
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
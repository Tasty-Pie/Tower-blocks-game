using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private const float maxDisplacement = 2.2f;
    private float oscillationVelocity = 5.0f;

    private bool canMove;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    void Start()
    {
        canMove = true;
        if (UnityEngine.Random.Range(0, 2) != 0)
        {
            oscillationVelocity *= -1.0f;
        }
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
        if (canMove)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += oscillationVelocity * Time.deltaTime;
            if (newPosition.x > maxDisplacement)
            {
                oscillationVelocity *= -1.0f;
                newPosition.x = maxDisplacement;
            }
            else if (newPosition.x < -maxDisplacement)
            {
                oscillationVelocity *= -1.0f;
                newPosition.x = -maxDisplacement;
            }
            transform.position = newPosition;
        }
    }
} // CLASS END
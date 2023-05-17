using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private AnchoredJoint2D myJoint;

    private Rigidbody2D myBody;

    public static bool gameOver = false;
    private bool ignoreCollision = false;
    private bool ignoreTrigger = false;

    [SerializeField] private AudioSource blockCollisionSound;
    [SerializeField] private AudioSource blockDropSound;
    [SerializeField] public AudioSource triggerSound;

    public GameObject lineRendererPrefab;
    public LineController myLr;

    void Start()
    {
        GameplayController.instance.currentBlock = this;
    }

    void Update()
    {
    }

    void Awake()
    {
        GameObject lineObj = Instantiate(lineRendererPrefab);
        myLr = lineObj.GetComponent<LineController>();
        Transform[] thePoints = { this.transform, GameplayController.instance.point.transform };
        myLr.SetUpLine(thePoints);

        myBody = GetComponent<Rigidbody2D>();
        myBody.mass = 1.0f;
        myJoint = GetComponent<AnchoredJoint2D>();
        myBody.AddForce(transform.right * 150.0f * (UnityEngine.Random.Range(0, 2) == 0 ? -1.0f : 1.0f));
    }

    public void DropBlock()
    {
        if (myJoint != null)
        {
            blockDropSound.Play();
            myJoint.breakForce = 0;
            myJoint = null;
            Destroy(myLr.gameObject);
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

        myBody.mass = 20.0f;
        GameplayController.instance.landedBlocks.Add(this.gameObject);
        GameplayController.instance.playerScore++;
        GameplayController.instance.SpawnNewBlock();
    }

    void OnCollisionEnter2D(Collision2D collidedObject)
    {
        if (ignoreCollision)
        {
            return;
        }
        if (collidedObject.gameObject.tag == "Platform" || collidedObject.gameObject.tag == "Block")
        {
            blockCollisionSound.Play();
            var oldVelocity = myBody.velocity;
            myBody.velocity = new Vector2(oldVelocity[0] / 2.0f, oldVelocity[1]);
            Invoke("Landed", 3.0f);
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
            ignoreTrigger = true;
            triggerSound.Play();
            if (GameplayController.instance.attempts == 0)
            {
                CancelInvoke("Landed");
                gameOver = true;
            }
            else
            {
                GameplayController.instance.loseAttempt();
                CancelInvoke("Landed");
                Landed();
                GameplayController.instance.playerScore -= 2;
            }
        }
        else if (triggeredObject.gameObject.tag == "OutOfBounds")
        {
            Destroy(this.gameObject);
        }
    }
} // CLASS END
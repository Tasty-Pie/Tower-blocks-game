using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;

    private float smoothMove = 5.0f;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothMove * Time.deltaTime);
    }
} // CLASS END
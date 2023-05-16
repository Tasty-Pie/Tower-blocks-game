using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SpawnBlock()
    {
        GameObject blockObj = Instantiate(blockPrefab);
        blockObj.transform.position = this.transform.position;
    }
} // CLASS END
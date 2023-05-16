using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    private const float displacement = 2.2f;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SpawnBlock()
    {
        //Vector3 initialposition = this.transform.position;
        //quaternion initialrotation = this.transform.rotation;
        //if (unityengine.random.range(0, 2) == 0)
        //{
        //    initialrotation.set(0, 0, 45, 1);
        //    initialposition = new vector3(initialposition.x + displacement, initialposition.y, initialposition.z);
        //}
        //else
        //{
        //    initialrotation.set(0, 0, -45, 1);
        //    initialposition = new vector3(initialposition.x - displacement, initialposition.y, initialposition.z);
        //}
        GameObject blockObj = Instantiate(blockPrefab);
        blockObj.transform.position = this.transform.position;
    }
} // CLASS END
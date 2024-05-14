using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonSpawner : MonoBehaviour
{
    public GameObject watermelonPrefab;
    public GameObject socket;

    public void WatermelonSpawnerFunction()
    {
        Instantiate(watermelonPrefab, socket.transform.position, Quaternion.identity);
    }

}
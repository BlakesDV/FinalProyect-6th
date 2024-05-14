using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class BullsEyePuzzle : MonoBehaviour
{
    public GameObject watermelon;
    private int destroyThisCount = 0;
    public GameObject socket;

    void Start()
    {
        UpdateObjectCount();
    }

    void UpdateObjectCount()
    {
        GameObject[] destroyThisObjects = GameObject.FindGameObjectsWithTag("DestroyThis");
        destroyThisCount = destroyThisObjects.Length;
        if (destroyThisCount >= 5)
        {
            WatermelonSpawner();
        }
    }

    public void WatermelonSpawner()
    {
        // Instantiate the watermelon inside the socket
        Instantiate(watermelon, socket.transform.position, Quaternion.identity);
    }

    public void ObjectDestroyed()
    {
        UpdateObjectCount();
    }
}

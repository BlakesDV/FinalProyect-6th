using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellPuzzle : MonoBehaviour
{
    public GameObject watermelon;
    public GameObject[] buttons;

    private int activeButtonCount = 0;

    void Update()
    {
        if (activeButtonCount == 3)
        {
            WatermelonSpawner(); 
        }
    }
    public void WatermelonSpawner()
    {
        Instantiate(watermelon, transform.position, Quaternion.identity);
    }
}

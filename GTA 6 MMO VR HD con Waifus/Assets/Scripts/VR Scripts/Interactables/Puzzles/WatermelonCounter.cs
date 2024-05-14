using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WatermelonCounter : MonoBehaviour
{
    public GameObject socket1;
    public GameObject socket2;
    public GameObject socket3;
    public GameObject socket4;
    public GameObject socket5;

    public GameObject gameOverPanel;

    // Update is called once per frame
    void Update()
    {
        if (CheckForWatermelons())
        {
            // Activa el panel del canvas
            gameOverPanel.SetActive(true);
        }
    }

    bool CheckForWatermelons()
    {
        bool allSocketsFilled = true;
        if (!HasWatermelon(socket1))
            allSocketsFilled = false;
        if (!HasWatermelon(socket2))
            allSocketsFilled = false;
        if (!HasWatermelon(socket3))
            allSocketsFilled = false;
        if (!HasWatermelon(socket4))
            allSocketsFilled = false;
        if (!HasWatermelon(socket5))
            allSocketsFilled = false;
        return allSocketsFilled;
    }

    bool HasWatermelon(GameObject socket)
    {
        Collider[] colliders = Physics.OverlapBox(socket.transform.position, socket.transform.localScale / 2);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Watermelon"))
            {
                return true;
            }
        }
        return false;
    }
}

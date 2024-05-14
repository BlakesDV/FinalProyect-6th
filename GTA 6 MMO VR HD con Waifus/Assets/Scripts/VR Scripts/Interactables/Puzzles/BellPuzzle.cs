using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BellPuzzle : MonoBehaviour
{
    public GameObject watermelon;
    public GameObject[] buttons;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable buttonGrip;
    private bool buttonActive = false;
    private int activeButtonCount = 0;


    private void Start()
    {
        buttonGrip = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        buttonGrip.selectEntered.AddListener(ButtonActivator);
    }

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

    private void ButtonActivator(SelectEnterEventArgs args)
    {
        buttonActive = !buttonActive;
        watermelon.SetActive(buttonActive);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRIntController : MonoBehaviour
{
    //script recovered from VR assigments

    float sliderValue;
    XRSlider xRSlider;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable buttonGrip;
    private bool buttonActive = false;
    private int activeButtonCount = 0;
    public GameObject socket;
    XRSocketInteractor sx; 
    public GameObject watermelonPrefab;

   


    void Start()
    {
        xRSlider = GetComponent<XRSlider>();
        sx = GetComponent<XRSocketInteractor>();
        buttonGrip = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        buttonGrip.selectEntered.AddListener(ButtonActivator);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MovX()
    {
        print("Movimiento en X");
    }

    public void MovY()
    {
        print("Movimiento en Y");
    }

    public void TriggerEnter()
    {
        IXRSelectInteractable x = sx.GetOldestInteractableSelected();
        Destroy(x.transform.gameObject);
    }

    public void ValueChange(float value)
    {
        sliderValue = value;
    }

    public void ButtonPress()
    {
        Debug.Log("Button Pressed");
    }

    //activate and deactivate buttons
    private void ButtonActivator(SelectEnterEventArgs args)
    {
        buttonActive = !buttonActive;
        watermelonPrefab.SetActive(buttonActive);

    }
    public void WatermelonSpawnerFunction()
    {
        Instantiate(watermelonPrefab, socket.transform.position, Quaternion.identity);
    }

    public void LeverOn()
    {
        socket.SetActive(false);
    }

    public void LeverOff()
    {
        socket.SetActive(true);
    }
}

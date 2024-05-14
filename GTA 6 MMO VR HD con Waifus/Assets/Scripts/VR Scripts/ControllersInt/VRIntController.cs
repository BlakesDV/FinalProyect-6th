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
    public GameObject watermelonBP;
    public GameObject watermelonLP;
    public GameObject socketBP;
    public GameObject socketLP;
    public GameObject socket;

    //public GameObject cannonball;
    //public Transform cannonPos;
    //public Rigidbody cannon;
    //public float cannonballSpeed = 10f;
    XRSocketInteractor sx;

    public XRBaseInteractable[] buttons;
    private int activeButtonCount = 0;

    void Start()
    {
        xRSlider = GetComponent<XRSlider>();
        sx = GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeButtonCount == buttons.Length)
        {
            WatermelonSpawner();
        }
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
        //Instantiate(cannonball, cannonPos.position, Quaternion.identity);

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
        activeButtonCount++;
        if (activeButtonCount >= buttons.Length)
        {
            WatermelonSpawner();
        }
    }


    public void WatermelonSpawner()
    {
        GameObject watermelonInstance = Instantiate(watermelonBP, transform.position, Quaternion.identity);
        watermelonInstance.SetActive(true);
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

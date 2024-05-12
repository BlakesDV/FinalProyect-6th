using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRIntController : MonoBehaviour
{
    //script recovered from VR assigments

    float sliderValue;
    XRSlider xRSlider;
    public GameObject key;
    public GameObject socket;

    public GameObject cannonball;
    public Transform cannonPos;
    public Rigidbody cannon;
    public float cannonballSpeed = 10f;
    XRSocketInteractor sx;

    void Start()
    {
        xRSlider = GetComponent<XRSlider>();
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
        Instantiate(cannonball, cannonPos.position, Quaternion.identity);

        IXRSelectInteractable x = sx.GetOldestInteractableSelected();
        Destroy(x.transform.gameObject);

    }

    public void ValueChange()
    {
        
    }

    public void ButtonPress()
    {

    }

    public void KeySpawner()
    {
        Instantiate(key, transform.position, Quaternion.identity);
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

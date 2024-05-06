using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayController : MonoBehaviour
{
    [SerializeField] GameObject leftTeleportation;
    [SerializeField] GameObject righTeleportation;

    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rigthActivate;

    [SerializeField] InputActionProperty leftCancel;
    [SerializeField] InputActionProperty rigthCancel;

    void Update()
    {
        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1);
        righTeleportation.SetActive(rigthCancel.action.ReadValue<float>() == 0 && rigthActivate.action.ReadValue<float>() > 0.1);
    }
}

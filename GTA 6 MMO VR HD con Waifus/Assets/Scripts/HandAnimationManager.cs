using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationManager : MonoBehaviour
{

    public InputActionProperty Dedos_ActionAnimator;
    public InputActionProperty Puño_ActionAnimator;
    public Animator HandAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float TriggerValue = Dedos_ActionAnimator.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger", TriggerValue);

        float gripValue = Puño_ActionAnimator.action.ReadValue<float>();
        HandAnimator.SetFloat("Grip", gripValue);
    }
}

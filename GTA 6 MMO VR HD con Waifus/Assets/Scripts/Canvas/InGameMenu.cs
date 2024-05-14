using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.UI;


public class InGameMenu : MonoBehaviour
{
    public GameObject canvas;
    public Button backButton;

    void Update()
    {
        // Assuming you want to listen to onClick event of the backButton
        if (backButton != null)
        {
            backButton.onClick.AddListener(Desactivar);
        }
        else
        {
            Debug.LogWarning("Back button is not assigned!");
        }
    }

    void Desactivar()
    {
        // Desactivate the canvas object
        canvas.SetActive(false);
    }
}


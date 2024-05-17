using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookieSpawner : MonoBehaviour
{
    [SerializeField] GameObject cookie;
    [SerializeField] Transform cookieSpawner;
    private bool canSpawnCookie = true;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _interactor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        _interactor.activated.AddListener(SpawnCookieVR);
    }
    void Update()
    {
        if (canSpawnCookie && Input.GetKeyDown(KeyCode.C))
        {
            SpawnCookie();
        }
    }

    void SpawnCookie()
    {
        GameObject tempCookie = Instantiate(cookie, cookieSpawner.position, Quaternion.identity);
        canSpawnCookie = false;
    }
    
    void SpawnCookieVR(ActivateEventArgs arg)
    {
        GameObject tempCookie = Instantiate(cookie, cookieSpawner.position, Quaternion.identity);
        canSpawnCookie = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Acompañante"))
        {
            Destroy(gameObject);
        }
    }
}

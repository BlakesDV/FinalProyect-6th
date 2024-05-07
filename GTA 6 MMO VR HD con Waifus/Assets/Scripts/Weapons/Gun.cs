using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawner;
    [SerializeField] float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable _interactor = GetComponent<XRGrabInteractable>();
        _interactor.activated.AddListener(FireGun);
    }

    public void FireGun(ActivateEventArgs arg) {
        GameObject tempBullet = Instantiate(bullet, bulletSpawner.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody>().velocity = bulletSpawner.forward * bulletSpeed;
        Destroy(tempBullet, 6);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRMov : MonoBehaviour
{
    public float speed = 1f;
    public float sensitivity = 1f;

    void Update()
    {
        PlayerMov();
    }

    public void PlayerMov()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}

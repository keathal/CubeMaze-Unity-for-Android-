using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public float moveSpeed = 12f;
    float VerticalVelocity, gravity = 30.0f;
    CharacterController controller;
    Vector3 moveVector;
    VirtualJoy joystick;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        joystick = FindObjectOfType<VirtualJoy>();
    }

    void Update()
    {
        moveVector = Vector2.zero;

        if (controller.isGrounded)
        {
            //VerticalVelocity = 0;
            VerticalVelocity = gravity * Time.deltaTime;
        }
        else
        {
            VerticalVelocity = -gravity * Time.deltaTime;
            moveVector.y = VerticalVelocity * moveSpeed * Time.deltaTime*3;
        }


        moveVector.x = -joystick.Vertical() * moveSpeed * 5 * Time.deltaTime;
        moveVector.z = joystick.Horizontal() * moveSpeed * 5 * Time.deltaTime;

        
        if(moveVector.x==0 && moveVector.z==0)
        {
            moveVector.x = -Input.acceleration.y * moveSpeed * 5 * Time.deltaTime;
            moveVector.z = Input.acceleration.x * moveSpeed * 5 * Time.deltaTime;
        }


        controller.Move(moveVector);
        gameObject.transform.Rotate(new Vector3(moveVector.z*20, 0, -moveVector.x*20));
        transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
    }
  
}

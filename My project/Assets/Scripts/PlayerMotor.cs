using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private GunShoot gun;
    private bool isGrounded;
    public float speed = 15f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gun = GetComponent<GunShoot>();
    }

    // Update is called once per frame
    void Update(){
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
            playerVelocity.x = playerVelocity.x > 0 ? 0f : playerVelocity.x / 2;
            Debug.Log(playerVelocity.z);
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Shoot()
    {
        gun.Shoot();
        if(!isGrounded){
            playerVelocity += ((Camera.main.transform.forward * -1) * Mathf.Sqrt(2f * -3.0f * gravity));
        }
    }
}

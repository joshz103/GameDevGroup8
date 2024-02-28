using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

/*
 The inputs are handled by Unity's built in InputSystem package. Inputs can be assigned by opening PlayerInput.inputactions (located in Assets/characters/player)
 You can see some of the methods such as move, jump, and sprint are called via the Player Input component. Open the dropdown menus for Events and Gameplay and you will see them.
 Also note that .isGrounded is a handy function built into the CharacterController class.
 Also also note that multiplying by time.Deltatime will make an action NOT dependant on framerate.
 */

public class PlayerController : MonoBehaviour
{
    private playerstats playerStat;

    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;

    private float speed;
    private float movementMultiplier;
    public float currentSpeed;
    private float acceleration;
    private bool isMoving;
    private bool isSprinting;
    public float targetSpeed;

    //[SerializeField] private Movement movement; --changed this, unused now. Didn't play well with stats

    private float smoothTime = 0.05f;
    private float currentVelocity;

    private float gravity = -1.0f;
    private float gravityMultiplier = 15.0f;
    private float y_velocity;
    private float terminalVelocity = -30f;

    private float jumpPower;

    public float moveXAccel;
    public float moveZAccel;


    // Start is called before the first frame update. Gets the ControllerController component from whatever object has this script. We can then reference it in the code.
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStat = GetComponent<playerstats>();
        movementMultiplier = 2f;
        acceleration = 12.5f;
    }

    // Update is called once per frame. Calls all these methods every frame to update movement, rotation, and gravity. Any method with time.Deltatime will make adjustments independant of framerate.
    void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        ApplyStats();
    }

    //Called every frame to make the player face where they are moving. smoothTime controls how quickly the player turns.
    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    //Called every frame to apply movement to the player. Base speed is set in the speed variable while sprint is handled by the Movement struct.
    private void ApplyMovement()
    {
        if (isMoving && !isSprinting)
        {
            targetSpeed = speed;
        }
        else if (isMoving && isSprinting)
        {
            targetSpeed = speed * movementMultiplier;
        }
        else
        {
            targetSpeed = 0;
        }

        //var targetSpeed = isSprinting ? speed * movementMultiplier : speed; //If isSprinting then targetSpeed = movement.speed * movement.multiplier else targetSpeed = movement.speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 movementGravity = new Vector3(0f, direction.y, 0f);      //The .Move function affects X, Z, and for some reason Y. Had to separate into 2 move functions so sprint doesn't affect your gravity scale!


        //moveXAccel = Mathf.MoveTowards(moveXAccel, direction.x, 0.25f * Time.deltaTime);
        //moveZAccel = Mathf.MoveTowards(moveZAccel, direction.z, 0.25f * Time.deltaTime);

        Vector3 movementXZ = new Vector3(direction.x, 0f, direction.z);
        //Vector3 movementXZ = new Vector3(moveXAccel, 0f, moveZAccel);

        characterController.Move(movementXZ * currentSpeed * Time.deltaTime);
        //Debug.Log(movementXZ * currentSpeed * Time.deltaTime);
        characterController.Move(movementGravity * Time.deltaTime);
    }

    //Called every frame to apply gravity to player. Checks if the player is grounded and their Y velocity is less than 0 (Velocity is set to -1 when grounded).
    private void ApplyGravity()
    {
        if (characterController.isGrounded && y_velocity < 0.0f)
        {
            y_velocity = -1f;
        }
        else
        {
            y_velocity += gravity * gravityMultiplier * Time.deltaTime;
            if (y_velocity < terminalVelocity)
            {
                y_velocity = terminalVelocity; //Clamps the max negative y velocity (aka the max falling speed) to -30 so it doesn't keep multiplying and reaching absurd values (and eventually crash the game).
            }
        }
        direction.y = y_velocity;
    }

    //******************************
    //WASD MOVEMENT (Not Sprinting!)
    //******************************
    public void Move(InputAction.CallbackContext context)
    {
        isMoving = context.started || context.performed;
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }

    //******************************
    //JUMPING
    //******************************
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        if (!characterController.isGrounded)
        {
            return;
        }
        y_velocity += jumpPower;
    }

    //******************************
    //SPRINTING - Just changes the a bool to see if shift is being held
    //******************************
    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.started || context.performed;
    }

    //Applies stats
    public void ApplyStats()
    {
        speed = playerStat.getSpeed();
        jumpPower = playerStat.getJumpHeight();
    }

    //******************************
    //MOVEMENT - This handles sprint acceleration and speed to multiply movement by. Edit the variables in Unity once you select the player object. TODO: Grab values for speed from stats class
    //******************************
    /*
    [Serializable]
    public struct Movement
    {
        [HideInInspector] public float speed;
        public float multiplier;
        public float acceleration;

        [HideInInspector]public bool isSprinting;
        [HideInInspector]public float currentSpeed;
    }
    */
}

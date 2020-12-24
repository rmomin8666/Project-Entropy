﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

/*
To do besides the feel
- dash
- shield
- dodge
- short hop
- edge grabbing -- waiting on hands
- crouch -- maybe crouch walking
*/

//states add more
public enum playerState{
    idling = 0,
    dashing = 1,
    walking = 2,
    running = 4,
    shielding = 8,
    falling = 16,
    fastFalling = 32,
    crouching = 64
}



public class PlayerController : MonoBehaviour
{
    
    //x movement variables
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float airBaseAcceleration = .25f;
    [SerializeField] private float airAcceleration = .75f;
    [SerializeField] private float airSpeed = 3f;
    [SerializeField] private float dashBaseAcceleration = 3f;
    [SerializeField] private float dashAcceleration = 3f;
    [SerializeField] private float dashSpeed = 3f;
    [SerializeField] private float traction = 1f;

    //y movement variables
    [SerializeField] private float fastFallGravity = -50f;
    [SerializeField] private float fastFallVelocity = -15f;
    [SerializeField] private float fallVelocity = -10f; 
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private int maxJumps = 2;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;

    


    private float jumpVelocity; //whats needed to get to jumpHeight
    private Rigidbody body;

    private bool jump_IP = false;
    private float horizontal_IP;
    private float vertical_IP;

 
    private int jumps = 0;
    private bool isGrounded = true;

    playerState state = playerState.idling;
    
    void Start(){
        body = GetComponent<Rigidbody>();
        jumpVelocity = Mathf.Sqrt(jumpHeight*gravity*-2f);
    }

    void Awake(){
        Application.targetFrameRate = 50;
    }

    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position, .15f, groundLayers);
        horizontal_IP = Input.GetAxis("Horizontal");
        vertical_IP = Input.GetAxis("Vertical");
        if(Input.GetButtonDown("Jump") && jumps<maxJumps){
            jump_IP = true;
        }
        if(Abs(horizontal_IP) > .01f){ //flip model ignore small inputs
            transform.forward = new Vector3(horizontal_IP,0,0);
        }
        Debug.Log(state);
    }

    private void groundedPhysics(ref Vector3 velocityChange){
        float currVelocityY = body.velocity.y;
        float currVelocityX = body.velocity.x;
        int h_sign = System.Math.Sign(horizontal_IP);

        if(horizontal_IP == 0 && currVelocityX != 0){
            velocityChange.x -= Sign(currVelocityX)*traction*Time.deltaTime; //slow down to a stop
            if(Sign(currVelocityX) != Sign(velocityChange.x+currVelocityX)){
                velocityChange.x = -currVelocityX;
            }
            state = playerState.idling;
        }else if(horizontal_IP != 0 && Abs(horizontal_IP)<.5){
            velocityChange.x = walkSpeed*h_sign;
            state = playerState.walking;
        }else if(horizontal_IP != 0 && state != playerState.running){
            //dash here
            state = playerState.dashing;
            velocityChange.x = (h_sign*dashBaseAcceleration + dashAcceleration*horizontal_IP)*Time.deltaTime;
            if(Abs(velocityChange.x+currVelocityX) >= dashSpeed){
                state = playerState.running;
            }
        }

        //limits
        if(state == playerState.walking && Abs(velocityChange.x + currVelocityX) > walkSpeed){ //walking
            velocityChange.x = h_sign*walkSpeed-currVelocityX;
        }
        else if(state = playerState.running && Abs(velocityChange.x + currVelocityX) > runSpeed){
            velocityChange.x -= Sign(currVelocityX)*traction*Time.deltaTime;
            if(Abs(velocityChange.x+currVelocityX) <runSpeed){
                velocityChange.x = runSpeed - ;
            }
        }

    
    }

    private void airPhysics(ref Vector3 velocityChange){
        float currVelocityY = body.velocity.y;
        float currVelocityX = body.velocity.x;
        int h_sign = System.Math.Sign(horizontal_IP);
        bool isFastFalling = currVelocityY <= 0 && vertical_IP < -.75;

        velocityChange.x += (h_sign*airBaseAcceleration + airAcceleration*horizontal_IP)*Time.deltaTime; //apply air acceletation   

        //gravity
        if(isFastFalling){
            state = playerState.fastFalling;
            velocityChange.y += fastFallGravity*Time.deltaTime;
        }else{
            state = playerState.falling;
            velocityChange.y += gravity*Time.deltaTime;
        }

        //limits
        if(Abs(velocityChange.x + currVelocityX) > airSpeed){
            velocityChange.x = h_sign*airSpeed-currVelocityX;
        }
        if(isFastFalling && (velocityChange.y + currVelocityY < fastFallVelocity)){
            velocityChange.y = fastFallVelocity-currVelocityY;
        }else if(!isFastFalling && (velocityChange.y + currVelocityY < fallVelocity)){
            velocityChange.y = fallVelocity-currVelocityY;
        }

    }

    void FixedUpdate(){
        //essentially, we blindly calculate velocities and then apply limits after
        Vector3 velocityChange = Vector3.zero; //zero out velocity to add to each body each PU(physics update)
        float currVelocityY = body.velocity.y;
        if(jump_IP){
            velocityChange.y += jumpVelocity;
            velocityChange.y -= currVelocityY; //stop in air then jump
            //if(jumps > 0 && h_sign != Sign(currVelocityX)){
            //    velocityChange.x -= currVelocityX;//remove prev x velocity
            //}
            jumps++;
            jump_IP = false;
        }else if(isGrounded){
            jumps = 0;
            jump_IP = false; //land on ground after clicking jump maxJumps+1 times, don't auto jump
        }
        if(isGrounded){
            groundedPhysics(ref velocityChange);
        }else{
            airPhysics(ref velocityChange);
        }

        body.AddForce(velocityChange, ForceMode.VelocityChange);
    }

}
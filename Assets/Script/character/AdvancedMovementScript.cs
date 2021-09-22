using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdvancedMovementScript : MonoBehaviour
{
    //game objects
    [SerializeField] GameObject CameraAnchor;
    [SerializeField] Rigidbody playerRigidBody;

    //properties
    [SerializeField] float WalkingSpeed = 3;
    [SerializeField] float RunningSpeed = 6;
    [SerializeField] float SneakingSpeed = 1.5f;

    [SerializeField] float walkAccelMultiplier = 0.5f;
    [SerializeField] float runAccelMultiplier = 1;
    [SerializeField] float sneakAccelMultiplier = 0.5f;

    //universal variables
    /**state machine
     * 0 = walking
     * 1 = running
     * 2 = sneaking
     */
    int state = 0;
    Vector3 desiredDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //take input from player
        //calculate acceleration direction
        //taking into account airbourn state 
        CalculateDirection();
        if(Input.GetKey(KeyCode.LeftShift))
        {
            MovePlayer(SneakingSpeed, sneakAccelMultiplier, desiredDirection);
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            MovePlayer(RunningSpeed, runAccelMultiplier, desiredDirection);
        }
        else
        {
             MovePlayer(WalkingSpeed, walkAccelMultiplier, desiredDirection);
        }
    }

    /*
     * update the desired direction to a normalized direction vector
     */
    void CalculateDirection()
    {
        //get user input
        //get forward direction
        if (Input.GetKey("w") && !Input.GetKey("s"))
        {
            //x axis +
            desiredDirection = CameraAnchor.transform.forward;
        }
        else if (!Input.GetKey("w") && Input.GetKey("s"))
        {
            //x axis -
            desiredDirection = -CameraAnchor.transform.forward;
        }
        else
        {
            //no x axis
            desiredDirection = Vector3.zero;
        }
        //get sideways direction
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            //z axis -
            desiredDirection += -CameraAnchor.transform.right;
        }
        else if (!Input.GetKey("a") && Input.GetKey("d"))
        {
            //x axis +
            desiredDirection += CameraAnchor.transform.right;
        }
        //normalize direction
        desiredDirection = desiredDirection.normalized;
    }

    /**
     * 
     */
    void MovePlayer(float speed, float accelMultiplier, Vector3 direction)
    {
        Vector3 MoveVelocity = direction*speed - playerRigidBody.velocity;
        playerRigidBody.AddForce(MoveVelocity*accelMultiplier, ForceMode.Acceleration);
    }


    /**
     * detect ground/slope contact
     *
    void ContactDetection()
    {
        //slope handling
        RaycastHit detectionHit;
        Vector3 slopeVector = Vector3.up;
        if (Physics.Raycast(playerRigidBody.transform.position - new Vector3(0,0.3f,0), desiredVelocity.normalized * 1 + Vector3.down * 0.6f, out detectionHit, 1, groundMask)) {
            Debug.Log(detectionHit.point + " hit location");
            Debug.Log(playerRigidBody.transform.position + " player position");
            Debug.DrawLine(detectionHit.point, detectionHit.point + Vector3.up * 0.2f, Color.blue, 10);
        }
        desiredVelocity = new Vector3(desiredVelocity.x, detectionHit.point.y - (playerRigidBody.transform.position.y - 0.5f), desiredVelocity.z);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(playerRigidBody.transform.position - Vector3.up * playerCapsuleCollider.height, playerCapsuleCollider.radius + GroundDistance, groundMask);
    }
    */
}

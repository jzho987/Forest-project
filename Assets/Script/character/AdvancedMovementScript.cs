using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdvancedMovementScript : MonoBehaviour
{
    //game objects
    [SerializeField] GameObject CameraAnchor;
    [SerializeField] Rigidbody playerRigidBody;
    [SerializeField] LayerMask groundMask;

    //character properties
    [SerializeField] float WalkingSpeed = 3;
    [SerializeField] float RunningSpeed = 6;
    [SerializeField] float SneakingSpeed = 1.5f;

    [SerializeField] float walkAccelMultiplier = 0.5f;
    [SerializeField] float runAccelMultiplier = 1;
    [SerializeField] float sneakAccelMultiplier = 0.5f;

    [SerializeField] float JumpMovementSpeed = 3;
    [SerializeField] float JumpAccelMultiplier = 0.2f;
    [SerializeField] float JumpStrength = 1;
    [SerializeField] float gravityMultiplier = 1;

    //universal variables
    /**state machine
     * 0 = walking
     * 1 = running
     * 2 = sneaking
     */
    int state = 0;
    Vector3 desiredDirection = Vector3.zero;
    [SerializeField] float groundDistance = 1f;
    [SerializeField] float scanRadius = 0.5f;

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
        VerticalAction();
        if (isGrounded())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MovePlayer(SneakingSpeed, sneakAccelMultiplier, desiredDirection);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                MovePlayer(RunningSpeed, runAccelMultiplier, desiredDirection);
            }
            else
            {
                MovePlayer(WalkingSpeed, walkAccelMultiplier, desiredDirection);
            }
        }
        else
        {
            MovePlayerFlat(JumpMovementSpeed, JumpAccelMultiplier, desiredDirection);
        }
    }

    void VerticalAction()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            playerRigidBody.AddForce(Vector3.up * JumpStrength, ForceMode.Acceleration);
        }
        else if(!isGrounded())
        {
            playerRigidBody.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }

    }

    public void InputDirection(bool forward, bool backward, bool left, bool right)
    {
        Vector3 slopeAngle = getSlopeAngle();
        if (forward && !backward)
        {
            //x axis +
            desiredDirection = CameraAnchor.transform.forward;
        }
        else if (!forward && backward)
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
        if (left && !right)
        {
            //z axis -
            desiredDirection += -CameraAnchor.transform.right;
        }
        else if (!left && right)
        {
            //x axis +
            desiredDirection += CameraAnchor.transform.right;
        }

        //normalize direction
        Vector3 sideAngle = Vector3.Cross(desiredDirection, Vector3.down);
        desiredDirection = Vector3.Cross(sideAngle, slopeAngle).normalized;
    }

    bool isGrounded()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerRigidBody.position - Vector3.up * groundDistance, scanRadius, groundMask);
        return hitColliders.Length != 0;
    }

    Vector3 getSlopeAngle()
    {
        //return up if not grounded
        if(!isGrounded())
            return Vector3.up;
        //raycase downwards with a set distance
        RaycastHit hit;
        if (Physics.Raycast(playerRigidBody.position,Vector3.down, out hit, 1, groundMask))
        {
            //return raycase normal if hit
            return hit.normal;
        }
        //return up if no raycast hit
        return Vector3.up;
    }

    /**
     * Move the player according to the desired direction
     */
    void MovePlayer(float speed, float accelMultiplier, Vector3 direction)
    {
        Vector3 MoveVelocity = direction*speed - playerRigidBody.velocity;
        playerRigidBody.AddForce(MoveVelocity*accelMultiplier, ForceMode.Acceleration);
    }

    /**
     * Move the player according to the desired direction, excluding the yaxis
     */
    void MovePlayerFlat(float speed, float accelMultiplier, Vector3 direction)
    {
        Vector3 MoveVelocity = direction * speed - playerRigidBody.velocity;
        playerRigidBody.AddForce(Vector3.Scale(MoveVelocity * accelMultiplier,new Vector3(1,0,1)), ForceMode.Acceleration);
    }
}

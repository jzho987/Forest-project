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
    [SerializeField] initializeCharacter initialize;
    characterEvent events;
    CharacterStatistics statsContainer;

    //this is used to combat drag
    [SerializeField] float MovementMultiplier;

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
    [SerializeField] float terminalVelocity;

    //temporary variable
    //record grounded state for determining when state changed
    [SerializeField] CameraController playerCameraController;
    bool lastGroundedState;
    public bool queuedJump;
    float jumpQueueTime = 0.1f;
    float lastJumpQueued;

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        lastJumpQueued = 0;

        //subscribe
        initialize.onInitializeCharacter += initializeCharacterCallback;
    }

    private void initializeCharacterCallback(characterEvent events, CharacterStatistics stats)
    {
        Debug.Log("initialized hehe");
        this.events = events;
        this.statsContainer = stats;

        events.MovementInput += InputDirection;
        events.onJumpDetected += jump;
    }

    /**
     * this is the call back to character controller's on jump detected event
     * it will give the character a upward force to simulate jump
     * 
     * this fits in the rigidbody construct of the game
     * 
     * this should only be triggered once before the is grounded state turns false
     * could be bug prone in that sense
     * 
     * should implement a input queue system inside of this class
     */
    void jump()
    {
        if (isGrounded())
        {
            Debug.Log("jumped");
            playerRigidBody.AddForce(Vector3.up * statsContainer.JumpStrength, ForceMode.Acceleration);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //take input from player
        //calculate acceleration direction
        //taking into account airbourn state 
        ApplyGravityMannual();

        if (isGrounded())
        {
            if(lastGroundedState == false)
            {
                //this means the player landed
                //StartCoroutine(playerCameraController.landingShake(0.1f,0.4f));
                lastGroundedState = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                MovePlayer(statsContainer.SneakingSpeed * NormalToAngle(desiredDirection), statsContainer.sneakAccelMultiplier, desiredDirection);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                MovePlayer(statsContainer.RunningSpeed * NormalToAngle(desiredDirection), statsContainer.runAccelMultiplier, desiredDirection);
            }
            else
            {
                MovePlayer(statsContainer.WalkingSpeed * NormalToAngle(desiredDirection), statsContainer.walkAccelMultiplier, desiredDirection);
            }
        }
        else
        {
            lastGroundedState = false;
            MovePlayerFlat(statsContainer.JumpMovementSpeed, statsContainer.JumpAccelMultiplier, desiredDirection);
        }
    }

    /**
     * mannually apply gravity to the character if the character is on the ground
     * 
     * invoked every frame hence check grounded state inside of this method
     */
    void ApplyGravityMannual()
    {
        if(!isGrounded() && playerRigidBody.velocity.y >= terminalVelocity)
        {
            playerRigidBody.AddForce(Vector3.down * statsContainer.gravityMultiplier, ForceMode.Acceleration);
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

    //return the angle as a float where 1 is flat and 0 is upright
    float NormalToAngle(Vector3 normal)
    {
        return (1 - normal.normalized.y) / 1;
    }

    /**
     * Move the player according to the desired direction
     */
    void MovePlayer(float speed, float accelMultiplier, Vector3 direction)
    {
        Vector3 MoveVelocity = direction*speed - playerRigidBody.velocity;
        playerRigidBody.AddForce(MoveVelocity * accelMultiplier * MovementMultiplier, ForceMode.Acceleration);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdvancedMovementScript : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public CapsuleCollider playerCapsuleCollider;
    public GameObject headPointer;
    public GameObject featPointer;

    public float WalkingSpeed;
    public float RunningSpeed;

    public Vector3 desiredVelocity;

    float pointerSpeedMultiplier = 5f;
    float playerSpeed;

    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        ContactDetection();
        moveCharacter();
    }

    void setSpeed()
    {
        Vector3 forward = Vector3.zero;
        Vector3 sideways = Vector3.zero;

        if (Input.GetKey("w") && !Input.GetKey("s"))
        {
            //x axis +
            forward = headPointer.transform.forward;
        }
        else if (!Input.GetKey("w") && Input.GetKey("s"))
        {
            //x axis -
            forward = -headPointer.transform.forward;
        }

        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            //z axis -
            sideways = -headPointer.transform.right;
        }
        else if (!Input.GetKey("a") && Input.GetKey("d"))
        {
            //x axis +
            sideways = headPointer.transform.right;
        }

        desiredVelocity = forward + sideways;

        if(Input.GetKey(KeyCode.LeftControl))
        {
            playerSpeed = RunningSpeed;
        }
        else
        {
            playerSpeed = WalkingSpeed;
        }

    }

    void moveCharacter()
    {
        Vector3 velDiff = desiredVelocity * playerSpeed - playerRigidBody.velocity;
        playerRigidBody.AddForce(velDiff, ForceMode.Acceleration);
    }

    void movePointer()
    {
        Vector3 velDiff = desiredVelocity - featPointer.transform.localPosition;
        featPointer.transform.Translate(velDiff * pointerSpeedMultiplier * Time.deltaTime);
    }

    /**
     * detect ground/slope contact
     */
    void ContactDetection()
    {
        //slope handling
        RaycastHit detectionHit;
        Vector3 slopeVector = Vector3.up;
        if (Physics.Raycast(playerRigidBody.transform.position - new Vector3(0,0.3f,0), desiredVelocity.normalized * 1 + Vector3.down * 0.6f, out detectionHit, 1, groundMask)) {
            Debug.DrawLine(detectionHit.point, detectionHit.point + Vector3.up * 0.2f, Color.blue, 10);
        }
        desiredVelocity = new Vector3(desiredVelocity.x, detectionHit.point.y - (playerRigidBody.transform.position.y - 0.5f), desiredVelocity.z).normalized;
        DebugDirectionCast();
    }

    void DebugDirectionCast()
    {
        Vector3 direction = playerRigidBody.velocity.normalized;
        Debug.DrawLine(playerRigidBody.transform.position, playerRigidBody.transform.position + desiredVelocity, Color.red, 0.2f);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(playerRigidBody.transform.position - Vector3.up * playerCapsuleCollider.height, playerCapsuleCollider.radius + GroundDistance, groundMask);
    }
}

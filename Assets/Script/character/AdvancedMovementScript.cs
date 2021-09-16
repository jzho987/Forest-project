using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdvancedMovementScript : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public GameObject headPointer;
    public GameObject featPointer;

    public float WalkingSpeed;
    public float RunningSpeed;

    public Vector3 desiredVelocity;

    float pointerSpeedMultiplier = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        moveCharacter();
        movePointer();
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

        Vector3 direction = forward + sideways;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            desiredVelocity = direction.normalized * RunningSpeed;
        }
        else
        {
            desiredVelocity = direction.normalized * WalkingSpeed;
        }
    }

    void moveCharacter()
    {
        Vector3 velDiff = desiredVelocity - playerRigidBody.velocity;
        playerRigidBody.AddForce(velDiff, ForceMode.Acceleration);
    }

    void movePointer()
    {
        Vector3 velDiff = desiredVelocity - featPointer.transform.localPosition;
        featPointer.transform.Translate(velDiff * pointerSpeedMultiplier * Time.deltaTime);
    }
}

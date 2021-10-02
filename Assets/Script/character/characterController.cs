﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdvancedMovementScript))]
public class characterController : MonoBehaviour
{
    [SerializeField] PlayerInventorySystem playerInventorySystem;
    [SerializeField] AdvancedMovementScript playerMovementSystem;
    public float interactionDistance;
    public GameObject crossHairAnchor;
    [SerializeField] int HarvestStrength;

    //keybinding used for input tracking
    [SerializeField] string movementForward;
    [SerializeField] string movementBackward;
    [SerializeField] string movementleft;
    [SerializeField] string movementright;

    [SerializeField] string jump;
    [SerializeField] string sneak;
    [SerializeField] string sprint;

    //use this for player property
    [SerializeField] float swingCoolDownTime;
    float swingCoolDown;

    worldState currState;
    enum worldState
    {
        inspection, //player is inspecting inventory
        active //player is active in the world
    }

    // Start is called before the first frame update
    void Start()
    {
        swingCoolDown = 0f;
        currState = worldState.active;
    }

    // Update is called once per frame
    void Update()
    {
        SwingCoolDown();
        if(currState == worldState.active)
        {
            ActiveInput();
        }
        else if(currState == worldState.inspection)
        {
            inspectInput();
        }
    }

    void movementInput()
    {
        playerMovementSystem.InputDirection(Input.GetKey("w"), Input.GetKey("s"), Input.GetKey("a"), Input.GetKey("d"));
    }

    void InteractionInput(Interactable interactableObjectHit)
    {
        //left click action can be help down since there is use cool down
        if (Input.GetMouseButton(0))
        {
            //only swing when cool down is clear
            if (swingCoolDown == 0)
            {
                interactableObjectHit.f1Interaction(this);
                swingCoolDown = swingCoolDownTime;
                //call to display the swing animation
            }
            else
            {

            }
        }
        //right click action only have tap right now.
        else if (Input.GetMouseButtonDown(1))
        {
            interactableObjectHit.f2Interaction(this);
        }
    }

    void defaultInput()
    {
        //default actions
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
        else if (Input.GetKeyDown("e"))
        {
            //open inventory
            switchState();
            Cursor.lockState = CursorLockMode.None;
            playerInventorySystem.spawnUI();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            playerInventorySystem.incrementSelection();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            playerInventorySystem.decrementSelection();
        }
    }

    void ActiveInput() 
    { 
        //raycast interaction
        RaycastHit hit;
        if (Physics.Raycast(crossHairAnchor.transform.position, crossHairAnchor.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.tag.Equals("interactable"))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                InteractionInput(interactable);
            }
        }
        defaultInput();
        movementInput();
    }

    void inspectInput()
    {
        /* exit inventory
         */
        if (Input.GetKeyDown("e"))
        {
            switchState();
            Cursor.lockState = CursorLockMode.Locked;
            playerInventorySystem.killUI();
        }
    }

    public void switchState()
    {
        Debug.Log("stateSwitched");
        if (currState == worldState.active)
            currState = worldState.inspection;
        else
            currState = worldState.active;
    }

    void SwingCoolDown()
    {
        swingCoolDown = !(swingCoolDown <= 0) ? swingCoolDown - Time.deltaTime : 0;
    }

    public int getHarvestStrength()
    {
        return HarvestStrength;
    }

    public PlayerInventorySystem getPlayerInventorySystem()
    {
        return this.playerInventorySystem;
    }
}

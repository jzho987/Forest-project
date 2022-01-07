using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AdvancedMovementScript))]
public class characterController : MonoBehaviour
{
    [SerializeField] initializeCharacter initialize;

    characterEvent events;
    CharacterStatistics characterStatistics;

    [SerializeField] PlayerInventorySystem playerInventorySystem;
    [SerializeField] handAnimation playerHandAnimation;
    public GameObject crossHairAnchor;

    [SerializeField] GameObject cameraGO;
    [SerializeField] GameObject UIGO;
    public PhotonView PV;

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
    [SerializeField] LayerMask interactableMask;
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
        initialize.onInitializeCharacter += initializeCharacterCallback;
        swingCoolDown = 0f;
        currState = worldState.active;

        if(!PV.IsMine)
        {
            Destroy(cameraGO);
            Destroy(UIGO);
        }

    }

    private void initializeCharacterCallback(characterEvent events, CharacterStatistics stats)
    {
        Debug.Log("initialized hehe");
        this.events = events;
        this.characterStatistics = stats;
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
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

            if (Input.GetKey(movementForward) || Input.GetKey(movementBackward) || Input.GetKey(movementleft) || Input.GetKey(movementright))
            {
                playerHandAnimation.startWalkingAnimation();
            }
            else 
            {
                playerHandAnimation.endWalkingAnimation();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                events.callOnJumpDetected();
            }
        }
        
    }

    void movementInput()
    {
        events.callMovementInput(Input.GetKey(movementForward), Input.GetKey(movementBackward), Input.GetKey(movementleft), Input.GetKey(movementright));
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
            events.callOnInventoryOpen();
        }
        else if (Input.GetKeyDown("c"))
        {
            //open crafting menu
            switchState();
            Cursor.lockState = CursorLockMode.None;
            events.callOnCraftingTabOpen();
        }

        //if there are mouse scroll wheel input, call event
        //done like this so no unnecessary event calls
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            events.callOnMouseScrollUp();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            events.callOnMouseScrollDown();
        }
    }

    void ActiveInput() 
    { 
        //raycast interaction
        RaycastHit hit;
        if (Physics.Raycast(crossHairAnchor.transform.position, crossHairAnchor.transform.forward, out hit, characterStatistics.interactionDistance, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            InteractionInput(interactable);
        }
        defaultInput();
        movementInput();
    }

    void inspectInput()
    {
        /* exit all UI elements
         */
        if (Input.GetKeyDown("e"))
        {
            switchState();
            Cursor.lockState = CursorLockMode.Locked;
            events.callOnExitUIMenus();
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

    public void SwingAnimation()
    {
        playerHandAnimation.swingAxe(swingCoolDownTime);
    }

    public float getHarvestStrength()
    {
            return characterStatistics.AxeProciciency;
    }
    
    public PlayerInventorySystem getPlayerInventorySystem()
    {
        return this.playerInventorySystem;
    }
}

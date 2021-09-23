using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] PlayerInventorySystem playerInventorySystem;
    public float interactionDistance;
    public GameObject crossHairAnchor;
    [SerializeField] int HarvestStrength;

    worldState currState;
    enum worldState
    {
        inspection, //player is inspecting inventory
        active //player is active in the world
    }

    // Start is called before the first frame update
    void Start()
    {
        currState = worldState.active;
    }

    // Update is called once per frame
    void Update()
    {
        if(currState == worldState.active)
        {
            ActiveInput();
        }
        else if(currState == worldState.inspection)
        {
            inspectInput();
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
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.f1Interaction(this);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    interactable.f2Interaction(this);
                }
            }
        }

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

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            playerInventorySystem.incrementSelection();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            playerInventorySystem.decrementSelection();
        }
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

    public int getHarvestStrength()
    {
        return HarvestStrength;
    }

    public PlayerInventorySystem getPlayerInventorySystem()
    {
        return this.playerInventorySystem;
    }
}

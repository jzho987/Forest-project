using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
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
                else if (Input.GetKeyDown("e"))
                {
                    interactable.PickUpInteraction();
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
            this.GetComponent<PlayerInventorySystem>().spawnUI();
        }

    }

    void inspectInput()
    {
        /* exit inventory
         */
        if (Input.GetKeyDown("e"))
        {
            switchState();
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
}

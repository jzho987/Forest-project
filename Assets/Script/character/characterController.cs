using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float interactionDistance;
    public GameObject crossHairAnchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findInteractable();
    }

    void findInteractable() 
    {
        RaycastHit hit;
        if (Physics.Raycast(crossHairAnchor.transform.position, crossHairAnchor.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.tag.Equals("interactable")) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                Debug.Log(interactable.Interactions());
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.f1Interaction(1);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    interactable.f2Interaction();
                }
                else if(Input.GetKeyDown("e"))
                {
                    interactable.PickUpInteraction();
                }
            }
        }
    }
}

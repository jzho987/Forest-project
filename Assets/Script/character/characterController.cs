using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float interactionDistance;

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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance))
        {
            if (hit.collider.tag.Equals("interactable")) {
                Interactable script = hit.collider.GetComponent<Interactable>();
                if (Input.GetMouseButtonDown(0))
                {
                    script.f1Interaction();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    script.f2Interaction();
                }
            }
        }
    }
}

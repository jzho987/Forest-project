using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handAnimation : MonoBehaviour
{

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startWalkingAnimation()
    {
        animator.SetBool("Moving",true);
    }

    public void endWalkingAnimation()
    {
        animator.SetBool("Moving", false);
    }

    public void swingAxe(float speed)
    {
        animator.SetFloat("swingSpeed", speed);
        animator.Play("Swing");
    }
}

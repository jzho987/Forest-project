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

    public void startHandAnimation(float time)
    {
        animator.Play("swing");
        animator.SetFloat("swingSpeed", 0.833f / time);
    }
}

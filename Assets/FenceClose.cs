using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceClose : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenFence()
    {
        animator.SetBool("OpenFence", true);
    } 
}

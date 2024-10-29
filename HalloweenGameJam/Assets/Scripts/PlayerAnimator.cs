using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb;

    // Animator initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetInteger("X Axis", 0);
        animator.SetInteger("Y Axis", 1);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0) {
            animator.SetBool("Is Moving", true);

        } else {
            animator.SetBool("Is Moving", false);
        }
    }

        void Update()
    {
        int x = (int)Math.Round(Input.GetAxisRaw("Horizontal"));
        int y = (int)Math.Round(Input.GetAxisRaw("Vertical"));

        // Don't update the variables to both be 0 when idling so that the animator remembers the last orientation
        if (x != 0 || y != 0)
        {
            animator.SetInteger("X Axis", x);
            animator.SetInteger("Y Axis", y);
        }
        

    }
}

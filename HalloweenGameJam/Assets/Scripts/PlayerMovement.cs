using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 

    [SerializeField] private float speed;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashLength;
    private float directionX;
    private float directionY;
    private bool isDashing;

    [SerializeField] private float cooldown;
    private float dashTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // store rigidbody2d component in rb
        isDashing = false;
        dashTimer = cooldown;
    }

    private void FixedUpdate()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // returns -1 if A pressed, 1 if D pressed, otherwise 0
        directionY = Input.GetAxisRaw("Vertical"); // returns -1 if S pressed, 1 if W pressed, otherwise 0

        // apply speed to character (vertical, horizontal or diagonal)
        if (!isDashing)
        {
            rb.velocity = new Vector2(directionX, directionY).normalized * speed;
        }

    }

    void Update()
    {
        dashTimer += Time.deltaTime; // increment cooldown over time

        if (Input.GetKeyDown(KeyCode.Space) && dashTimer > cooldown)
        {
            StartCoroutine(Dash()); // start the Dash coroutine, i.e. function continues repeatedly until yield return called
            dashTimer = 0f; // reset timer
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true; // character's velocity cannot be affected externally during dash
        rb.AddForce(new Vector2(directionX, directionY).normalized * dashForce, ForceMode2D.Impulse); // player will dash in the currently inputted direction 
        yield return new WaitForSeconds(dashLength); // repeat above code until certain amount of seconds has passed
        isDashing = false;
    }
}

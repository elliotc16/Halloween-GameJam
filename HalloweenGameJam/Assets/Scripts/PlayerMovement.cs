using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float dashForce;
    private float directionX;
    private float directionY;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDashing = false;
    }

    private void FixedUpdate()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        if (!isDashing)
        {
            rb.velocity = new Vector2(directionX, directionY).normalized * speed;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.AddForce(new Vector2(directionX, directionY).normalized * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
    }
}

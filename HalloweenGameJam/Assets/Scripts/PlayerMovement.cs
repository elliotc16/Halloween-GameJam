using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 

    [SerializeField] private float speed;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashLength;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackLength;
    private float directionX;
    private float directionY;
    private bool isDashing;
    private bool isKnockback;

    private Camera mainCamera;
    private Vector3 mousePos;
    private Vector3 pointDirection;
    private float rotationZ;

    [SerializeField] private float cooldown;
    private float dashTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // store rigidbody2d component in rb
        isDashing = false;
        isKnockback = false;
        dashTimer = cooldown;
    }

    private void FixedUpdate()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // returns -1 if A pressed, 1 if D pressed, otherwise 0
        directionY = Input.GetAxisRaw("Vertical"); // returns -1 if S pressed, 1 if W pressed, otherwise 0

        // apply speed to character (vertical, horizontal or diagonal)
        if (!isDashing & !isKnockback)
        {
            rb.velocity = new Vector2(directionX, directionY).normalized * speed;
        }

    }

    void Update()
    {
        dashTimer += Time.deltaTime; // increment cooldown over time

        if (Input.GetKeyDown(KeyCode.Space) && dashTimer > cooldown && isKnockback == false)
        {
            StartCoroutine(Dash()); // start the Dash coroutine, i.e. function continues repeatedly until yield return called
            dashTimer = 0f; // reset timer
        }

        // take mouse position using camera as reference
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // direction vector = difference between mouse position and player position
        pointDirection = mousePos - transform.position;
        pointDirection = new Vector2(pointDirection.x, pointDirection.y).normalized;

        // rotate the player towards the cursor (-90 since original sprite is vertical)
        rotationZ = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
    }

    private IEnumerator Dash()
    {
        isDashing = true; // character's velocity cannot be affected externally during dash
        rb.AddForce(new Vector2(directionX, directionY).normalized * dashForce, ForceMode2D.Impulse); // player will dash in the currently inputted direction 
        yield return new WaitForSeconds(dashLength); // repeat above code until certain amount of seconds has passed
        isDashing = false;
    }

    private IEnumerator Knockback(Vector2 direction)
    {
        isKnockback = true; // character's velocity cannot be affected externally during dash
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); // player will dash in the currently inputted direction 
        yield return new WaitForSeconds(knockbackLength); // repeat above code until certain amount of seconds has passed
        isKnockback = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.tag == "Enemy" && !isKnockback)
        {
            if (isDashing)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
            Vector2 direction = collider.gameObject.transform.position - transform.position;
            this.GetComponent<PlayerCombat>().TakeDamage(1);
            StartCoroutine(Knockback(-direction));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private float timer;
    private Vector3 direction;
    private Vector3 mousePos;
    private Camera mainCamera;

    private void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody2D>();

        // take mouse position using camera as reference
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // direction vector = difference between mouse position and player position
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        direction = mousePos - playerTransform.position;
        direction = new Vector2(direction.x, direction.y).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed; // apply velocity in fixed direction
    }

    private void Update()
    {
        // destroy bullet if it doesn't make contact for too long
        if (timer > lifetime)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }
}

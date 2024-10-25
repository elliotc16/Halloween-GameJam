using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private float timer;
    private Vector2 direction;
    private Vector2 mousePos;
    private Camera mainCamera;

    private void Awake()
    {
        timer = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       // rb.velocity = direction * speed; // apply velocity in fixed direction
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

    /// <summary>
    /// Moves bullet to startPos and sets velocity towards destPos
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="destPos"></param>
    public void Fire(Vector2 startPos, Vector2 destPos)
    {
        transform.position = startPos;
        direction = destPos - startPos;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
}
